#!/usr/bin/env sh
set -eu
SCRIPT_DIR=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
cd "$SCRIPT_DIR/.."
command -v dotnet >/dev/null 2>&1 || { echo ".NET 8 SDK не найден" >&2; exit 1; }
dotnet restore EngineeringCalculator.sln
exec dotnet run --project src/EngineeringCalculator.Web --urls "http://127.0.0.1:5080"
