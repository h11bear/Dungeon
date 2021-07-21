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

$webApp = New-AzWebApp -ResourceGroupName $ResourceGroup -AppServicePlan $Plan -Name $AppName 
#$deplomentConfig = az webapp create --resource-group $ResourceGroup --plan $Plan --name $AppName --runtime "DOTNETCORE:3.1" --deployment-local-git

$webApp
#Write-Output "git app url: $($deplomentConfig.deploymentLocalGitUrl)"

#dotnet publish ../../Dungeon.Web/Dungeon.Web.csproj --configuration Release --output ./publish
#dotnet build ../../Dungeon.Web/Dungeon.Web.csproj /nologo /p:PublishProfile=Release /p:PackageLocation="C:\Some\Path\package" /p:OutDir="C:\Some\Path\out" /p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /maxcpucount:1 /p:platform="Any CPU" /p:configuration="Release" /p:DesktopBuildPackageLocation="C:\Some\Path\package\package.zip"
#dotnet build ../../Dungeon.Web/Dungeon.Web.csproj /nologo /p:PublishProfile=Release /p:PackageLocation="./publish" /p:OutDir="./out" /p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /maxcpucount:1 /p:platform="Any CPU" /p:configuration="Release" /p:DesktopBuildPackageLocation="./publish/package.zip"

# Set-Location publish

# Write-Host "Creating $AppName as an Azure webapp"

# #https://github.com/Azure/azure-cli/issues/15832
# ##az webapp up --dryrun --name $AppName --resource-group $ResourceGroup --runtime '"DOTNETCORE|3.1"' --sku F1
# #az webapp create --name $AppName --plan $Plan --resource-group $ResourceGroup --runtime '"DOTNETCORE|3.1"'
# #az webapp create --name $AppName --resource-group $ResourceGroup --plan $Plan --runtime '"DOTNETCORE|3.1"' --deployment-local-git

# #az webapp up --name $AppName --resource-group $ResourceGroup --plan $Plan --runtime "DOTNETCORE:3.1"
# #https://github.com/MicrosoftDocs/azure-docs/issues/43633

# az webapp up --name $AppName --resource-group $ResourceGroup --plan $Plan --html

# #https://docs.microsoft.com/en-us/azure/app-service/scripts/cli-deploy-local-git

# #https://github.com/h11bear/Dungeon.git

# Set-Location ..

# #az group delete --resource-group DungeonResourceGroup
# #az webapp up --name RoyDungeon --resource-group DungeonResourceGroup --plan dungeonPlan --runtime "DOTNETCORE:3.1" --html
# #az webapp up --name RoyDungeon --resource-group DungeonResourceGroup --plan dungeonPlan --html

# $PropertiesObject = @{
#     scmType = "LocalGit";
# }

# Set-AzResource -PropertyObject $PropertiesObject -ResourceGroupName roydungeon1 `
# -ResourceType Microsoft.Web/sites/config -ResourceName RoyDungeon1/web `
# -ApiVersion 2015-08-01 -Force

# git remote add azure https://roydungeon1.scm.azurewebsites.net:443/RoyDungeon1.git
# git push azure master

#Remove-AzResourceGroup -Name DungeonResourceGroup
#Remove-AzWebApp -ResourceGroupName DungeonResourceGroup -Name RoyDungeon