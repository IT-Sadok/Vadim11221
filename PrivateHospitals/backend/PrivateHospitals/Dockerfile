FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо всю папку backend/PrivateHospitals
COPY bPrivateHospitals/backend/PrivateHospitals .

# Відновлюємо залежності
RUN dotnet restore PrivateHospitals.sln

# Збираємо рішення
RUN dotnet build PrivateHospitals.sln --configuration Release --no-restore

# Публікуємо проєкт
RUN dotnet publish PrivateHospitals.sln --configuration Release --no-build -o /app/publish

# Запускаємо на базовому образі ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Вказуємо точку входу
ENTRYPOINT ["dotnet", "PrivateHospitals.API.dll"]
