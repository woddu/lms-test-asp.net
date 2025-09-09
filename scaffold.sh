#!/bin/bash
dotnet aspnet-codegenerator controller \
  -name "$1" \
  -m "$2" \
  -dc ApplicationDbContext \
  --relativeFolderPath Controllers \
  --useDefaultLayout \
  --referenceScriptLibraries
