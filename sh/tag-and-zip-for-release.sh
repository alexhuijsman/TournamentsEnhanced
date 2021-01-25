zipFile="$2/TournamentsEnhanced$1.zip"
folderToZip="$2/TournamentsEnhanced"
git tag $1
git push
git push origin $1
rm "$zipFile"
7z a "$zipFile" "$folderToZip"