# OncoTimeline - Setup Guide

## Project Overview

OncoTimeline is a private pediatric oncology companion application for tracking leukemia treatment progress. This MVP focuses on Timeline + Drug linking functionality.

## Architecture

**Clean Architecture with 4 layers:**
- **Domain**: Core entities and interfaces (no dependencies)
- **Application**: Business logic and DTOs (depends on Domain)
- **Infrastructure**: Data access, EF Core, repositories (depends on Domain)
- **API**: REST endpoints (depends on Application + Infrastructure)

**Frontend**: React with Vite

## Prerequisites

- .NET 8 SDK
- PostgreSQL 14+
- Node.js 18+
- npm or yarn

## Backend Setup

### 1. Database Setup

```bash
# Install PostgreSQL (macOS)
brew install postgresql@14
brew services start postgresql@14

# Create database
createdb oncotimeline

# Update connection string in src/OncoTimeline.API/appsettings.json if needed
```

### 2. Build and Run API

```bash
cd /tmp/OncoTimeline

# Restore packages
dotnet restore

# Build solution
dotnet build

# Run API (from API project directory)
cd src/OncoTimeline.API
dotnet run

# API will be available at http://localhost:5000
# Swagger UI at http://localhost:5000/swagger
```

The database will be automatically created and seeded with drug data on first run.

## Frontend Setup

```bash
cd /tmp/OncoTimeline/src/OncoTimeline.Web

# Install dependencies
npm install

# Run development server
npm run dev

# Frontend will be available at http://localhost:3000
```

## Database Schema

### Core Tables

**Patients**
- Id (PK)
- FirstName, LastName
- DateOfBirth, DiagnosisDate
- DiagnosisType, RiskCategory

**TreatmentPhases**
- Id (PK)
- PatientId (FK)
- Name, Description
- StartDate, EndDate
- DisplayOrder, Color

**TimelineEvents**
- Id (PK)
- PatientId (FK)
- TreatmentPhaseId (FK, nullable)
- Title, EventDate, Category
- Notes, Tags

**Drugs**
- Id (PK)
- Name, DrugClass
- MechanismOfAction
- WhyUsedInLeukemia
- ParentFriendlyExplanation
- TypicalOnsetTiming, DurationOfEffects
- ExpectedLabChanges, NeurologicalImpacts

**DrugSideEffects**
- Id (PK)
- DrugId (FK)
- EffectName, Severity
- Description, TypicalOnset

**TimelineEventDrugs** (Junction Table)
- TimelineEventId (PK, FK)
- DrugId (PK, FK)
- Dosage, Route

**LabResults** (Phase 2)
- Id (PK)
- PatientId (FK)
- LabDate
- WBC, ANC, Hemoglobin, Platelets

**SymptomEntries** (Phase 2)
- Id (PK)
- PatientId (FK)
- EntryDate
- Temperature, AppetiteLevel, EnergyLevel
- NauseaLevel, SleepQuality, MoodLevel

**AIKnowledgeArticles**
- Id (PK)
- Title, Category
- Content, Summary
- IsAIGenerated, GeneratedAt
- Disclaimer

## API Endpoints

### Timeline

```
GET    /api/timeline/patient/{patientId}
GET    /api/timeline/patient/{patientId}/range?startDate=...&endDate=...
POST   /api/timeline
PUT    /api/timeline/{id}
DELETE /api/timeline/{id}
```

### Drugs

```
GET    /api/drugs
GET    /api/drugs/{id}
GET    /api/drugs/name/{name}
POST   /api/drugs
```

## Seeded Drug Data

The application comes pre-seeded with three chemotherapy drugs:

1. **Vincristine** (Vinca Alkaloid)
   - Stops cancer cell division
   - Side effects: peripheral neuropathy, constipation, jaw pain

2. **Methotrexate** (Antimetabolite)
   - Blocks folic acid metabolism
   - Side effects: mucositis, bone marrow suppression, liver toxicity

3. **Daunorubicin** (Anthracycline)
   - Damages cancer cell DNA
   - Side effects: bone marrow suppression, cardiotoxicity, nausea

## Frontend Structure

