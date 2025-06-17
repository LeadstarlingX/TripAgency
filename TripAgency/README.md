# TripAgency - Car Rental & Trip Booking System

A comprehensive car rental and trip booking system built with .NET 8, following Clean Architecture principles.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Background Services](#background-services)
- [Database Schema](#database-schema)
- [Deployment](#deployment)
- [Contributing](#contributing)

## 🎯 Overview

TripAgency is a full-featured car rental and trip booking platform that allows customers to:
- Rent cars with or without drivers
- Book guided trips and tours
- Manage payments and bookings
- Track booking status and history

The system includes background services for automated booking management and payment verification.

## 🏗️ Architecture

This project follows **Clean Architecture** principles with the following layers:

```
TripAgency/
├── Domain/                 # Core business logic and entities
├── Application/           # Application services and DTOs
├── Infrastructure/        # Data access, external services
├── API/                  # Web API controllers
└── BlazorPresentation/   # Blazor UI components
```

### Architecture Layers:

1. **Domain Layer**: Contains business entities, enums, and core business rules
2. **Application Layer**: Contains application services, DTOs, and business logic
3. **Infrastructure Layer**: Contains data access, repositories, and external integrations
4. **Presentation Layer**: Contains API controllers and UI components

## ✨ Features

### Core Features
- **Car Rental Management**: Complete car rental system with availability tracking
- **Trip Booking**: Guided tour and trip booking capabilities
- **Payment Processing**: Integrated payment system with transaction tracking
- **User Management**: Customer and employee account management
- **Booking Management**: Comprehensive booking lifecycle management

### Advanced Features
- **Background Services**: Automated booking cancellation for incomplete payments
- **Real-time Availability**: Dynamic car availability updates
- **Multi-role Authentication**: Admin, Employee, Customer, and User roles
- **Comprehensive Logging**: Detailed logging for monitoring and debugging

## 🛠️ Technology Stack

- **.NET 8**: Latest .NET framework
- **Entity Framework Core**: ORM for data access
- **SQL Server**: Primary database
- **ASP.NET Core Identity**: Authentication and authorization
- **AutoMapper**: Object mapping
- **Swagger/OpenAPI**: API documentation
- **Blazor**: Web UI framework
- **Background Services**: Automated task processing

## 📁 Project Structure

```
TripAgency/
├── Domain/
│   ├── Context/                    # Database contexts
│   ├── Entities/
│   │   ├── ApplicationEntities/    # Business entities
│   │   └── IdentityEntities/       # Identity-related entities
│   ├── Enum/                       # Enumerations
│   └── Common/                     # Base classes and common types
├── Application/
│   ├── DTOs/                       # Data Transfer Objects
│   ├── IApplicationServices/       # Service interfaces
│   ├── IReositosy/                 # Repository interfaces
│   ├── IUnitOfWork/                # Unit of Work interface
│   ├── Mapping/                    # AutoMapper profiles
│   ├── Filter/                     # Filter classes
│   └── Common/                     # Common application types
├── Infrastructure/
│   ├── Context/                    # DbContext implementations
│   ├── Repository/                 # Repository implementations
│   ├── ApplicationServices/        # Service implementations
│   ├── BackgroundServices/         # Background service implementations
│   ├── Seeds/                      # Database seeding
│   ├── Migrations/                 # EF Core migrations
│   └── DependencyInjection.cs      # DI configuration
├── API/
│   ├── Controllers/                # API controllers
│   ├── Middleware/                 # Custom middleware
│   ├── Areas/                      # API areas
│   └── Program.cs                  # API startup
└── BlazorPresentation/             # Blazor UI components
```

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (Local or Azure)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd TripAgency
   ```

2. **Configure Database**
   - Update connection strings in `API/appsettings.json`
   - Run Entity Framework migrations:
   ```bash
   cd TripAgency/API
   dotnet ef database update
   ```

3. **Run the Application**
   ```bash
   cd TripAgency/API
   dotnet run
   ```

4. **Access the Application**
   - API: `https://localhost:7001`
   - Swagger UI: `https://localhost:7001/swagger`
   - Blazor UI: `https://localhost:7002`

### Default Admin Account

- **Email**: admin@tripagency.com
- **Password**: Admin123!
- **Role**: Administrator

## 📚 API Documentation

### Base URL
- **Development**: `https://localhost:7001`
- **Production**: `https://api.tripagency.com`

### Authentication
The API uses JWT (JSON Web Tokens) for authentication. Include the token in the Authorization header:
```
Authorization: Bearer <your-jwt-token>
```

### Key Endpoints

#### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `POST /api/auth/refresh-token` - Refresh access token

#### Car Management
- `GET /api/cars` - Get all cars
- `GET /api/cars/{id}` - Get car by ID
- `POST /api/cars` - Create new car
- `PUT /api/cars/{id}` - Update car
- `DELETE /api/cars/{id}` - Delete car

#### Booking Management
- `GET /api/bookings` - Get all bookings
- `GET /api/bookings/{id}` - Get booking by ID
- `POST /api/bookings` - Create new booking
- `PUT /api/bookings/{id}` - Update booking
- `DELETE /api/bookings/{id}` - Delete booking

#### Payment Management
- `GET /api/payments` - Get all payments
- `POST /api/payments` - Create payment
- `PUT /api/payments/{id}` - Update payment

### Example API Usage

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

#### Get Cars
```http
GET /api/cars?category=VIP&available=true
Authorization: Bearer <token>
```

#### Create Booking
```http
POST /api/bookings
Authorization: Bearer <token>
Content-Type: application/json

{
  "customerId": 123,
  "bookingType": "CarBooking",
  "startDateTime": "2025-01-25T10:00:00Z",
  "endDateTime": "2025-01-26T10:00:00Z",
  "numOfPassengers": 2
}
```

### Error Handling
The API returns standard HTTP status codes:
- `200 OK`: Request successful
- `201 Created`: Resource created
- `400 Bad Request`: Invalid request
- `401 Unauthorized`: Authentication required
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

## 🔄 Background Services

### BookingPaymentCheckService

**Location**: `Infrastructure/BackgroundServices/BookingPaymentCheckService.cs`

**Purpose**: Automatically monitors and cancels bookings with incomplete payments before they start.

#### How It Works

1. **Execution Frequency**: Runs every 10 minutes
2. **Monitoring Window**: Checks bookings within 10 minutes of their start time
3. **Payment Verification**: Compares `AmountDue` vs `AmountPaid` for each booking
4. **Automatic Actions**: 
   - Cancels bookings with incomplete payments
   - Sets car status to `Available` for cancelled car bookings
   - Logs all actions for monitoring

#### Business Logic

```csharp
// Check if booking is within 10 minutes of start time
var timeUntilStart = booking.StartDateTime - currentTime;
var tenMinutes = TimeSpan.FromMinutes(10);

if (timeUntilStart <= tenMinutes)
{
    // Verify payment completion
    var totalAmountDue = booking.Payments.Sum(p => p.AmountDue);
    var totalAmountPaid = booking.Payments.Sum(p => p.AmountPaid);
    
    if (totalAmountDue != totalAmountPaid)
    {
        // Cancel booking and free up resources
        booking.Status = BookingStatusEnum.Cancelled;
        
        // Make car available if it's a car booking
        if (booking.BookingType == BookingTypes.CarBooking)
        {
            car.CarStatus = CarStatusEnum.Available;
        }
    }
}
```

#### Configuration

**Registration**: The service is registered in `Infrastructure/DependencyInjection.cs`:

```csharp
private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
{
    services.AddHostedService<BookingPaymentCheckService>();
    return services;
}
```

#### Logging

The service provides comprehensive logging:

- **Start/End**: Logs when the service starts and completes
- **Cancellations**: Logs each cancelled booking with details
- **Car Availability**: Logs when cars are made available
- **Errors**: Logs any exceptions with full details
- **Summary**: Logs summary of actions taken

Example log output:
```
[INFO] Booking payment check service started at: 2025-01-20T10:00:00Z
[INFO] Cancelled booking 123 due to incomplete payment. AmountDue: 500.00, AmountPaid: 300.00, TimeUntilStart: 00:05:30
[INFO] Made car 45 available again after cancelling booking 123
[INFO] Payment check completed: 1 bookings cancelled, 1 cars made available
```

#### Error Handling

- **Exception Handling**: Wraps all operations in try-catch blocks
- **Graceful Degradation**: Continues running even if individual operations fail
- **Retry Logic**: Waits 5 minutes before retrying after errors
- **Detailed Logging**: Logs full exception details for debugging

#### Performance Considerations

- **Efficient Queries**: Uses `FindWithComplexIncludes` for optimized data loading
- **Batch Processing**: Processes all bookings in a single database transaction
- **Memory Management**: Uses scoped services to prevent memory leaks
- **Async Operations**: All database operations are async for better performance

### Adding New Background Services

#### Step 1: Create the Service

```csharp
public class NewBackgroundService : BackgroundService
{
    private readonly ILogger<NewBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public NewBackgroundService(
        ILogger<NewBackgroundService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PerformTaskAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in NewBackgroundService");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private async Task PerformTaskAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        // Your business logic here
    }
}
```

#### Step 2: Register the Service

Add to `Infrastructure/DependencyInjection.cs`:

```csharp
private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
{
    services.AddHostedService<BookingPaymentCheckService>();
    services.AddHostedService<NewBackgroundService>(); // Add this line
    return services;
}
```

## 🗄️ Database Schema

### Core Entities

#### Car
- `Id` (int, PK)
- `Model` (string)
- `Capacity` (int)
- `Color` (string)
- `Image` (string)
- `CategoryId` (int, FK)
- `CarStatus` (enum)
- `Pph` (decimal) - Price per hour
- `Ppd` (decimal) - Price per day
- `Mbw` (decimal) - Minimum booking worth

#### Booking
- `Id` (int, PK)
- `CustomerId` (long, FK)
- `EmployeeId` (long?, FK)
- `BookingType` (string)
- `StartDateTime` (datetime)
- `EndDateTime` (datetime)
- `Status` (enum)
- `NumOfPassengers` (int)

#### Payment
- `Id` (int, PK)
- `BookingId` (int, FK)
- `Status` (enum)
- `AmountDue` (decimal)
- `AmountPaid` (decimal)
- `PaymentDate` (datetime)
- `Notes` (string)

#### Category
- `Id` (int, PK)
- `Title` (string)

#### Customer
- `Id` (long, PK)
- `UserId` (long, FK)
- `FirstName` (string)
- `LastName` (string)
- `Country` (string)
- `IsDeleted` (boolean)
- `IsActive` (boolean)

### Relationships

- **Car** ↔ **Category** (Many-to-One)
- **Booking** ↔ **Customer** (Many-to-One)
- **Booking** ↔ **Employee** (Many-to-One)
- **Booking** ↔ **Payment** (One-to-Many)
- **CarBooking** ↔ **Car** (Many-to-One)
- **CarBooking** ↔ **Booking** (One-to-One)

### Database Seeding

The system includes comprehensive seeding data:

#### Categories
- VIP
- Eco
- Family
- Luxury
- SUV
- Compact

#### Cars (13 cars total)
- **VIP**: Mercedes, BMW 7 Series
- **Eco**: Tesla, Toyota Prius
- **Family**: Honda Odyssey, Toyota Sienna
- **Luxury**: Audi A8, Lexus LS
- **SUV**: Toyota Highlander, Honda CR-V
- **Compact**: Honda Civic, Toyota Corolla, Ford Focus

#### Default Admin Account
- **Email**: admin@tripagency.com
- **Password**: Admin123!
- **Role**: Administrator

## 🚀 Deployment

### Local Development

1. **Database Setup**
   ```bash
   dotnet ef database update --project TripAgency/Infrastructure --startup-project TripAgency/API
   ```

2. **Run Application**
   ```bash
   dotnet run --project TripAgency/API
   ```

### Production Deployment

1. **Environment Configuration**
   - Set `ASPNETCORE_ENVIRONMENT=Production`
   - Configure production connection strings
   - Set up logging configuration

2. **Database Migration**
   ```bash
   dotnet ef database update --project TripAgency/Infrastructure --startup-project TripAgency/API
   ```

3. **Publish Application**
   ```bash
   dotnet publish TripAgency/API -c Release -o ./publish
   ```

### Docker Deployment

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TripAgency.sln", "./"]
COPY ["API/API.csproj", "API/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
```

## 🔧 Configuration

### App Settings

#### Development (`appsettings.Development.json`)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TripAgencyDb;Trusted_Connection=true;MultipleActiveResultSets=true",
    "IdentityConnection": "Server=(localdb)\\mssqllocaldb;Database=TripAgencyIdentityDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

#### Production (`appsettings.Production.json`)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=TripAgencyDb;User Id=your-user;Password=your-password;",
    "IdentityConnection": "Server=your-server;Database=TripAgencyIdentityDb;User Id=your-user;Password=your-password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🧪 Testing

### Unit Testing

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test TripAgency.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Integration Testing

```bash
# Run integration tests
dotnet test TripAgency.IntegrationTests
```

### API Testing

1. **Swagger UI**: Access at `https://localhost:7001/swagger`
2. **Postman**: Import collection from API documentation
3. **Automated Tests**: Use test projects for API testing



### Key Metrics to Monitor

- **Background Service Execution**: Frequency and success rates
- **Database Performance**: Query execution times
- **API Response Times**: Endpoint performance
- **Error Rates**: Application and database errors
- **Resource Usage**: Memory and CPU utilization

### Health Checks

```csharp
// Add health checks
services.AddHealthChecks()
    .AddDbContext<ApplicationDbContext>()
    .AddDbContext<IdentityAppDbContext>();
```

Access health checks at: `/health`

## 🔒 Security

### Authentication & Authorization

- **JWT Tokens**: Secure token-based authentication
- **Role-based Access**: Admin, Employee, Customer, User roles
- **Password Policies**: Strong password requirements
- **Account Lockout**: Protection against brute force attacks

### Data Protection

- **Connection Strings**: Encrypted in production
- **Sensitive Data**: Properly hashed and encrypted
- **Input Validation**: Comprehensive validation on all inputs
- **SQL Injection Protection**: Entity Framework parameterized queries

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Code Style Guidelines

- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Write unit tests for business logic
- Follow Clean Architecture principles

### Development Workflow

1. **Feature Development**
   - Create feature branch from main
   - Implement feature with tests
   - Update documentation
   - Create pull request

2. **Code Review**
   - All changes require review
   - Automated tests must pass
   - Documentation must be updated

3. **Deployment**
   - Merge to main branch
   - Automated deployment pipeline
   - Monitor for issues

## 🐛 Troubleshooting

### Common Issues

1. **Database Connection Issues**
   - Verify connection strings
   - Check SQL Server is running
   - Ensure database exists

2. **Background Service Not Running**
   - Check service registration in DI
   - Verify no startup exceptions
   - Check application logs

3. **Authentication Issues**
   - Verify JWT configuration
   - Check user credentials
   - Validate token expiration

4. **Performance Issues**
   - Monitor database queries
   - Check for memory leaks
   - Review background service frequency

### Debug Mode

Enable debug logging for detailed information:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information"
    }
  }
}
```

## 📞 Support

For support and questions:
- **Documentation**: Check this README and API docs
- **Issues**: Create GitHub issues for bugs
- **Email**: support@tripagency.com
- **Discord**: Join our community server


*Built with ❤️ using .NET 8 and Clean Architecture* 