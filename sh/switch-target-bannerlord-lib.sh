gameVersion=$1
versionedLibFolder=lib/Bannerlord/$gameVersion
targetLibFolder=lib/Bannerlord/target
rm -rf $targetLibFolder
mkdir -p $targetLibFolder
cp $versionedLibFolder/*.dll $targetLibFolder
echo Switched target to $gameVersion.