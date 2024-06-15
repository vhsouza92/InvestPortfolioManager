# InvestPortfolioManager

InvestPortfolioManager is a backend system for managing investment portfolios. It allows users to manage financial products, buy, sell, and track investments. The system is built using .NET 8, following Domain-Driven Design (DDD) and Clean Architecture principles. It includes support for high-volume requests and ensures low response times. The project includes Docker support for easy deployment.

## Features

- Manage financial products
- Daily email notifications for administrators about products nearing maturity
- Buy and sell financial products
- Track investment transactions and generate statements
- High performance for product queries and statements

## Technologies

- .NET 8
- Entity Framework Core
- RabbitMQ
- SQL Server
- Docker

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker and Docker Compose

### Setup and Running the Application

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/InvestPortfolioManager.git
    cd InvestPortfolioManager
    ```

2. **Build and run the Docker containers:**

    ```bash
    docker-compose up --build
    ```

3. **Apply Entity Framework Core migrations:**

    ```bash
    docker exec -it investportfoliomanager-api /bin/bash
    dotnet ef database update --project ../InvestPortfolioManager.Infrastructure
    ```

4. **Access the API:**

    The API should be accessible at `http://localhost:8080`.

### Using the API

- **Swagger UI:**

    You can use Swagger UI to interact with the API. Open your browser and go to `http://localhost:8080/swagger`.

## Folder Structure

- **InvestPortfolioManager.API**: Contains the API project.
- **InvestPortfolioManager.Application**: Contains application services and DTOs.
- **InvestPortfolioManager.Domain**: Contains domain entities and repositories interfaces.
- **InvestPortfolioManager.Infrastructure**: Contains data access implementations and DbContext.
- **sql-scripts**: Contains SQL scripts for database initialization.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

