# Booking System

Whats Including In This Repository
 # Booking System Api
  - Identity Server (https://localhost:8000)
  - API Gateways (https://localhost:8020)
  - Stay Management API (https://localhost:8010/swagger/index.html)
  - Stay Management gRPC (https://localhost:8011)
  - Promotion Management API (https://localhost:8020/swagger/index.html)
  - Promotion Management gRPC (https://localhost:8021)
  - Cars Manegenent API (https://localhost:8040)
  - Flight Management API (https://localhost:8050/swagger/index.html)
  - Booking Management API (https://localhost:8060/swagger/index.html)
  - Experiences Management API (https://localhost:8070/swagger/index.html)
  - Payment Management API (https://localhost:8090/swagger/index.html)
  - Payment Processor (https://localhost:8091)
  - Trips Management API (https://localhost:8110/swagger/index.html)

# Identity Server 6
 - "Duende.IdentityServer.AspNetIdentity" Package Version="6.3.2"
 - ASP.NET Core Web API application (.NET 8)
 - REST API principles
 - MSSQL Server, EF (Version="6.0.0")
 - JWT Authentication
 - Swagger Open API implementation

 # API Gateway ( ocelot )
  - JWT Authentication
  - Implement API Gateways with Ocelot (Version="18.0.0")

# Stay Management API
 - .NET Core 8
 - EF Core 6
 - Redis ( TODO )

# Promotion Management (API, gRPC)
 - .NET 8
 - PostgreSQL
 - gRPC (Version="2.57.0")

# Cars Management API
 - .NET 8
 - EF Core
 - GrapQL


# Microservices Cross-Cutting Implementations
  - Implementing Centralized Distributed Logging with Elastic Stack (ELK); Elasticsearch, Logstash, Kibana and SeriLog for Microservices
  - Use the HealthChecks feature in back-end ASP.NET microservices
  - Using Watchdog in separate service that can watch health and load across services, and report health about the microservices by querying with the HealthChecks
# Microservices Resilience Implementations
  - Making Microservices more resilient Use IHttpClientFactory to implement resilient HTTP requests
  - Implement Retry and Circuit Breaker patterns with exponential backoff with IHttpClientFactory and Polly policies
# Docker Compose establishment with all microservices on docker
  - Containerization of microservices
  - Containerization of databases
  - Override Environment variables