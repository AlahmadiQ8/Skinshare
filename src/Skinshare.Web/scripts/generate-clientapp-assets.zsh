#!/usr/bin/env zsh

pathToIndex="ClientApp/dist/index.html";
lineToStyleSheets=$(awk '/stylesheet/{ print NR; exit }' $pathToIndex );  # DANGER Super Brittle
lineToScripts=$(awk '/script/{ print NR; exit }' $pathToIndex );          # DANGER Super Brittle
pathToPartials="Pages/Shared"

styleSheets=$(sed "${lineToStyleSheets}q;d" $pathToIndex);
styleSheets=$(echo $styleSheets | sed 's/<\/head>//g' );
scripts=$(sed "${lineToScripts}q;d" $pathToIndex);
scripts=$(echo $scripts | sed 's/<\/body>//g' );

# echo $styleSheets > "$pathToPartials/_AppStyleSheetsProd.cshtml";
# echo "Created prod prartials for _AppStyleSheetsProd.cshtml";

echo $scripts > "$pathToPartials/_AppScriptsProd.cshtml"
echo "Created prod prartials for _AppScriptsProd.cshtml";
