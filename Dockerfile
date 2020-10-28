FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app

# Copy sln
COPY ./src/CurrencyViewer.sln ./src/CurrencyViewer.sln

# Copy csproj and restore as distinct layers
COPY ./src/CurrencyViewer/*.csproj ./src/CurrencyViewer/
COPY ./src/CurrencyViewer.Application/*.csproj ./src/CurrencyViewer.Application/
COPY ./src/CurrencyViewer.Domain/*.csproj ./src/CurrencyViewer.Domain/
COPY ./src/CurrencyViewer.Infrastructure/*.csproj ./src/CurrencyViewer.Infrastructure/
COPY ./tests/CurrencyViewer.Application.Tests/*.csproj ./tests/CurrencyViewer.Application.Tests/

RUN dotnet restore ./src/CurrencyViewer.sln

# Copy everything else and build
COPY ./src/CurrencyViewer/. ./src/CurrencyViewer/
COPY ./src/CurrencyViewer.Application/. ./src/CurrencyViewer.Application/
COPY ./src/CurrencyViewer.Domain/. ./src/CurrencyViewer.Domain/
COPY ./src/CurrencyViewer.Infrastructure/. ./src/CurrencyViewer.Infrastructure/

RUN dotnet publish ./src/CurrencyViewer/CurrencyViewer.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

WORKDIR /app
COPY --from=build-env /app/out .

HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "CurrencyViewer.API.dll"]