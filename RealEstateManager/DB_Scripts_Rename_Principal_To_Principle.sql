-- Rename column PrincipalAmount to PrincipleAmount in PropertyLoanTransaction if necessary
-- This script is safe to run multiple times.
IF COL_LENGTH('dbo.PropertyLoanTransaction', 'PrincipalAmount') IS NOT NULL
BEGIN
    PRINT 'Renaming column PrincipalAmount to PrincipleAmount on PropertyLoanTransaction';
    EXEC sp_rename 'dbo.PropertyLoanTransaction.PrincipalAmount', 'PrincipleAmount', 'COLUMN';
END
ELSE
BEGIN
    PRINT 'Column PrincipalAmount does not exist or was already renamed.';
END
