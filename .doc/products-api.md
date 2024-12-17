[Back to README](../README.md)

### Products

#### GET /products
- Description: Retrieve a list of all products
- Query Parameters:
  - `page` (optional): Page number for pagination (default: 1)
  - `size` (optional): Number of items per page (default: 10)
  - `order` (optional): Ordering of results (e.g., "price desc, title asc")
- Example: GET /products?page=1&size=10&order=price desc
- Response: 
  ```json
  {
    "data": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "title": "Smartphone XYZ Pro",
        "price": 999.99,
        "description": "Latest model smartphone with 6.7-inch display and 5G capability",
        "category": "Electronics",
        "image": "https://example.com/images/smartphone-xyz-pro.jpg",
        "rating": {
          "rate": 4.5,
          "count": 127
        }
      },
      {
        "id": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
        "title": "Wireless Headphones",
        "price": 199.99,
        "description": "Premium noise-canceling wireless headphones",
        "category": "Electronics",
        "image": "https://example.com/images/wireless-headphones.jpg",
        "rating": {
          "rate": 4.8,
          "count": 89
        }
      }
    ],
    "totalItems": 50,
    "currentPage": 1,
    "totalPages": 5,
    "success": true,
    "message": "Products retrieved successfully"
  }
  ```

#### POST /products
- Description: Add a new product
- Request Body:
  ```json
  {
    "title": "Smart Watch Series X",
    "price": 299.99,
    "description": "Advanced fitness tracking with heart rate monitoring",
    "category": "Electronics",
    "image": "https://example.com/images/smartwatch-x.jpg",
    "rating": {
      "rate": 0,
      "count": 0
    }
  }
  ```
- Success Response: 
  ```json
  {
    "data": {
      "id": "7ec85f64-5717-4562-b3fc-2c963f66def8",
      "title": "Smart Watch Series X",
      "price": 299.99,
      "description": "Advanced fitness tracking with heart rate monitoring",
      "category": "Electronics",
      "image": "https://example.com/images/smartwatch-x.jpg",
      "rating": {
        "rate": 0,
        "count": 0
      }
    },
    "success": true,
    "message": "Product created successfully"
  }
  ```

#### GET /products/{id}
- Description: Retrieve a specific product by ID
- Path Parameters:
  - `id`: Product ID (GUID)
- Example: GET /products/3fa85f64-5717-4562-b3fc-2c963f66afa6
- Success Response: 
  ```json
  {
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "title": "Smartphone XYZ Pro",
      "price": 999.99,
      "description": "Latest model smartphone with 6.7-inch display and 5G capability",
      "category": "Electronics",
      "image": "https://example.com/images/smartphone-xyz-pro.jpg",
      "rating": {
        "rate": 4.5,
        "count": 127
      }
    },
    "success": true,
    "message": "Product retrieved successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "Product not found",
    "errors": ["Product with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```

#### PUT /products
- Description: Update a specific product
- Request Body:
  ```json
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Smartphone XYZ Pro Max",
    "price": 1099.99,
    "description": "Latest model smartphone with 6.7-inch display, 5G capability, and enhanced camera",
    "category": "Electronics",
    "image": "https://example.com/images/smartphone-xyz-pro-max.jpg",
    "rating": {
      "rate": 4.5,
      "count": 127
    }
  }
  ```
- Success Response: 
  ```json
  {
    "data": {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "title": "Smartphone XYZ Pro Max",
      "price": 1099.99,
      "description": "Latest model smartphone with 6.7-inch display, 5G capability, and enhanced camera",
      "category": "Electronics",
      "image": "https://example.com/images/smartphone-xyz-pro-max.jpg",
      "rating": {
        "rate": 4.5,
        "count": 127
      }
    },
    "success": true,
    "message": "Product updated successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "Product not found",
    "errors": ["Product with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```

#### DELETE /products/{id}
- Description: Delete a specific product
- Path Parameters:
  - `id`: Product ID (GUID)
- Example: DELETE /products/3fa85f64-5717-4562-b3fc-2c963f66afa6
- Success Response: 
  ```json
  {
    "data": {
      "success": true
    },
    "success": true,
    "message": "Product deleted successfully"
  }
  ```
- Error Response (404 Not Found):
  ```json
  {
    "success": false,
    "message": "Product not found",
    "errors": ["Product with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found"]
  }
  ```

#### GET /products/categories
- Description: Retrieve all product categories
- Example: GET /products/categories
- Success Response: 
  ```json
  {
    "data": [
      "Electronics",
      "Clothing",
      "Books",
      "Home & Garden",
      "Sports & Outdoors"
    ],
    "success": true,
    "message": "Product categories retrieved successfully"
  }
  ```

#### GET /products/category/{category}
- Description: Retrieve products in a specific category
- Path Parameters:
  - `category`: Category name
- Query Parameters:
  - `page` (optional): Page number for pagination (default: 1)
  - `size` (optional): Number of items per page (default: 10)
  - `order` (optional): Ordering of results (e.g., "price desc, title asc")
- Example: GET /products/category/Electronics?page=1&size=10&order=price desc
- Success Response: 
  ```json
  {
    "data": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "title": "Smartphone XYZ Pro",
        "price": 999.99,
        "description": "Latest model smartphone with 6.7-inch display and 5G capability",
        "category": "Electronics",
        "image": "https://example.com/images/smartphone-xyz-pro.jpg",
        "rating": {
          "rate": 4.5,
          "count": 127
        }
      },
      {
        "id": "5dc85f64-5717-4562-b3fc-2c963f66bcd2",
        "title": "Wireless Headphones",
        "price": 199.99,
        "description": "Premium noise-canceling wireless headphones",
        "category": "Electronics",
        "image": "https://example.com/images/wireless-headphones.jpg",
        "rating": {
          "rate": 4.8,
          "count": 89
        }
      }
    ],
    "totalItems": 15,
    "currentPage": 1,
    "totalPages": 2,
    "success": true,
    "message": "Products retrieved successfully"
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
  <a href="./carts-api.md">Next: Carts API</a>
</div>