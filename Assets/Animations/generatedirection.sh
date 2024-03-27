@echo off
setlocal EnableDelayedExpansion

REM Définis le chemin du dossier contenant les sous-dossiers
set "baseFolderPath=W:\Dev\Unity\template\Assets\animation\Knight"

REM Définis la partie du nom de fichier à supprimer
set "partToRemove=BottomRight"

REM Parcours tous les sous-dossiers et fichiers
for /d %%d in ("%baseFolderPath%\*") do (
    echo Processing: "%%d"
    pushd "%%d"
    for %%f in (*%partToRemove%*) do (
        set "newFileName=%%f"
        set "newFileName=!newFileName:%partToRemove%=!"
        ren "%%f" "!newFileName!"
        echo Renamed "%%f" to "!newFileName!"
    )
    popd
)

echo All files have been processed.
pause
