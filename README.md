# ParkShare API

A peer-to-peer parking marketplace API built with ASP.NET Core. Users can list their parking spaces (garages, lots, covered spots, plazas) for rent and book spaces from other owners.

## Tech Stack

- **.NET 10** / ASP.NET Core Web API
- **Entity Framework Core 9** with **PostgreSQL** (Npgsql provider)
- **Swashbuckle** for Swagger/OpenAPI docs
- **Docker** (multi-stage build)

## Project Structure

```
ParkShareApi/
├── Common/         # Shared types (e.g. ApiResponse envelope)
├── Controllers/    # Auth, Spaces, Bookings endpoints
├── DTOs/           # Request/response contracts
├── Data/           # AppDbContext (EF Core)
├── Migrations/     # EF Core migrations
├── Models/         # Domain entities (User, ParkingSpace, Booking, Chat)
├── Services/       # Business logic (UserService, SpaceService, BookingService)
├── Program.cs      # App bootstrap, DI, Swagger, auto-migrate
└── Dockerfile
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL 13+ (local or Docker)

### Configuration

The default connection string is in [appsettings.json](ParkShareApi/appsettings.json):

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=ParkShareDb;Username=postgres;Password=postgres"
```

Override it via environment variable for production:

```
ConnectionStrings__DefaultConnection=Host=...;Port=5432;Database=...;Username=...;Password=...
```

### Run locally

```bash
cd ParkShareApi
dotnet restore
dotnet run
```

The API auto-applies pending EF Core migrations on startup, so the database schema is created on first run.

Swagger UI is mounted at the root of the app:

- `https://localhost:<port>/` — interactive Swagger UI
- `https://localhost:<port>/swagger/v1/swagger.json` — OpenAPI spec

### Run with Docker

```bash
docker build -t parkshare-api -f ParkShareApi/Dockerfile ParkShareApi
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=ParkShareDb;Username=postgres;Password=postgres" \
  parkshare-api
```

## API Overview

All responses use a uniform `ApiResponse<T>` envelope (status code, message, data).

### Auth — `/api/auth`

| Method | Route        | Description                  |
| ------ | ------------ | ---------------------------- |
| POST   | `/register`  | Create a new user account    |
| POST   | `/login`     | Authenticate with email/pwd  |

### Parking Spaces — `/api/spaces`

| Method | Route                     | Description                                         |
| ------ | ------------------------- | --------------------------------------------------- |
| GET    | `/`                       | List spaces (filter by `?city=` and `?type=`)       |
| GET    | `/{id}`                   | Get a single space by id                            |
| GET    | `/owner/{ownerId}`        | List all spaces belonging to an owner               |
| POST   | `/`                       | Create a new parking space listing                  |
| PATCH  | `/{id}/availability`      | Toggle the space's availability flag                |

`SpaceType` values: `Garage`, `OpenLot`, `Covered`, `Plaza`.

### Bookings — `/api/bookings`

| Method | Route                  | Description                              |
| ------ | ---------------------- | ---------------------------------------- |
| GET    | `/{id}`                | Get a booking by id                      |
| GET    | `/user/{userId}`       | List bookings made by a user             |
| GET    | `/space/{spaceId}`     | List bookings for a specific space       |
| POST   | `/`                    | Create a new booking                     |
| PATCH  | `/{id}/status`         | Update booking status (approve/reject…)  |

## Database Migrations

To add a new migration after changing the model:

```bash
cd ParkShareApi
dotnet ef migrations add <MigrationName>
```

Migrations are applied automatically on app startup (`db.Database.Migrate()` in [Program.cs](ParkShareApi/Program.cs)). To apply manually:

```bash
dotnet ef database update
```

## Notes

- The `LangVersion` is set to `14` and `Nullable` is enabled.
- Swagger UI is exposed in all environments to make testing easier from any deployment.
