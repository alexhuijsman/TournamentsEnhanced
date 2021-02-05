gameVersion=$1
versionedLibFolder=lib/Bannerlord/$gameVersion
activeLibFolder=lib/Bannerlord/active
rm -rf $activeLibFolder
mkdir $activeLibFolder
cp $versionedLibFolder/*.dll $activeLibFolder