# Définis le chemin de base où se trouve le dossier BottomRight
$basePath = "W:\Dev\Unity\template\Assets\animation\Knight"

# Liste des directions
$directions = "BottomLeft", "BottomRight", "TopLeft", "TopRight", "Left", "Right", "Top", "Bottom"

# Boucle à travers chaque direction et effectue la copie et le renommage
foreach ($dir in $directions) {
  Write-Host "Copying BottomRight to $dir"
  
  # Copie le dossier BottomRight en un nouveau dossier basé sur la direction actuelle
  Copy-Item "$basePath\BottomRight" "$basePath\$dir" -Recurse
  
  Write-Host "Renaming files in $dir"

  # Obtient tous les fichiers dans le nouveau dossier qui commence par BottomRight
  Get-ChildItem "$basePath\$dir\BottomRight*" | ForEach-Object {
    # Construit le nouveau nom de fichier
    $newName = $_.Name -replace "BottomRight", $dir
    
    # Renomme le fichier
    Rename-Item $_.FullName -NewName $newName
    
    Write-Host "Renamed $($_.Name) to $newName"
  }
  
  Write-Host "Finished processing $dir"
}

Write-Host "All directories processed"
