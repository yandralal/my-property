# PowerShell script to deploy Angular and WebAPI to IIS as applications under one site
# Requires Administrator privileges

# Configuration
$SiteName = "MyProperty"
$SitePort = 8080
$HostName = "myproperty.com"
$SitePhysicalPath = "C:\inetpub\wwwroot\MyProperty"

$AngularProjectPath = "E:\Source Code\my-property\my-property-ui"
$AngularDistPath = "$AngularProjectPath\dist\my-property-ui\browser"
$AngularAppPath = $SitePhysicalPath

$WebApiProjectPath = "E:\Source Code\my-property\MyPropertyApi\MyPropertyApi.csproj"
$WebApiPublishPath = "E:\Source Code\my-property\MyPropertyApi\publish"
$WebApiAppPath = "$SitePhysicalPath\api"

# Database Configuration
$SqlServerInstance = "localhost\SQLEXPRESS"
$DatabaseName = "MyPropertyDB"
$AppPoolIdentity = "IIS APPPOOL\$SiteName"

# Check if running as Administrator
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) {
    Write-Error "This script must be run as Administrator!"
    exit 1
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Deploying MyProperty to IIS" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Import-Module WebAdministration

# Clean up old sites if they exist
Write-Host "Checking for old sites to clean up..." -ForegroundColor Yellow
$oldSites = @("my-property-ui", "my-property-api")
foreach ($oldSite in $oldSites) {
    $site = Get-Website -Name $oldSite -ErrorAction SilentlyContinue
    if ($site) {
        Write-Host "Removing old site: $oldSite"
        Stop-Website -Name $oldSite -ErrorAction SilentlyContinue
        Remove-Website -Name $oldSite
        
        # Remove associated app pool if it exists and is not used by other sites
        $appPool = Get-ChildItem IIS:\AppPools | Where-Object { $_.Name -eq $oldSite }
        if ($appPool) {
            $poolUsage = Get-Website | Where-Object { $_.ApplicationPool -eq $oldSite }
            if (-not $poolUsage) {
                Write-Host "Removing old app pool: $oldSite"
                Remove-WebAppPool -Name $oldSite
            }
        }
    }
}

# Step 1: Stop existing site and apps if they exist
Write-Host "Step 1: Stopping existing site and applications..." -ForegroundColor Yellow
$existingSite = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
if ($existingSite) {
    Write-Host "Stopping website '$SiteName'..."
    Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
    
    $appPool = $existingSite.ApplicationPool
    Write-Host "Stopping application pool '$appPool'..."
    Stop-WebAppPool -Name $appPool -ErrorAction SilentlyContinue
    
    # Wait for app pool to fully stop
    $timeout = 30
    $elapsed = 0
    while ($elapsed -lt $timeout) {
        $state = (Get-WebAppPoolState -Name $appPool -ErrorAction SilentlyContinue).Value
        if ($state -eq "Stopped" -or $null -eq $state) {
            break
        }
        Start-Sleep -Seconds 1
        $elapsed++
    }
    Write-Host "Application pool stopped."
}

# Step 2: Update Angular environment configuration
Write-Host ""
Write-Host "Step 2: Updating Angular environment configuration..." -ForegroundColor Yellow
$envFilePath = "$AngularProjectPath\src\environments\environment.ts"
$envContent = Get-Content $envFilePath -Raw
$newApiUrl = "http://$HostName`:$SitePort/api"
$envContent = $envContent -replace "apiUrl:\s*'[^']*'", "apiUrl: '$newApiUrl'"
Set-Content $envFilePath -Value $envContent -NoNewline
Write-Host "API URL updated to: $newApiUrl"

# Step 3: Build Angular app
Write-Host ""
Write-Host "Step 3: Building Angular application..." -ForegroundColor Yellow
pushd $AngularProjectPath
npm run build -- --configuration production
if ($LASTEXITCODE -ne 0) {
    Write-Error "Angular build failed!"
    popd
    exit 1
}
popd
Write-Host "Angular build completed."

# Step 4: Publish WebAPI
Write-Host ""
Write-Host "Step 4: Publishing WebAPI..." -ForegroundColor Yellow

# Clean publish directory to avoid nested path issues
if (Test-Path $WebApiPublishPath) {
    Remove-Item $WebApiPublishPath -Recurse -Force -ErrorAction SilentlyContinue
}

dotnet publish $WebApiProjectPath -c Release -o $WebApiPublishPath
if ($LASTEXITCODE -ne 0) {
    Write-Error "WebAPI publish failed!"
    exit 1
}
Write-Host "WebAPI publish completed."

# Step 5: Create/Update directory structure
Write-Host ""
Write-Host "Step 5: Preparing directories..." -ForegroundColor Yellow

# Create site root if needed
if (-not (Test-Path $SitePhysicalPath)) {
    New-Item -ItemType Directory -Path $SitePhysicalPath -Force | Out-Null
}

# Clean root directory for Angular (but preserve api subdirectory if it exists)
Get-ChildItem -Path $SitePhysicalPath -Exclude "api" | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue

# Create API app directory
if (-not (Test-Path $WebApiAppPath)) {
    New-Item -ItemType Directory -Path $WebApiAppPath -Force | Out-Null
} else {
    Remove-Item $WebApiAppPath\* -Recurse -Force -ErrorAction SilentlyContinue
}

# Step 6: Copy files
Write-Host ""
Write-Host "Step 6: Copying application files..." -ForegroundColor Yellow
Write-Host "Copying Angular files to root..."
Copy-Item "$AngularDistPath\*" $AngularAppPath -Recurse -Force
Write-Host "Copying Angular web.config to exclude /api from routing..."
Copy-Item "$AngularProjectPath\web.config" "$SitePhysicalPath\web.config" -Force
Write-Host "Copying WebAPI files to /api..."
Copy-Item "$WebApiPublishPath\*" $WebApiAppPath -Recurse -Force

# Step 7: Configure IIS
Write-Host ""
Write-Host "Step 7: Configuring IIS..." -ForegroundColor Yellow

# Create main app pool if needed
$mainAppPool = $SiteName
$existingPool = Get-ChildItem IIS:\AppPools | Where-Object { $_.Name -eq $mainAppPool }
if (-not $existingPool) {
    Write-Host "Creating application pool '$mainAppPool'..."
    New-WebAppPool -Name $mainAppPool
    Set-ItemProperty IIS:\AppPools\$mainAppPool -Name managedRuntimeVersion -Value ""
}

# Create website if it doesn't exist
if (-not $existingSite) {
    Write-Host "Creating website '$SiteName'..."
    New-Website -Name $SiteName -PhysicalPath $SitePhysicalPath -ApplicationPool $mainAppPool -Port $SitePort -HostHeader $HostName
} else {
    Write-Host "Website '$SiteName' already exists."
}

# Add hosts file entry for custom hostname
$hostsPath = "C:\Windows\System32\drivers\etc\hosts"
$hostsEntry = "127.0.0.1    $HostName"
$hostsContent = Get-Content $hostsPath -Raw
if ($hostsContent -notmatch [regex]::Escape($HostName)) {
    Write-Host "Adding entry to hosts file for $HostName..."
    Add-Content -Path $hostsPath -Value "`n$hostsEntry"
    Write-Host "Hosts file updated."
}

# Remove old UI application if it exists
$uiApp = Get-WebApplication -Site $SiteName -Name "ui" -ErrorAction SilentlyContinue
if ($uiApp) {
    Write-Host "Removing old UI application..."
    Remove-WebApplication -Site $SiteName -Name "ui"
}

# Remove existing API application if it exists
$apiApp = Get-WebApplication -Site $SiteName -Name "api" -ErrorAction SilentlyContinue
if ($apiApp) {
    Remove-WebApplication -Site $SiteName -Name "api"
}

# Create API application (Angular is at root)
Write-Host "Creating API application at /api..."
New-WebApplication -Site $SiteName -Name "api" -PhysicalPath $WebApiAppPath -ApplicationPool $mainAppPool

# Step 8: Configure Database Permissions
Write-Host ""
Write-Host "Step 8: Configuring database permissions..." -ForegroundColor Yellow

try {
    # Check if SQL Server is accessible
    $sqlTest = sqlcmd -S $SqlServerInstance -E -Q "SELECT @@VERSION" -h -1 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Warning "SQL Server instance '$SqlServerInstance' is not accessible. Skipping database configuration."
        Write-Warning "Please configure database permissions manually if needed."
    } else {
        Write-Host "Configuring SQL Server login and permissions for '$AppPoolIdentity'..."
        
        # Create SQL script to grant permissions
        $sqlScript = @"
USE [master];
GO

-- Create login if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'$AppPoolIdentity')
BEGIN
    CREATE LOGIN [$AppPoolIdentity] FROM WINDOWS WITH DEFAULT_DATABASE=[master];
    PRINT 'Login created for $AppPoolIdentity';
END
ELSE
BEGIN
    PRINT 'Login already exists for $AppPoolIdentity';
END
GO

USE [$DatabaseName];
GO

-- Create user if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'$AppPoolIdentity')
BEGIN
    CREATE USER [$AppPoolIdentity] FOR LOGIN [$AppPoolIdentity];
    PRINT 'User created in $DatabaseName for $AppPoolIdentity';
