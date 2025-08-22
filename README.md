# CloudJ - IT Solutions Marketplace

![alt text](https://s8.hostingkartinok.com/uploads/images/2019/12/d0dd8db6f14d5e873b1b650252545ab0.png)

CloudJ is a prototype-simulator of an IT solutions marketplace built with .NET Core. The platform provides a comprehensive cloud-based ecosystem for discovering, purchasing, and deploying software solutions.

## About the Application

This IT solutions marketplace prototype includes the following core services:

1. **Authentication Service** - Based on IdentityServer with support for external authentication providers
2. **Solutions Service** - Implements business logic for software solution models (CRUD operations, filtering, review system, and pricing plans)
3. **Billing Service** - Handles regular user orders (user balance top-up/update, solution purchase, and cloud access simulation)
4. **Curated Collections Service** - Manages themed collections created by the marketplace owner

## REST API

The application is built on .NET Core and implements RESTful APIs for all its services, significantly simplifying integration. The programming interface includes comprehensive Swagger documentation for easy API exploration and testing.

## Client Application

The client-side implementation uses MVC architecture for time efficiency. Future versions will transition to a Single Page Application (SPA) built with Angular for enhanced user experience.

## Scalability for User Solution Deployment

To enable automatic deployment of user products, we utilize two approaches:

1. **Custom Provider Specification** - Our proprietary specification for developing custom providers to access your own cloud infrastructure (with subsequent redirect to solution owner's services)
2. **Docker Containers** - Solutions packaged in Docker containers for deployment in our cloud infrastructure

## Directory Structure

The application is divided into 2 main solutions:

### Marketplace (CloudJ)

The main marketplace solution follows a layered architecture:

1. **Data Access Layer (DataAccessLayer)** - Code-first database implementation using Entity Framework Core
2. **Business Logic Layer (BusinessLogicLayer)** - Core services for data processing and business rule implementation
3. **API Layer** - RESTful API providing access to BLL services
4. **User Interface** - MVC-based UI for interacting with the application API
5. **Contracts** - Helper classes for data transfer between layers
6. **Infrastructure** - Extension methods for Dependency Injection configuration

### IdentityServer

A standard MVC application that contains IdentityServer, database context, and supporting API for the client application. This solution provides comprehensive user identity management capabilities including:

- User authentication and authorization
- User profile editing and management
- Email verification systems
- Password recovery mechanisms
- External authentication provider integration

## Technology Stack

- **.NET Core** - Primary framework for backend services
- **Entity Framework Core** - Object-relational mapping and database management
- **IdentityServer** - Authentication and authorization framework
- **MVC** - Model-View-Controller pattern for web interface
- **Swagger** - API documentation and testing
- **Docker** - Containerization for deployment
- **Angular** - Planned frontend framework for future SPA implementation

## Getting Started

### Prerequisites

- .NET Core SDK
- SQL Server or compatible database
- Docker (optional, for containerized deployment)

### Running the Application

1. Clone the repository
2. Navigate to the CloudJ directory
3. Restore NuGet packages: `dotnet restore`
4. Update database: `dotnet ef database update`
5. Run the application: `dotnet run`

## Architecture Overview

The application follows a clean architecture pattern with clear separation of concerns:

```
┌─────────────────┐    ┌─────────────────┐
│   CloudJ.Client │    │  IdentityServer │
│      (MVC)      │    │      (Auth)     │
└─────────────────┘    └─────────────────┘
         │                       │
         └───────────┬───────────┘
                     │
         ┌─────────────────┐
         │   CloudJ.API    │
         │   (REST API)    │
         └─────────────────┘
                     │
         ┌─────────────────┐
         │ Business Logic  │
         │     Layer       │
         └─────────────────┘
                     │
         ┌─────────────────┐
         │  Data Access    │
         │     Layer       │
         └─────────────────┘
                     │
         ┌─────────────────┐
         │    Database     │
         │  (EF Core)      │
         └─────────────────┘
```

## Demo Videos

Watch these demonstration videos to see CloudJ in action:

- [CloudJ Marketplace Demo](https://youtu.be/biZxMeNEn9k)
- [Authentication System Demo](https://youtu.be/bFSFZUFDn_c)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is available under the MIT License. See the LICENSE file for more details.
