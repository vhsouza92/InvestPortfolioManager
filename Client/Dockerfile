# Use the official .NET image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Client/InvestPortfolioManager.Client.API/InvestPortfolioManager.Client.API.csproj", "Client/InvestPortfolioManager.Client.API/"]
COPY ["Client/InvestPortfolioManager.Client.Application/InvestPortfolioManager.Client.Application.csproj", "Client/InvestPortfolioManager.Client.Application/"]
COPY ["Client/InvestPortfolioManager.Client.Domain/InvestPortfolioManager.Client.Domain.csproj", "Client/InvestPortfolioManager.Client.Domain/"]
COPY ["Client/InvestPortfolioManager.Client.Infrastructure/InvestPortfolioManager.Client.Infrastructure.csproj", "Client/InvestPortfolioManager.Client.Infrastructure/"]
COPY ["Shared/InvestPortfolioManager.Shared/InvestPortfolioManager.Shared.csproj", "Shared/InvestPortfolioManager.Shared/"]
RUN dotnet restore "Client/InvestPortfolioManager.Client.API/InvestPortfolioManager.Client.API.csproj"
COPY . .
WORKDIR "/src/Client/InvestPortfolioManager.Client.API"
RUN dotnet build "InvestPortfolioManager.Client.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestPortfolioManager.Client.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestPortfolioManager.Client.API.dll"]
