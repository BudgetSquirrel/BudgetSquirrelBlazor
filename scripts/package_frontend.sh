#!/bin/bash
REL_DIR="release"

ZIP_DEST="$REL_DIR/Frontend.zip"

OUT_DIR="bin/Release/BudgetSquirrel.Frontend"

dotnet publish --output $OUT_DIR --configuration "Release" --framework "net5.0" --runtime linux-arm --self-contained true src/BudgetSquirrel.Frontend/BudgetSquirrel.Frontend.csproj

[[ -d $REL_DIR ]] || mkdir $REL_DIR
cd $OUT_DIR
zip -r ../../../$ZIP_DEST *
