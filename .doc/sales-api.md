[Back to README](../README.md)

### Sales

#### GET /sales
- Description: Retrieve a list of sales with filtering capabilities
- Query Parameters:
  - `page` (optional): Page number for pagination (default: 1)
  - `size` (optional): Number of items per page (default: 10)
  - `order` (optional): Ordering of results (e.g., "date desc, number asc")
- Example: GET /sales?page=1&size=10&order=date desc
- Response: 
  ```json
  {
    "data": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "number": 1001,
        "date": "2024-03-15T14:30:00Z",
        "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
        "total": 249.97,
        "items": [
          {
            "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
            "quantity": 2,
            "price": 99.99,
            "subtotal": 199.98
          },
          {
            "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
            "quantity": 1,
            "price": 49.99,
            "subtotal": 49.99
          }
        ]
      }
    ],
    "totalItems": 50,
    "currentPage": 1,
    "totalPages": 5,
    "success": true,
    "message": "Sales retrieved successfully"
  }
  ```

#### POST /sales
- Description: Create a new sale
- Request Body:
  ```json
  {
    "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
    "items": [
      {
        "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
        "quantity": 2,
        "price": 99.99
      },
      {
        "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
        "quantity": 1,
        "price": 49.99
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "number": 1001,
      "date": "2024-03-15T14:30:00Z",
      "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
      "total": 249.97,
      "items": [
        {
          "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
          "quantity": 2,
          "price": 99.99,
          "subtotal": 199.98
        },
        {
          "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
          "quantity": 1,
          "price": 49.99,
          "subtotal": 49.99
        }
      ]
    },
    "success": true,
    "message": "Sale created successfully"
  }
  ```

#### GET /sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID (GUID)
- Example: GET /sales/3fa85f64-5717-4562-b3fc-2c963f66afa6
- Success Response: 
  ```json
  {
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "number": 1001,
      "date": "2024-03-15T14:30:00Z",
      "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
      "total": 249.97,
      "items": [
        {
          "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
          "quantity": 2,
          "price": 99.99,
          "subtotal": 199.98
        },
        {
          "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
          "quantity": 1,
          "price": 49.99,
          "subtotal": 49.99
        }
      ]
    },
    "success": true,
    "message": "Sale retrieved successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "Sale not found",
    "errors": ["Sale with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```

#### GET /sales/by-number/{number}
- Description: Retrieve a specific sale by its number
- Path Parameters:
  - `number`: Sale number (integer)
- Example: GET /sales/by-number/1001
- Success Response: 
  ```json
  {
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "number": 1001,
      "date": "2024-03-15T14:30:00Z",
      "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
      "total": 249.97,
      "items": [
        {
          "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
          "quantity": 2,
          "price": 99.99,
          "subtotal": 199.98
        },
        {
          "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
          "quantity": 1,
          "price": 49.99,
          "subtotal": 49.99
        }
      ]
    },
    "success": true,
    "message": "Sale retrieved successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "Sale not found",
    "errors": ["Sale with number '1001' was not found"]
  }
  ```

#### PUT /sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID (GUID)
- Example: PUT /sales/3fa85f64-5717-4562-b3fc-2c963f66afa6
- Request Body:
  ```json
  {
    "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
    "items": [
      {
        "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
        "quantity": 3,
        "price": 99.99
      },
      {
        "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
        "quantity": 2,
        "price": 49.99
      }
    ]
  }
  ```
- Success Response: 
  ```json
  {
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "number": 1001,
      "date": "2024-03-15T14:30:00Z",
      "customerId": "7bc85f64-5717-4562-b3fc-2c963f66def8",
      "total": 399.95,
      "items": [
        {
          "productId": "9ec85f64-5717-4562-b3fc-2c963f66abc1",
          "quantity": 3,
          "price": 99.99,
          "subtotal": 299.97
        },
        {
          "productId": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
          "quantity": 2,
          "price": 49.99,
          "subtotal": 99.98
        }
      ]
    },
    "success": true,
    "message": "Sale updated successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "Sale not found",
    "errors": ["Sale with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./products-api.md">Previous: Products API</a>
  <a href="./users-api.md">Next: Users API</a>
</div> 