@ECHO OFF
SET BASEPATH=%~dp0%
SET NUGETPATH=%BASEPATH%.nuget\

rem remove installed version to get the latest
FOR /D %%p IN ("%BASEPATH%Packages\PSake*") DO rmdir "%%p" /s /q >nul 2>nul

rem ensure psake is installed
nuget install -OutputDirectory Packages PSake >nul

REM Find psake with version
for /f "tokens=*" %%F in ('dir /b /a:d "%BASEPATH%Packages\PSake*"') do call set PSAKEPATH=%BASEPATH%Packages\%%F\

powershell -NoProfile -ExecutionPolicy Bypass -Command "& '%PSAKEPATH%tools\psake\psake.ps1' %*; if ($psake.build_success -eq $false) { exit 1 } else { exit 0 }"

:END
