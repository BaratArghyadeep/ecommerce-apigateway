FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["APIGateWay.API/APIGateWay.API.csproj", "APIGateWay.API/"]
RUN dotnet restore "APIGateWay.API/APIGateWay.API.csproj"

COPY . .
WORKDIR "/src/APIGateWay.API"
RUN dotnet build "APIGateWay.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGateWay.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateWay.API.dll"]