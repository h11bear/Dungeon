param(
    [String]$AppName = $(throw "AppName is required"),
    [string]$ResourceGroup = $(throw "ResourceGroup is required")
)
<#
    ./Grant-StorageAccountAccess.ps1 -AppName roydungeon2 -ResourceGroup DungeonResourceGroup
#>
$storageName = "$($AppName)storage"
$spID = (Get-AzWebApp -ResourceGroupName $ResourceGroup -Name $AppName).identity.principalid
$storageId= (Get-AzStorageAccount -ResourceGroupName $ResourceGroup -Name $storageName).Id
New-AzRoleAssignment -ObjectId $spID -RoleDefinitionName "Storage Blob Data Contributor" -Scope $storageId
