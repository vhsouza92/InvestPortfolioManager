# Use the official .NET image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["InvestPortfolioManager.API/InvestPortfolioManager.API.csproj", "InvestPortfolioManager.API/"]
COPY ["InvestPortfolioManager.Application/InvestPortfolioManager.Application.csproj", "InvestPortfolioManager.Application/"]
COPY ["InvestPortfolioManager.Domain/InvestPortfolioManager.Domain.csproj", "InvestPortfolioManager.Domain/"]
COPY ["InvestPortfolioManager.Infrastructure/InvestPortfolioManager.Infrastructure.csproj", "InvestPortfolioManager.Infrastructure/"]
RUN dotnet restore "InvestPortfolioManager.API/InvestPortfolioManager.API.csproj"
COPY . .
WORKDIR "/src/InvestPortfolioManager.API"
RUN dotnet build "InvestPortfolioManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestPortfolioManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestPortfolioManager.API.dll"]
