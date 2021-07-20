param(
    [String]$AppName = $(throw "AppName is required"),
    [string]$ResourceGroup = $(throw "ResourceGroup is required"),
    [string]$Plan = $(throw "Plan is required"),
    [string]$Location = $(throw "Location is required")
)
<#
./Publish-Dungeon -AppName RoyDungeon -ResourceGroup DungeonResourceGroup -Location eastus -Plan dungeonPlan
./Publish-Dungeon -AppName RoyDungeon -ResourceGroup DungeonResourceGroup -Location eastus
#>

$resourceExists = az group exists --name $ResourceGroup
if ($resourceExists -eq $false) {
    Write-Output "$ResourceGroup does not exist, creating"
    az group create --name $ResourceGroup --location $Location --output table
    Write-Output "Creating app service plan $Plan for $ResourceGroup"
    az appservice plan create --name $Plan --resource-group $ResourceGroup --sku FREE --output table
}
else {
    Write-Output "$ResourceGroup already exists"
}

$deployFolder = "./publish"

if (Test-Path -Path $deployFolder)
{
    Write-Output "Cleaning previous published output"
    Remove-Item $deployFolder -Recurse
}

dotnet publish ../../Dungeon.Web/Dungeon.Web.csproj --configuration Release --output ./publish

Set-Location publish

Write-Host "Creating $AppName as an Azure webapp"

#https://github.com/Azure/azure-cli/issues/15832
##az webapp up --dryrun --name $AppName --resource-group $ResourceGroup --runtime '"DOTNETCORE|3.1"' --sku F1
#az webapp create --name $AppName --plan $Plan --resource-group $ResourceGroup --runtime '"DOTNETCORE|3.1"'
az webapp create --name $AppName --resource-group $ResourceGroup --plan $Plan --runtime '"DOTNETCORE|3.1"' --deployment-local-git

#https://docs.microsoft.com/en-us/azure/app-service/scripts/cli-deploy-local-git

#https://github.com/h11bear/Dungeon.git

Set-Location ..

#az group delete --resource-group DungeonResourceGroup
