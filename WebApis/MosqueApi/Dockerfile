FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApis/MosqueApi/MosqueApi.csproj", "WebApis/MosqueApi/"]
COPY ["Domain/WebCore/WebCore.csproj", "Domain/WebCore/"]
COPY ["Infrastructures/AuthenticationBroker/AuthenticationBroker.csproj", "Infrastructures/AuthenticationBroker/"]
COPY ["Domain/Entity/Entity.csproj", "Domain/Entity/"]
COPY ["Infrastructures/CacheBroker/CacheBroker.csproj", "Infrastructures/CacheBroker/"]
COPY ["Infrastructures/DatabaseBroker/DatabaseBroker.csproj", "Infrastructures/DatabaseBroker/"]
COPY ["Infrastructures/MinIoBroker/MinIoBroker.csproj", "Infrastructures/MinIoBroker/"]
COPY ["Services/MosqueService/MosqueService.csproj", "Services/MosqueService/"]
RUN dotnet restore "WebApis/MosqueApi/MosqueApi.csproj"
COPY . .
WORKDIR "/src/WebApis/MosqueApi"
RUN dotnet build "./MosqueApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MosqueApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MosqueApi.dll"]
