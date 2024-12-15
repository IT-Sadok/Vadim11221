FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо рішення
COPY PrivateHospitals.sln . 

# Копіюємо всі необхідні проєктні файли
COPY PrivateHospitals.API/ ./PrivateHospitals.API/
COPY PrivateHospitals.Application/ ./PrivateHospitals.Application/
COPY PrivateHospitals.Core/ ./PrivateHospitals.Core/
COPY PrivateHospitals.Infrastructure/ ./PrivateHospitals.Infrastructure/
COPY PrivateHospitals.Migration/ ./PrivateHospitals.Migration/

# Відновлюємо залежності
RUN dotnet restore PrivateHospitals.sln

# Збираємо рішення
RUN dotnet build PrivateHospitals.sln --configuration Release --no-restore

# Публікуємо рішення
RUN dotnet publish PrivateHospitals.sln --configuration Release --no-build -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .


ENTRYPOINT ["dotnet", "PrivateHospitals.dll"]
