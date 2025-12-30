# One-Click Installer for MyProperty Application with Auto-Prerequisites Installation
# Requires Administrator privileges

#Requires -RunAsAdministrator

$ErrorActionPreference = "Stop"

Write-Host ""
Write-Host "" -ForegroundColor Cyan
Write-Host "       MyProperty Auto-Installer v2.0                      " -ForegroundColor Cyan
Write-Host "" -ForegroundColor Cyan
Write-Host ""

# Function to download and install
function Install-Prerequisite {
    param(
        [string]$Name,
        [string]$Url,
        [string]$InstallerPath,
        [string]$InstallArgs = "/quiet /norestart"
    )
    
    Write-Host "  Downloading $Name..." -ForegroundColor Yellow
    Invoke-WebRequest -Uri $Url -OutFile $InstallerPath -UseBasicParsing
    Write-Host "  Installing $Name..." -ForegroundColor Yellow
    Start-Process -FilePath $InstallerPath -ArgumentList $InstallArgs -Wait
    Remove-Item $InstallerPath -Force -ErrorAction SilentlyContinue
    Write-Host " $Name installed successfully" -ForegroundColor Green
}

Write-Host " Checking and Installing Prerequisites " -ForegroundColor Yellow
Write-Host ""

# 1. Check and Install IIS
Write-Host "[1/4] Checking IIS..." -ForegroundColor Cyan
$iisFeature = Get-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole -ErrorAction SilentlyContinue
if ($iisFeature.State -ne "Enabled") {
    Write-Host "  IIS not found. Installing IIS..." -ForegroundColor Yellow
    try {
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServer -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-CommonHttpFeatures -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpErrors -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-ApplicationDevelopment -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-NetFxExtensibility45 -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-HealthAndDiagnostics -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpLogging -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-Security -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-RequestFiltering -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-Performance -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerManagementTools -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-ManagementConsole -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-StaticContent -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-DefaultDocument -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-DirectoryBrowsing -All -NoRestart
        Enable-WindowsOptionalFeature -Online -FeatureName IIS-ASPNET45 -All -NoRestart
        Write-Host " IIS installed successfully" -ForegroundColor Green
    } catch {
        Write-Error "Failed to install IIS: $_"
        pause
        exit 1
    }
} else {
    Write-Host " IIS is already installed" -ForegroundColor Green
}

# 2. Check and Install .NET SDK
Write-Host ""
Write-Host "[2/4] Checking .NET SDK..." -ForegroundColor Cyan
try {
    $dotnetVersion = dotnet --version 2>$null
    Write-Host " .NET SDK is already installed ($dotnetVersion)" -ForegroundColor Green
} catch {
    Write-Host "  .NET SDK not found. Installing .NET 9.0 SDK..." -ForegroundColor Yellow
    try {
        $dotnetUrl = "https://download.visualstudio.microsoft.com/download/pr/690416bb-be6c-42a0-8a74-337a60c2adb1/77e885bf45ff5f2f3ae58a48134e7218/dotnet-sdk-9.0.101-win-x64.exe"
        $dotnetInstaller = "$env:TEMP\dotnet-sdk-installer.exe"
        Install-Prerequisite -Name ".NET 9.0 SDK" -Url $dotnetUrl -InstallerPath $dotnetInstaller -InstallArgs "/quiet /norestart"
        
        # Refresh PATH
        $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
    } catch {
        Write-Warning "Failed to auto-install .NET SDK. Please install manually from: https://dotnet.microsoft.com/download/dotnet/9.0"
        pause
        exit 1
    }
}

# 3. Check and Install Node.js
Write-Host ""
Write-Host "[3/4] Checking Node.js..." -ForegroundColor Cyan
try {
    $nodeVersion = node --version 2>$null
    Write-Host " Node.js is already installed ($nodeVersion)" -ForegroundColor Green
} catch {
    Write-Host "  Node.js not found. Installing Node.js LTS..." -ForegroundColor Yellow
    try {
        $nodeUrl = "https://nodejs.org/dist/v20.18.1/node-v20.18.1-x64.msi"
        $nodeInstaller = "$env:TEMP\node-installer.msi"
        Install-Prerequisite -Name "Node.js LTS" -Url $nodeUrl -InstallerPath $nodeInstaller -InstallArgs "/quiet /norestart"
        
        # Refresh PATH
        $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
    } catch {
        Write-Warning "Failed to auto-install Node.js. Please install manually from: https://nodejs.org/"
        pause
        exit 1
    }
}

