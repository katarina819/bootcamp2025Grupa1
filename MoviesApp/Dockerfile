# === BUILD STAGE ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiraj sve fajlove u kontejner
COPY . .

# Restore nuget paketa
RUN dotnet restore MoviesApp.WebApi/MoviesApp.WebApi.csproj

# Build + publish
RUN dotnet publish MoviesApp.WebApi/MoviesApp.WebApi.csproj -c Release -o /app/publish

# === RUNTIME STAGE ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Kopiraj iz build stage-a
COPY --from=build /app/publish .

# Start aplikacije
ENTRYPOINT ["dotnet", "MoviesApp.WebApi.dll"]
