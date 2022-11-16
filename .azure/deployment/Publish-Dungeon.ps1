param(
    [String]$AppName = $(throw "AppName is required"),
    [string]$ResourceGroup = $(throw "ResourceGroup is required"),
    [string]$Plan = $(throw "Plan is required"),
    [string]$Location = $(throw "Location is required")
)
<#
./Publish-Dungeon -AppName RoyDungeon2 -ResourceGroup DungeonResourceGroup -Location eastus -Plan DungeonPlan
#>

$resourceExists = az group exists --name $ResourceGroup
if ($resourceExists -eq $false) {
    Write-Output "$ResourceGroup does not exist, creating"
    az group create --name $ResourceGroup --location $Location

    Write-Output "Creating app service plan $Plan for $ResourceGroup"
    az appservice plan create --name $Plan --resource-group $ResourceGroup --sku FREE

    # Write-Output "Creating webapp $AppName for $ResourceGroup"
    # az webapp create --resource-group $ResourceGroup --plan $Plan --name $AppName --runtime "DOTNETCORE:3.1"
}
else {
    Write-Output "$ResourceGroup already exists"
}

#az deployment group create --resource-group $resourceGroup --template-file ".\template\template.json" --parameters ".\template\parameters.json"

$deployFolder = "./publish"

if (Test-Path -Path $deployFolder)
{
    Write-Output "Cleaning previous published output"
    Remove-Item $deployFolder -Recurse
}


dotnet publish ../../Dungeon.RazorPages/Dungeon.RazorPages.csproj --configuration Release --output ./publish/www
#dotnet publish ../../Dungeon.RazorPages/Dungeon.RazorPages.csproj --configuration Debug --output ./publish/www

Compress-Archive -Path ./publish/www/* -DestinationPath "publish/$AppName.zip"

az webapp deployment source config-zip --resource-group DungeonResourceGroup --name $AppName --src "publish/$AppName.zip"

#Remove-AzResourceGroup -Name DungeonResourceGroup
