#!/bin/bash
ZIP_DEST=$1

OUT_DIR="bin/Release/BudgetSquirrel.Backend"

dotnet publish --output $OUT_DIR --configuration "Release" --framework "net5.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-arm --self-contained false src/BudgetSquirrel.Backend/BudgetSquirrel.Backend.csproj
cd $OUT_DIR
zip -r $ZIP_DEST *
