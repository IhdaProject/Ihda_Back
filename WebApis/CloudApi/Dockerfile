FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApis/CloudApi/CloudApi.csproj", "WebApis/CloudApi/"]
COPY ["Domain/Entity/Entity.csproj", "Domain/Entity/"]
COPY ["Domain/WebCore/WebCore.csproj", "Domain/WebCore/"]
COPY ["Infrastructures/AuthenticationBroker/AuthenticationBroker.csproj", "Infrastructures/AuthenticationBroker/"]
COPY ["Infrastructures/CacheBroker/CacheBroker.csproj", "Infrastructures/CacheBroker/"]
COPY ["Infrastructures/DatabaseBroker/DatabaseBroker.csproj", "Infrastructures/DatabaseBroker/"]
COPY ["Infrastructures/MinIoBroker/MinIoBroker.csproj", "Infrastructures/MinIoBroker/"]
RUN dotnet restore "WebApis/CloudApi/CloudApi.csproj"
COPY . .
WORKDIR "/src/WebApis/CloudApi"
RUN dotnet build "./CloudApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CloudApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudApi.dll"]
