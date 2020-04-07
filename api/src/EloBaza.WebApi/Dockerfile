FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/EloBaza.WebApi/EloBaza.WebApi.csproj", "src/EloBaza.WebApi/"]
COPY ["src/EloBaza.Infrastructure/EloBaza.Infrastructure.csproj", "src/EloBaza.Infrastructure/"]
COPY ["src/EloBaza.Application/EloBaza.Application.csproj", "src/EloBaza.Application/"]
COPY ["src/EloBaza.Domain/EloBaza.Domain.csproj", "src/EloBaza.Domain/"]
RUN dotnet restore "src/EloBaza.WebApi/EloBaza.WebApi.csproj"
COPY . .
WORKDIR "src/EloBaza.WebApi"
RUN dotnet build "EloBaza.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EloBaza.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EloBaza.WebApi.dll"]