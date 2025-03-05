# Gunakan image .NET SDK untuk build aplikasi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Salin file proyek (.csproj) agar restore lebih cepat
COPY ["AuthAPI.csproj", "./"]
RUN dotnet restore

# Salin semua file ke dalam container
COPY . .
RUN dotnet build -c Release -o /app/build

# Publish aplikasi
RUN dotnet publish -c Release -o /app/publish

# Gunakan image runtime untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthAPI.dll"]
