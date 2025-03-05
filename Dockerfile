# Gunakan image .NET SDK yang sesuai
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set working directory di dalam container
WORKDIR /app

# Salin file proyek (.csproj) dulu agar restore lebih cepat
COPY ["AuthAPI.csproj", "./"]

# Jalankan restore dependencies
RUN dotnet restore

# Salin semua file kode ke dalam container
COPY . .

# Build aplikasi
RUN dotnet publish -c Release -o /publish

# Gunakan runtime image yang lebih ringan untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# Perintah untuk menjalankan aplikasi
CMD ["dotnet", "AuthAPI.dll"]
