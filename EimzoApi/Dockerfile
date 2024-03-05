FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN ["ls", "-la"]
RUN ["ls", "-la", "../"]
RUN dotnet nuget add source http://192.168.122.25:2000/v3/index.json -n LocalBaget
RUN dotnet restore "EimzoApi/EimzoApi.csproj"
COPY . .
WORKDIR "/src/EimzoApi"
RUN dotnet build "EimzoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EimzoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EimzoApi.dll"]
