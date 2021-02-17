gameVersion=$(grep -o -a -m 1 -h -r "e[[:digit:]]\.[[:digit:]]\.[[:digit:]]" "$PATH_TO_BANNERLORD/Modules/Sandbox/SubModule.xml")
versionedFolder=lib/Bannerlord/$gameVersion
rm -rf $versionedFolder
mkdir -p $versionedFolder
cp "$PATH_TO_BANNERLORD"/bin/Win64_Shipping_Client/*.dll $versionedFolder
echo Copied $gameVersion to local cache.
