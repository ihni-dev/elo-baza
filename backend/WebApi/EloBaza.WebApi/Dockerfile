FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY ["WebApi/EloBaza.WebApi/EloBaza.WebApi.csproj", "WebApi/EloBaza.WebApi/"]
COPY ["WebApi/EloBaza.Infrastructure/EloBaza.Infrastructure.csproj", "WebApi/EloBaza.Infrastructure/"]
COPY ["WebApi/EloBaza.ServiceBusListener/EloBaza.ServiceBusListener.csproj", "WebApi/EloBaza.ServiceBusListener/"]
COPY ["WebApi/EloBaza.Application/EloBaza.Application.csproj", "WebApi/EloBaza.Application/"]
COPY ["WebApi/EloBaza.Domain/EloBaza.Domain.csproj", "WebApi/EloBaza.Domain/"]
RUN dotnet restore "WebApi/EloBaza.WebApi/EloBaza.WebApi.csproj"

COPY . .

WORKDIR "WebApi/EloBaza.WebApi"

RUN dotnet build "EloBaza.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EloBaza.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EloBaza.WebApi.dll"]