# 4. Check and Install URL Rewrite Module
Write-Host ""
Write-Host "[4/4] Checking IIS URL Rewrite Module..." -ForegroundColor Cyan
Import-Module WebAdministration -ErrorAction SilentlyContinue
$rewriteModule = Get-WebGlobalModule -Name "RewriteModule" -ErrorAction SilentlyContinue
if (-not $rewriteModule) {
    Write-Host "  URL Rewrite Module not found. Installing..." -ForegroundColor Yellow
    try {
        $rewriteUrl = "https://download.microsoft.com/download/1/2/8/128E2E22-C1B9-44A4-BE2A-5859ED1D4592/rewrite_amd64_en-US.msi"
        $rewriteInstaller = "$env:TEMP\rewrite_amd64_en-US.msi"
        Install-Prerequisite -Name "URL Rewrite Module" -Url $rewriteUrl -InstallerPath $rewriteInstaller
    } catch {
        Write-Warning "Failed to install URL Rewrite Module. Angular routing may not work properly."
    }
} else {
    Write-Host " URL Rewrite Module is already installed" -ForegroundColor Green
}

# 5. Install ASP.NET Core Hosting Bundle
Write-Host ""
Write-Host "[OPTIONAL] Checking ASP.NET Core Hosting Bundle..." -ForegroundColor Cyan
$hostingBundleKey = "HKLM:\SOFTWARE\WOW6432Node\Microsoft\Updates\.NET\Microsoft .NET Runtime - 9.0"
if (-not (Test-Path $hostingBundleKey)) {
    Write-Host "  ASP.NET Core Hosting Bundle not found. Installing..." -ForegroundColor Yellow
    try {
        $hostingBundleUrl = "https://download.visualstudio.microsoft.com/download/pr/2a7ae819-fbc4-4611-a1ba-f3b072d4ea25/32f3b931550f7b315d9827d564202eeb/dotnet-hosting-9.0.1-win.exe"
        $hostingBundleInstaller = "$env:TEMP\dotnet-hosting-bundle.exe"
        Install-Prerequisite -Name "ASP.NET Core Hosting Bundle" -Url $hostingBundleUrl -InstallerPath $hostingBundleInstaller -InstallArgs "/quiet /norestart OPT_NO_SHAREDFX=1 OPT_NO_RUNTIME=1"
    } catch {
        Write-Warning "Failed to install ASP.NET Core Hosting Bundle (optional)."
    }
} else {
    Write-Host " ASP.NET Core Hosting Bundle is already installed" -ForegroundColor Green
}

Write-Host ""
Write-Host "" -ForegroundColor Green
Write-Host "All prerequisites are installed!" -ForegroundColor Green
Write-Host "" -ForegroundColor Green
Write-Host ""

# Execute deployment script
Write-Host " Starting Application Deployment " -ForegroundColor Yellow
Write-Host ""

$deployScript = Join-Path $PSScriptRoot "..\deploy-to-iis.ps1"

if (-not (Test-Path $deployScript)) {
    Write-Error "Deployment script not found: $deployScript"
    pause
    exit 1
}

try {
    & $deployScript
    
    if ($LASTEXITCODE -eq 0 -or $null -eq $LASTEXITCODE) {
        Write-Host ""
        Write-Host "" -ForegroundColor Green
        Write-Host "          Installation Completed Successfully!            " -ForegroundColor Green
        Write-Host "" -ForegroundColor Green
        Write-Host ""
        Write-Host "NOTE: If IIS or .NET was just installed, you may need to restart" -ForegroundColor Yellow
        Write-Host "      your computer for changes to take full effect." -ForegroundColor Yellow
        Write-Host ""
    } else {
        Write-Error "Deployment failed"
        pause
        exit 1
    }
} catch {
    Write-Error "Installation failed: $($_.Exception.Message)"
    pause
    exit 1
}
