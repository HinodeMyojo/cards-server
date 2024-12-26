#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# COPY ../../AnalyticService/StatisticService.API/Protos /src/Protos
WORKDIR /src

COPY ["CardsServer.API/CardsServer.API.csproj", "CardsServer.API/"]
COPY ["CardsServer.BLL/CardsServer.BLL.csproj", "CardsServer.BLL/"]
COPY ["CardsServer.DAL/CardsServer.DAL.csproj", "CardsServer.DAL/"]
RUN dotnet restore "./CardsServer.API/./CardsServer.API.csproj"
COPY . .
WORKDIR "/src/CardsServer.API"
RUN dotnet build "./CardsServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CardsServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardsServer.API.dll"]
