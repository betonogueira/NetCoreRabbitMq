cd ..\src
IF [%1] == [] GOTO error
@Echo Off
dotnet pack -o _build --version-suffix %1
dotnet publish --framework netcoreapp2.0 --runtime win10-x64
EXIT /B
:error
Echo version required.