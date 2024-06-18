# Dockerfile for InvestPortfolioManager.Notification

# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copy necessary files
COPY . .

# Copy the appsettings.json
COPY Notification/InvestPortfolioManager.Notification/appsettings.json .

# Build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Notification/InvestPortfolioManager.Notification/InvestPortfolioManager.Notification.csproj", "Notification/InvestPortfolioManager.Notification/"]
RUN dotnet restore "Notification/InvestPortfolioManager.Notification/InvestPortfolioManager.Notification.csproj"
COPY . .
WORKDIR "/src/Notification/InvestPortfolioManager.Notification"
RUN dotnet build "InvestPortfolioManager.Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestPortfolioManager.Notification.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestPortfolioManager.Notification.dll"]
