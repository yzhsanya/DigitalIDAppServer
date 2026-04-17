FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY GovDigitalApp.sln .
COPY src/GovDigitalApp.API/GovDigitalApp.API.csproj src/GovDigitalApp.API/
COPY src/GovDigitalApp.Application/GovDigitalApp.Application.csproj src/GovDigitalApp.Application/
COPY src/GovDigitalApp.Domain/GovDigitalApp.Domain.csproj src/GovDigitalApp.Domain/
COPY src/GovDigitalApp.Infrastructure/GovDigitalApp.Infrastructure.csproj src/GovDigitalApp.Infrastructure/
COPY tests/GovDigitalApp.IntegrationTests/GovDigitalApp.IntegrationTests.csproj tests/GovDigitalApp.IntegrationTests/

RUN dotnet restore src/GovDigitalApp.API/GovDigitalApp.API.csproj

COPY . .

RUN dotnet publish src/GovDigitalApp.API/GovDigitalApp.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GovDigitalApp.API.dll"]