END
ELSE
BEGIN
    PRINT 'User already exists in $DatabaseName for $AppPoolIdentity';
END
GO

-- Grant db_owner role
IF NOT IS_ROLEMEMBER('db_owner', '$AppPoolIdentity') = 1
BEGIN
    ALTER ROLE [db_owner] ADD MEMBER [$AppPoolIdentity];
    PRINT 'Granted db_owner role to $AppPoolIdentity';
END
ELSE
BEGIN
    PRINT '$AppPoolIdentity already has db_owner role';
END
GO
"@

        # Save script to temp file
        $tempSqlFile = [System.IO.Path]::GetTempFileName() + ".sql"
        Set-Content -Path $tempSqlFile -Value $sqlScript
        
        # Execute SQL script
        $sqlOutput = sqlcmd -S $SqlServerInstance -E -i $tempSqlFile 2>&1
        
        # Clean up temp file
        Remove-Item $tempSqlFile -Force -ErrorAction SilentlyContinue
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Database permissions configured successfully." -ForegroundColor Green
            Write-Host $sqlOutput
        } else {
            Write-Warning "Failed to configure database permissions. Error output:"
            Write-Warning $sqlOutput
            Write-Warning "You may need to configure database permissions manually."
        }
    }
} catch {
    Write-Warning "Error configuring database permissions: $_"
    Write-Warning "You may need to configure database permissions manually."
}

# Step 9: Start the site
Write-Host ""
Write-Host "Step 9: Starting website..." -ForegroundColor Yellow

# Check if app pool is already started
$poolState = (Get-WebAppPoolState -Name $mainAppPool).Value
if ($poolState -ne "Started") {
    Start-WebAppPool -Name $mainAppPool
    Write-Host "Application pool started."
} else {
    Write-Host "Application pool is already running."
}

# Try to start website, ignore if already running
try {
    $siteState = (Get-WebsiteState -Name $SiteName).Value
    if ($siteState -ne "Started") {
        Start-Website -Name $SiteName -ErrorAction Stop
        Write-Host "Website started."
    } else {
        Write-Host "Website is already running."
    }
} catch {
    Write-Host "Website is already running."
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "Deployment Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host "Site Name: $SiteName"
Write-Host "Hostname: $HostName"
Write-Host "Angular UI: http://$HostName`:$SitePort"
Write-Host "WebAPI: http://$HostName`:$SitePort/api/swagger"
Write-Host "========================================" -ForegroundColor Green
