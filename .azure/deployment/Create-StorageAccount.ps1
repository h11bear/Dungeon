param(
    [String]$AppName = $(throw "AppName is required"),
    [string]$ResourceGroup = $(throw "ResourceGroup is required"),
    [string]$Location = $(throw "Location is required")
)
<#
    ./Create-StorageAccount.ps1 -AppName roydungeon2 -ResourceGroup DungeonResourceGroup -Location eastus
#>
#Connect-AzAccount

$storageName = "$($AppName)storage"
$containerName = "$($AppName)blobcontainer"

$storageAccount = New-AzStorageAccount -ResourceGroupName $ResourceGroup `
  -Name $storageName `
  -Location $Location `
  -SkuName Standard_RAGRS `
  -Kind StorageV2

$ctx = $storageAccount.Context

New-AzStorageContainer -Name $containerName -Context $ctx -Permission blob


#az storage account create --name $storageName --resource-group $ResourceGroup --location $Location --sku Standard_ZRS --encryption-services blob
az storage account show -n $storageName -g $ResourceGroup --query id --out tsv