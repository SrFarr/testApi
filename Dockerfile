# Gunakan image .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory
WORKDIR /app

# Salin file proyek
COPY AuthAPI.csproj .

# Periksa apakah file proyek ada
RUN ls -lah

# Restore dependencies
RUN dotnet restore --disable-parallel

# Salin semua kode
COPY . .

# Build aplikasi
RUN dotnet publish -c Release -o /publish

# Gunakan image runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# Jalankan aplikasi
CMD ["dotnet", "AuthAPI.dll"]
