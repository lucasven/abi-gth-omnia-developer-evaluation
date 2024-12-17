[Back to README](../README.md)

### Users

#### POST /users
- Description: Add a new user
- Request Body:
  ```json
  {
    "email": "john.doe@example.com",
    "username": "johndoe",
    "password": "StrongP@ssw0rd"
  }
  ```
- Response: 
  ```json
  {
    "success": true,
    "message": "User created successfully",
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "email": "john.doe@example.com",
      "username": "johndoe"
    }
  }
  ```
- Status Codes:
  - 201: Created
  - 400: Bad Request (Invalid data or validation errors)

#### GET /users/{id}
- Description: Retrieve a specific user by ID
- Path Parameters:
  - `id`: User ID (GUID)
- Example: GET /users/3fa85f64-5717-4562-b3fc-2c963f66afa6
- Response: 
  ```json
  {
    "success": true,
    "message": "User retrieved successfully",
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "email": "john.doe@example.com",
      "username": "johndoe",
      "createdAt": "2024-03-15T10:30:00Z",
      "updatedAt": "2024-03-15T10:30:00Z"
    }
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "User not found",
    "errors": ["User with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```
- Status Codes:
  - 200: OK
  - 400: Bad Request (Invalid GUID format)
  - 404: Not Found

#### DELETE /users/{id}
- Description: Delete a specific user
- Path Parameters:
  - `id`: User ID (GUID)
- Example: DELETE /users/3fa85f64-5717-4562-b3fc-2c963f66afa6
- Success Response: 
  ```json
  {
    "success": true,
    "message": "User deleted successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "User not found",
    "errors": ["User with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```
- Status Codes:
  - 200: OK
  - 400: Bad Request (Invalid GUID format)
  - 404: Not Found

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./auth-api.md">Next: Auth API</a>
</div>
