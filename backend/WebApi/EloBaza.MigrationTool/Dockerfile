FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY ["WebApi/EloBaza.MigrationTool/EloBaza.MigrationTool.csproj", "WebApi/EloBaza.MigrationTool/"]
COPY ["WebApi/EloBaza.Infrastructure/EloBaza.Infrastructure.csproj", "WebApi/EloBaza.Infrastructure/"]
COPY ["WebApi/EloBaza.Application/EloBaza.Application.csproj", "WebApi/EloBaza.Application/"]
COPY ["WebApi/EloBaza.Domain/EloBaza.Domain.csproj", "WebApi/EloBaza.Domain/"]
RUN dotnet restore "WebApi/EloBaza.MigrationTool/EloBaza.MigrationTool.csproj"

COPY . .

WORKDIR "WebApi/EloBaza.MigrationTool"

RUN dotnet build "EloBaza.MigrationTool.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EloBaza.MigrationTool.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EloBaza.MigrationTool.dll"]