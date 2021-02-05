zipFile="$2/Modules/TournamentsEnhanced$1.zip"
folderToZip="$2/Modules/TournamentsEnhanced"
git tag $1 --force
git push
git push origin $1 --force
rm "$zipFile"
7z a "$zipFile" "$folderToZip"