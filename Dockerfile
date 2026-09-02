FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Build context MUST be the repository root (not ReCitiesApi.Server/).
COPY ReCitiesApi.Models/ReCitiesApi.Models.csproj ReCitiesApi.Models/
COPY ReCitiesApi.Infrastructure/ReCitiesApi.Infrastructure.csproj ReCitiesApi.Infrastructure/
COPY ReCitiesApi.Server/ReCitiesApi.Server.csproj ReCitiesApi.Server/
RUN dotnet restore ReCitiesApi.Server/ReCitiesApi.Server.csproj

COPY ReCitiesApi.Models/ ReCitiesApi.Models/
COPY ReCitiesApi.Infrastructure/ ReCitiesApi.Infrastructure/
COPY ReCitiesApi.Server/ ReCitiesApi.Server/
RUN dotnet publish ReCitiesApi.Server/ReCitiesApi.Server.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ReCitiesApi.Server.dll"]
