FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY bin/Release4/net5.0/publish/ webAPI/

WORKDIR /webAPI

CMD ASPNETCORE_URLS=http://*:$PORT dotnet webAPI.dll