```
src/OncoTimeline.Web/
├── src/
│   ├── components/
│   │   ├── Timeline.jsx          # Horizontal scrollable timeline
│   │   ├── DrugList.jsx          # Drug database grid
│   │   ├── DrugDetail.jsx        # Drug detail modal
│   │   └── Navigation.jsx        # Main navigation
│   ├── pages/
│   │   ├── TimelinePage.jsx      # Timeline view
│   │   ├── DrugsPage.jsx         # Drug database view
│   │   ├── KnowledgePage.jsx     # AI knowledge hub
│   │   ├── LabsPage.jsx          # Lab tracking (Phase 2)
│   │   └── SymptomsPage.jsx      # Symptom tracking (Phase 2)
│   ├── services/
│   │   └── api.js                # API client
│   ├── App.jsx                   # Main app component
│   ├── App.css                   # Calm medical design
│   └── main.jsx                  # Entry point
├── index.html
├── vite.config.js
└── package.json
```

## Key Features (MVP)

### 1. Master Timeline
- Horizontal scrollable timeline
- Zoom levels: Day, Week, Month
- Event categories: Chemotherapy, SpinalTap, Lab, Symptom, Hospitalization, Note
- Visual grouping by treatment phase
- Click events to view details

### 2. Drug Database
- Comprehensive drug information
- Parent-friendly explanations
- Side effect documentation
- Automatic linking from timeline events

### 3. Timeline-Drug Linking
- Add drugs to timeline events
- View drug details from timeline
- Track dosage and route

### 4. General Information (Placeholder)
- Educational content structure
- AI integration ready
- Safety disclaimers

## Privacy & Security

### Current Implementation
- Local database (not exposed)
- No authentication (MVP)
- CORS configured for local development

### Production Readiness Checklist
- [ ] Implement AWS Cognito authentication
- [ ] Add user authorization
- [ ] Encrypt data at rest (AWS RDS encryption)
- [ ] Encrypt data in transit (HTTPS only)
- [ ] Add audit logging
- [ ] Implement rate limiting
- [ ] Add input validation and sanitization
- [ ] Configure AWS WAF
- [ ] Set up backup strategy
- [ ] Add HIPAA compliance controls

## Future Enhancements

### Phase 2
- Lab result tracking with trend graphs
- Symptom tracking with pattern recognition
- Export capabilities (PDF reports)
- Mobile responsive design improvements

### Phase 3
- AI knowledge generation (AWS Bedrock)
- "Explain this week" feature
- Predictive symptom patterns
- Treatment phase templates

### Phase 4
- Multi-patient support (for families with multiple children)
- Caregiver collaboration features
- Appointment scheduling integration
- Medication reminders

## AWS Deployment Architecture

```
┌─────────────────┐
│   CloudFront    │  (CDN for React app)
└────────┬────────┘
         │
┌────────▼────────┐
│   S3 Bucket     │  (Static React files)
└─────────────────┘

┌─────────────────┐
│  API Gateway    │  (REST API)
└────────┬────────┘
         │
┌────────▼────────┐
│   ECS/Fargate   │  (.NET API containers)
└────────┬────────┘
         │
┌────────▼────────┐
│   RDS Postgres  │  (Encrypted database)
└─────────────────┘

┌─────────────────┐
│   Cognito       │  (Authentication)
└─────────────────┘

┌─────────────────┐
│   Bedrock       │  (AI knowledge generation)
└─────────────────┘
```

## Testing

```bash
# Backend unit tests (to be implemented)
cd tests
dotnet test

# Frontend tests (to be implemented)
cd src/OncoTimeline.Web
npm test
```

## Troubleshooting

### Database Connection Issues
- Verify PostgreSQL is running: `brew services list`
- Check connection string in appsettings.json
- Ensure database exists: `psql -l`

### API Not Starting
- Check port 5000 is available
- Verify .NET 8 SDK: `dotnet --version`
- Check logs in console output

### Frontend Not Loading
- Verify API is running at http://localhost:5000
- Check browser console for errors
- Ensure npm dependencies installed: `npm install`

## Support

This is a private medical tracking application. For questions about the codebase, refer to inline documentation and architecture diagrams.

**Medical Disclaimer**: This application is for educational and tracking purposes only. It does not provide medical advice. Always consult with your oncology team for medical decisions.
