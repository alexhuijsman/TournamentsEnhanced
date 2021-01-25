modVersion=$1
gameVersion=${modVersion%.*}
outDir=$2
sed 's,$(modVersion),'"$modVersion"',g;s,$(gameVersion),'"$gameVersion"',g' SubModule.xml > "${outDir}SubModule.xml"