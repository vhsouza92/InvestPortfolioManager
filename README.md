
# InvestPortfolioManager

InvestPortfolioManager is a distributed system for managing investment portfolios. The system is divided into two main modules: Operational and Client. The Operational module handles the management of financial products, while the Client module handles the buying, selling, and querying of investments. The two modules communicate asynchronously via RabbitMQ.

## Features

### Operational Module
- Manage financial products (create, update, delete, list).
- Publish financial product events to RabbitMQ.
- Send daily email notifications to administrators about products nearing maturity.

### Client Module
- Buy and sell financial products.
- Query available financial products and transaction history.
- Consume financial product events from RabbitMQ.

## Project Structure

```
InvestPortfolioManager
│
├── Operational
│   ├── InvestPortfolioManager.Operational.API
│   ├── InvestPortfolioManager.Operational.Application
│   ├── InvestPortfolioManager.Operational.Domain
│   ├── InvestPortfolioManager.Operational.Infrastructure
│   └── Dockerfile
│
├── Client
│   ├── InvestPortfolioManager.Client.API
│   ├── InvestPortfolioManager.Client.Application
│   ├── InvestPortfolioManager.Client.Domain
│   ├── InvestPortfolioManager.Client.Infrastructure
│   └── Dockerfile
│
├── Shared
│   └── InvestPortfolioManager.Shared
│
├── sql-scripts
│   ├── CreateDatabase.sql
│   └── init-sqlserver.sh
│
├── docker-compose.yml
└── README.md
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

### Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/vhsouza92/InvestPortfolioManager.git
   cd InvestPortfolioManager
   ```

2. **Build and run the Docker containers:**

   ```bash
   docker-compose up --build
   ```

3. **Apply database:**

   The database is created and seeded with initial data through the `CreateDatabase.sql` script.
   - Ensure that the CreateDatabase.sql script is in the correct directory and configured in the docker-compose.yml.

### Usage

- **Operational API:** Access Swagger UI at `http://localhost:8081/swagger`
- **Client API:** Access Swagger UI at `http://localhost:8082/swagger`

### Project Modules

#### Operational Module

**Namespace: `InvestPortfolioManager.Operational`**

- **API:** Contains controllers and API endpoints for managing financial products and sending email notifications.
- **Application:** Contains application services implementing business logic for financial product management.
- **Domain:** Contains domain entities, repositories, and events related to financial products.
- **Infrastructure:** Contains implementations of repositories, DbContext, and RabbitMQ event publisher.

#### Client Module

**Namespace: `InvestPortfolioManager.Client`**

- **API:** Contains controllers and API endpoints for buying, selling, and querying financial products.
- **Application:** Contains application services implementing business logic for transactions.
- **Domain:** Contains domain entities, repositories, and events related to transactions.
- **Infrastructure:** Contains implementations of repositories, DbContext, and RabbitMQ event consumer.

#### Shared Module

**Namespace: `InvestPortfolioManager.Shared`**

- Contains shared definitions of events used for communication between the Operational and Client modules.


### Git Repository

- https://github.com/vhsouza92/InvestPortfolioManager.git