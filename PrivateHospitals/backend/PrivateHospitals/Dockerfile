# Використовуємо .NET SDK для збірки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо всі файли для збірки
COPY . .

# Відновлюємо залежності
RUN dotnet restore PrivateHospitals.sln

# Збираємо рішення
RUN dotnet build PrivateHospitals.sln --configuration Release --no-restore

# Публікуємо тільки API проект
RUN dotnet publish PrivateHospitals.API/PrivateHospitals.API.csproj --configuration Release --no-build -o /app/publish

# Використовуємо ASP.NET Runtime для запуску
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копіюємо результати публікації
COPY --from=build /app/publish .

# Вказуємо вхідну точку на API проект
ENTRYPOINT ["dotnet", "PrivateHospitals.API.dll"]
