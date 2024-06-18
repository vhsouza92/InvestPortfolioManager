# Use the official .NET image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Operational/InvestPortfolioManager.Operational.API/InvestPortfolioManager.Operational.API.csproj", "Operational/InvestPortfolioManager.Operational.API/"]
COPY ["Operational/InvestPortfolioManager.Operational.Application/InvestPortfolioManager.Operational.Application.csproj", "Operational/InvestPortfolioManager.Operational.Application/"]
COPY ["Operational/InvestPortfolioManager.Operational.Domain/InvestPortfolioManager.Operational.Domain.csproj", "Operational/InvestPortfolioManager.Operational.Domain/"]
COPY ["Operational/InvestPortfolioManager.Operational.Infrastructure/InvestPortfolioManager.Operational.Infrastructure.csproj", "Operational/InvestPortfolioManager.Operational.Infrastructure/"]
COPY ["Shared/InvestPortfolioManager.Shared/InvestPortfolioManager.Shared.csproj", "Shared/InvestPortfolioManager.Shared/"]
RUN dotnet restore "Operational/InvestPortfolioManager.Operational.API/InvestPortfolioManager.Operational.API.csproj"
COPY . .
WORKDIR "/src/Operational/InvestPortfolioManager.Operational.API"
RUN dotnet build "InvestPortfolioManager.Operational.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestPortfolioManager.Operational.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestPortfolioManager.Operational.API.dll"]
