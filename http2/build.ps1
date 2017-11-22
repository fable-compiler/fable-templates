$ErrorActionPreference = "Stop"

yarn

Set-Location $psscriptroot\src
dotnet restore
dotnet fable yarn-build

Set-Location $psscriptroot