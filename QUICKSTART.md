# OncoTimeline - Quick Start Guide

## Prerequisites

- **.NET 10 SDK** (required)
- **PostgreSQL** (for production) or use in-memory DB for development
- **Node.js 18+** (for React frontend)

## Running the Backend API

```bash
# From project root - runs the API project automatically
dotnet run --project src/OncoTimeline.API/OncoTimeline.API.csproj

# Or from API directory
cd src/OncoTimeline.API
dotnet run
```

The API will start at:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: http://localhost:5000/swagger

## Available Endpoints

### Timeline Events
```
GET    /api/timeline/patient/{patientId}
GET    /api/timeline/patient/{patientId}/range?startDate=...&endDate=...
POST   /api/timeline
PUT    /api/timeline/{id}
DELETE /api/timeline/{id}
```

### Knowledge Hub
```
GET    /api/knowledge
GET    /api/knowledge/{id}
GET    /api/knowledge/category/{category}
GET    /api/knowledge/audience/{technical|nontechnical}
POST   /api/knowledge
```

### Treatment Phases
```
GET    /api/phases/patient/{patientId}
POST   /api/phases
```

### Drugs
```
GET    /api/drugs
GET    /api/drugs/{id}
GET    /api/drugs/name/{name}
POST   /api/drugs
```

## Database Setup

### Option 1: In-Memory (Development)
Already configured by default. Database is created automatically on startup with seed data.

### Option 2: PostgreSQL (Production)

1. Install PostgreSQL
2. Create database:
```sql
CREATE DATABASE oncotimeline;
```

3. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=oncotimeline;Username=postgres;Password=yourpassword"
  }
}
```

4. Run migrations:
```bash
cd src/OncoTimeline.Infrastructure
dotnet ef database update
```

## Seed Data

The application automatically seeds:
- 3 common B-ALL chemotherapy drugs (Vincristine, Methotrexate, Daunorubicin)
- Complete side effect information
- Technical and parent-friendly explanations

## Testing the API

### Using Swagger UI
1. Navigate to http://localhost:5000/swagger
2. Explore and test all endpoints interactively

### Using curl

**Get all drugs:**
```bash
curl http://localhost:5000/api/drugs
```

**Get knowledge articles:**
```bash
curl http://localhost:5000/api/knowledge
```

**Filter by audience:**
```bash
curl http://localhost:5000/api/knowledge/audience/nontechnical
```

## Running the Frontend (Coming Soon)

```bash
cd src/OncoTimeline.Web
npm install
npm run dev
```

Frontend will be available at http://localhost:5173

## Project Structure

```
OncoTimeline/
├── src/
│   ├── OncoTimeline.Domain/          # Entities & Interfaces
│   ├── OncoTimeline.Application/     # Services & DTOs
│   ├── OncoTimeline.Infrastructure/  # Data Access & Repositories
│   ├── OncoTimeline.API/             # REST API Controllers
│   └── OncoTimeline.Web/             # React Frontend
└── tests/
```

## Technology Stack

- **.NET 10** - Backend framework
- **Entity Framework Core 10** - ORM
- **PostgreSQL** - Database
- **Swagger/OpenAPI** - API documentation
- **React + Vite** - Frontend (coming soon)

## Next Steps

1. ✅ Backend API is ready
2. 🚧 Implement React frontend
3. 🚧 Add authentication (AWS Cognito)
4. 🚧 Deploy to AWS

## Troubleshooting

**Port already in use:**
```bash
# Change port in Properties/launchSettings.json
```

**Database connection issues:**
```bash
# Check PostgreSQL is running
pg_isready

# Verify connection string in appsettings.json
```

**Build errors:**
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

## Support

See `ARCHITECTURE.md` for detailed system design.
See `IMPLEMENTATION_STATUS.md` for current progress.
