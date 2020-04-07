FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/EloBaza.MigrationTool/EloBaza.MigrationTool.csproj", "src/EloBaza.MigrationTool/"]
COPY ["src/EloBaza.Infrastructure/EloBaza.Infrastructure.csproj", "src/EloBaza.Infrastructure/"]
COPY ["src/EloBaza.Application/EloBaza.Application.csproj", "src/EloBaza.Application/"]
COPY ["src/EloBaza.Domain/EloBaza.Domain.csproj", "src/EloBaza.Domain/"]
RUN dotnet restore "src/EloBaza.MigrationTool/EloBaza.MigrationTool.csproj"
COPY . .
WORKDIR "src/EloBaza.MigrationTool"
RUN dotnet build "EloBaza.MigrationTool.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EloBaza.MigrationTool.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EloBaza.MigrationTool.dll"]