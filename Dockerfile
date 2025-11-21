# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el proyecto
COPY . .

# Restaurar dependencias
RUN dotnet restore "./CommunityControl.API/CommunityControl.API.csproj"

# Publicar en modo Release
RUN dotnet publish "./CommunityControl.API/CommunityControl.API.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar publicación desde la etapa anterior
COPY --from=build /app/publish .

# Render define $PORT, así que lo usamos
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

# Iniciar API
ENTRYPOINT ["dotnet", "CommunityControl.API.dll"]
