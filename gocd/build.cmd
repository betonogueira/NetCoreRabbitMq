cd ..\src
git clean -xfd
dotnet restore
dotnet build -c release