FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MailService/EloBaza.MailService/EloBaza.MailService.csproj", "MailService/EloBaza.MailService/"]
RUN dotnet restore "MailService/EloBaza.MailService/EloBaza.MailService.csproj"
COPY . .
WORKDIR "/src/MailService/EloBaza.MailService"
RUN dotnet build "EloBaza.MailService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EloBaza.MailService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EloBaza.MailService.dll"]