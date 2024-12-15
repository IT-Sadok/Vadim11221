FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN dotnet restore PrivateHospitals.sln

RUN dotnet build PrivateHospitals.sln --configuration Release --no-restore

RUN dotnet publish PrivateHospitals.sln --configuration Release --no-build -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PrivateHospitals.dll"]