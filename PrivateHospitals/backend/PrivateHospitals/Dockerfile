FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо рішення
COPY PrivateHospitals/backend/PrivateHospitals/PrivateHospitals.sln . 

# Відновлюємо залежності
RUN dotnet restore PrivateHospitals/backend/PrivateHospitals/PrivateHospitals.sln 

# Збираємо рішення
RUN dotnet build PrivateHospitals/backend/PrivateHospitals/PrivateHospitals.sln  --configuration Release --no-restore

# Публікуємо рішення
RUN dotnet publish PrivateHospitals/backend/PrivateHospitals/PrivateHospitals.sln  --configuration Release --no-build -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .


ENTRYPOINT ["dotnet", "PrivateHospitals.dll"]
