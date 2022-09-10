#!/bin/bash
REL_DIR="release"

ZIP_DEST="$REL_DIR/Backend.zip"

OUT_DIR="bin/Release/BudgetSquirrel.Backend"

dotnet publish --output $OUT_DIR --configuration "Release" --framework "net6.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-arm --self-contained true src/BudgetSquirrel.Backend/BudgetSquirrel.Backend.csproj

[[ -d $REL_DIR ]] || mkdir $REL_DIR
cd $OUT_DIR
zip -r ../../../$ZIP_DEST *
