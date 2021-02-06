gameVersion=$1
versionedLibFolder=lib/Bannerlord/$gameVersion
targetLibFolder=lib/Bannerlord/target
rm -rf $targetLibFolder
mkdir $targetLibFolder
cp $versionedLibFolder/*.dll $targetLibFolder