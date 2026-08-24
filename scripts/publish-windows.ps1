$ErrorActionPreference = "Stop"
$Root = Split-Path -Parent $PSScriptRoot
Set-Location $Root
dotnet publish src/EngineeringCalculator.Web -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o artifacts/win-x64
Write-Host "Готово: $Root\artifacts\win-x64" -ForegroundColor Green
