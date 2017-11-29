# paket update
Set-Location $psscriptroot\http2
.\.paket\paket.exe update
Set-Location $psscriptroot\yargs
.\.paket\paket.exe update
Set-Location $psscriptroot\vscode
.\.paket\paket.exe update

# ts2fable generate
yarn global add ts2fable
Set-Location $psscriptroot\http2
yarn
.\generate.ps1
Set-Location $psscriptroot\yargs
yarn
.\generate.ps1
Set-Location $psscriptroot\vscode
yarn add ts2fable --dev

Set-Location $psscriptroot