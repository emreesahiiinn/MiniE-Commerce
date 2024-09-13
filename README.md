
# MiniE-Commerce

This project is a basic e-commerce platform developed using C#. It demonstrates a simple implementation of business logic, data access, and a Web API to handle common e-commerce functionalities.

## Features

- **Business Layer**: Handles business rules and processes.
- **Data Access Layer**: Manages database operations using repositories.
- **Entities**: Defines core objects like Products, Orders, etc.
- **WebAPI**: Provides endpoints for external interaction.

## Architectural Pattern

This project follows the **Layered Architecture** pattern, which consists of the following layers:

1. **Entity Layer (Entities)**: Defines the core entities used within the application, such as Product and Order.
2. **Core Layer**: This layer contains shared logic and abstractions used across the application. It serves as the backbone for common functionality that can be reused by other layers, including interfaces, constants, and shared services.
3. **Data Access Layer (DAL)**: Responsible for handling database operations, using the repository pattern to provide a structured approach to data access.
4. **Business Layer (BL)**: Contains the business logic and rules. This layer ensures that the data handled by the DAL is processed according to the business requirements.
5. **Presentation Layer (Web API)**: This is the layer that interacts with the user via HTTP requests and handles API responses.

The separation of concerns provided by this architecture ensures modularity and makes the project easier to maintain, extend, and test.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/emreesahiiinn/MiniE-Commerce.git
   ```
2. Open the solution file in Visual Studio: `MiniECommerce.sln`
3. Build and run the project.

## Usage

The API endpoints are accessible via `localhost:5000/api/`. Some example endpoints include:
- `/api/products`: To interact with product data (CRUD operations).
- `/api/orders`: To manage orders.
