# Developer Evaluation Project

## Project Requirements

This project implements a sales management API with the following features:

- Complete CRUD operations for sales records
- Business rules for quantity-based discounts
- Event publishing capabilities (optional)

### Business Rules

* 4+ identical items: 10% discount
* 10-20 identical items: 20% discount
* Maximum limit: 20 items per product
* No discounts for quantities below 4 items

## Getting Started

### Prerequisites

- Docker and Docker Compose
- .NET 8.0 SDK (for local development)

### Running with Docker

1. Clone the repository:
```bash
git clone https://github.com/lucasven/abi-gth-omnia-developer-evaluation
cd abi-gth-omnia-developer-evaluation
```

2. Start the application:
```bash
docker-compose up -d
```

The API will be available at `http://localhost:8080`

### Running Locally

1. Navigate to the backend directory:
```bash
cd src/Ambev.DeveloperEvaluation.WebApi
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

## API Documentation

The API documentation is available at:
- Swagger UI: `http://localhost:8080/swagger`
- OpenAPI JSON: `http://localhost:8080/swagger/v1/swagger.json`

I Updated all these API Documentation to fit the current API Responses.
### Detailed API Documentation
- [Authentication](.doc/auth-api.md)
- [Products](.doc/products-api.md)
- [Sales](.doc/sales-api.md)
- [Users](.doc/users-api.md)

## Project Structure

```
src/
├── Ambev.DeveloperEvaluation.Domain/     # Domain layer
├── Ambev.DeveloperEvaluation.WebApi/     # API layer
└── Ambev.DeveloperEvaluation.IoC/        # Dependency Injection
```

## Testing

To run the tests:
```bash
dotnet test
```

## Original Requirements

<details>
<summary>Click to expand</summary>

The original requirements include implementing a sales management API with the following features:
- Sale number
- Date when the sale was made
- Customer information
- Total sale amount
- Branch information
- Products, quantities, and prices
- Discounts
- Cancellation status
- Event publishing capabilities

</details>