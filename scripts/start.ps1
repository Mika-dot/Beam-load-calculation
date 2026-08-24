$ErrorActionPreference = "Stop"
$Root = Split-Path -Parent $PSScriptRoot
Set-Location $Root

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "Не найден .NET 8 SDK. Установите: https://dotnet.microsoft.com/download/dotnet/8.0"
}

dotnet restore EngineeringCalculator.sln
dotnet run --project src/EngineeringCalculator.Web --urls "http://127.0.0.1:5080"
