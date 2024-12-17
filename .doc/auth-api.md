[Back to README](../README.md)

### Authentication

#### POST /api/auth
- Description: Authenticate a user
- Request Body:
  ```json
  {
    "username": "johndoe",
    "password": "StrongP@ssw0rd"
  }
  ```
- Success Response: 
  ```json
  {
    "success": true,
    "message": "User authenticated successfully",
    "data": {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
    }
  }
  ```
- Error Response (400 Bad Request):
  ```json
  {
    "success": false,
    "message": "Validation failed",
    "errors": [
      "Username is required",
      "Password must be at least 8 characters long"
    ]
  }
  ```
- Error Response (401 Unauthorized):
  ```json
  {
    "success": false,
    "message": "Authentication failed",
    "errors": ["Invalid username or password"]
  }
  ```
- Status Codes:
  - 200: Successful authentication
  - 400: Invalid request (missing fields or validation errors)
  - 401: Unauthorized (invalid credentials)

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./users-api.md">Previous: Users API</a>
  <a href="./project-structure.md">Next: Project Structure</a>
</div>
