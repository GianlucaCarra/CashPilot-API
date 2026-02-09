FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CashPilot.sln .

COPY src/CashPilot.API/CashPilot.API.csproj src/CashPilot.API/
COPY src/CashPilot.Application/CashPilot.Application.csproj src/CashPilot.Application/
COPY src/CashPilot.Domain/CashPilot.Domain.csproj src/CashPilot.Domain/
COPY src/CashPilot.Infrastructure/CashPilot.Infrastructure.csproj src/CashPilot.Infrastructure/
COPY tests/CashPilot.Application.Tests/CashPilot.Application.Tests.csproj tests/CashPilot.Application.Tests/

RUN dotnet restore

COPY . .

RUN dotnet publish src/CashPilot.API/CashPilot.API.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CashPilot.API.dll"]
