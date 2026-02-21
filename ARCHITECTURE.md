# OncoTimeline - Architecture Documentation

## Product Vision

OncoTimeline is a premium pediatric B-ALL leukemia companion app with two core pillars:

1. **Premium Timeline**: Beautiful, interactive daily treatment tracking with professional visualization
2. **B-ALL Knowledge Hub**: Comprehensive leukemia education separated into technical and non-technical content

## System Architecture

### Clean Architecture Layers

```
┌─────────────────────────────────────────────────────────┐
│                     Presentation Layer                   │
│                                                           │
│  ┌─────────────────┐         ┌─────────────────┐       │
│  │   React Web     │         │   REST API      │       │
│  │   (Vite)        │◄────────┤   Controllers   │       │
│  └─────────────────┘         └─────────────────┘       │
└───────────────────────────────────┬─────────────────────┘
                                    │
┌───────────────────────────────────▼─────────────────────┐
│                   Application Layer                      │
│                                                           │
│  ┌─────────────────┐         ┌─────────────────┐       │
│  │ TimelineService │         │   DrugService   │       │
│  └─────────────────┘         └─────────────────┘       │
│                                                           │
│  ┌──────────────────────────────────────────┐           │
│  │              DTOs                         │           │
│  └──────────────────────────────────────────┘           │
└───────────────────────────────────┬─────────────────────┘
                                    │
┌───────────────────────────────────▼─────────────────────┐
│                    Domain Layer                          │
│                                                           │
│  ┌─────────────────────────────────────────┐            │
│  │  Entities: Patient, TimelineEvent,      │            │
│  │  Drug, TreatmentPhase, LabResult, etc.  │            │
│  └─────────────────────────────────────────┘            │
│                                                           │
│  ┌─────────────────────────────────────────┐            │
│  │  Interfaces: IRepositories,             │            │
│  │  IAIKnowledgeService                    │            │
│  └─────────────────────────────────────────┘            │
└───────────────────────────────────┬─────────────────────┘
                                    │
┌───────────────────────────────────▼─────────────────────┐
│                Infrastructure Layer                      │
│                                                           │
│  ┌─────────────────┐         ┌─────────────────┐       │
│  │  EF Core        │         │  Repositories   │       │
│  │  DbContext      │         │  Implementation │       │
│  └─────────────────┘         └─────────────────┘       │
│                                                           │
│  ┌──────────────────────────────────────────┐           │
│  │         PostgreSQL Database              │           │
│  └──────────────────────────────────────────┘           │
└─────────────────────────────────────────────────────────┘
```

## Data Model

### Entity Relationships

```
Patient (1) ──────────── (*) TimelineEvent
   │                            │
   │                            │ (*)
   │                            │
   │                      TimelineEventDrug (Junction)
   │                            │
   │                            │ (*)
   │                            │
   │ (*)                        │
   │                         Drug (1)
   │                            │
TreatmentPhase (*)              │ (*)
   │                            │
   │                      DrugSideEffect
   │
   │ (*)
   │
LabResult

   │ (*)
   │
SymptomEntry
```

### Key Relationships

- **Patient → TimelineEvent**: One-to-Many
- **Patient → TreatmentPhase**: One-to-Many
- **Patient → LabResult**: One-to-Many
- **Patient → SymptomEntry**: One-to-Many
- **TreatmentPhase → TimelineEvent**: One-to-Many
- **TimelineEvent ↔ Drug**: Many-to-Many (via TimelineEventDrug)
- **Drug → DrugSideEffect**: One-to-Many

## Component Architecture (React)

### Main Navigation Structure

```
App
├── Navigation (Premium UI)
└── Router
    ├── TimelinePage ⭐ PREMIUM FEATURE
    │   ├── TimelineControls (zoom, date range, filters)
    │   ├── PremiumTimeline (horizontal scrollable)
    │   │   ├── PhaseBar (Induction, Consolidation, Maintenance)
    │   │   ├── EventMarkers (daily events with icons)
    │   │   └── EventDetail (modal with full info)
    │   └── QuickAddEvent (floating action button)
    │
    ├── KnowledgeHubPage ⭐ CORE FEATURE
    │   ├── AudienceToggle (Technical / Non-Technical)
    │   ├── CategoryNav (Treatment Phases, Side Effects, Recovery, etc.)
    │   └── ContentArea
    │       ├── NonTechnicalView
    │       │   ├── ParentFriendlyExplanations
    │       │   ├── WhatToExpect
    │       │   └── SimplifiedTimelines
    │       └── TechnicalView
    │           ├── MedicalDetails
    │           ├── LabValueInterpretation
    │           └── ClinicalProtocols
    │
    ├── DrugsPage
    │   ├── DrugList (searchable, filterable)
    │   │   └── DrugCard (name, class, quick info)
    │   └── DrugDetail (modal)
    │       ├── TechnicalTab (mechanism, pharmacology)
    │       └── ParentTab (simple explanation, what to watch)
    │
    ├── LabsPage (Phase 2)
    │   ├── LabChart (trends over time)
    │   └── LabExplainer (technical vs non-technical)
    │
    └── SymptomsPage (Phase 2)
        ├── SymptomTracker (daily logging)
        └── SymptomChart (patterns visualization)
```

### Premium Timeline Features

**Visual Design**
- Horizontal scrollable canvas with smooth animations
- Color-coded treatment phases (Induction, Consolidation, Maintenance)
- Icon-based event markers (💉 chemo, 🏥 hospital, 🔬 labs, 📝 notes)
- Zoom levels: Day, Week, Month, Full Treatment
- Touch/gesture support for mobile

**Interactions**
- Click event → Full detail modal
- Drag to scroll timeline
- Pinch to zoom
- Quick add via floating button
- Filter by category (chemo, labs, symptoms, notes)

**Data Display**
- Event title and time
- Associated drugs with dosages
- Lab results (if applicable)
- Symptom notes
- Photos/attachments (future)

## API Design

### RESTful Endpoints

**Timeline Events** (Premium Feature)
- `GET /api/timeline/patient/{patientId}` - Get all events for patient
- `GET /api/timeline/patient/{patientId}/range` - Get events in date range
- `POST /api/timeline` - Create new event
- `PUT /api/timeline/{id}` - Update event
- `DELETE /api/timeline/{id}` - Delete event

**Drugs**
- `GET /api/drugs` - Get all drugs
- `GET /api/drugs/{id}` - Get drug by ID
- `GET /api/drugs/name/{name}` - Get drug by name
- `POST /api/drugs` - Create new drug

**Knowledge Hub** (B-ALL Education)
- `GET /api/knowledge` - Get all articles
- `GET /api/knowledge/category/{category}` - Get by category
- `GET /api/knowledge/{id}` - Get specific article
- `GET /api/knowledge/audience/{technical|nontechnical}` - Filter by audience

**Treatment Phases**
- `GET /api/phases/patient/{patientId}` - Get patient's treatment phases
- `POST /api/phases` - Create new phase
- `PUT /api/phases/{id}` - Update phase

### Request/Response Examples

**Create Timeline Event**
```json
POST /api/timeline
{
  "patientId": "guid",
  "treatmentPhaseId": "guid",
  "title": "Vincristine Infusion",
  "eventDate": "2024-01-15T10:00:00Z",
  "category": "Chemotherapy",
  "notes": "Day 1 of induction",
  "tags": "induction,vincristine",
  "drugs": [
    {
      "drugId": "guid",
      "dosage": "1.5 mg/m²",
      "route": "IV"
    }
  ]
}
```

**Response**
```json
{
  "id": "guid",
  "patientId": "guid",
  "treatmentPhaseId": "guid",
  "title": "Vincristine Infusion",
  "eventDate": "2024-01-15T10:00:00Z",
  "category": "Chemotherapy",
  "notes": "Day 1 of induction",
  "tags": "induction,vincristine",
  "drugs": [
    {
      "id": "guid",
      "name": "Vincristine",
      "drugClass": "Vinca Alkaloid",
      "parentFriendlyExplanation": "...",
      "dosage": "1.5 mg/m²",
      "route": "IV"
    }
  ]
}
```

## Security Architecture

### Authentication Flow (Production)

```
┌─────────┐                ┌──────────┐                ┌─────────┐
│ Browser │                │ Cognito  │                │   API   │
└────┬────┘                └────┬─────┘                └────┬────┘
     │                          │                           │
     │  1. Login Request        │                           │
     ├─────────────────────────►│                           │
     │                          │                           │
     │  2. JWT Token            │                           │
     │◄─────────────────────────┤                           │
     │                          │                           │
     │  3. API Request + JWT    │                           │
     ├──────────────────────────┼──────────────────────────►│
     │                          │                           │
     │                          │  4. Validate Token        │
     │                          │◄──────────────────────────┤
     │                          │                           │
     │                          │  5. Token Valid           │
     │                          ├──────────────────────────►│
     │                          │                           │
     │  6. Response             │                           │
     │◄─────────────────────────┼───────────────────────────┤
     │                          │                           │
```

### Data Protection

1. **At Rest**: RDS encryption with AWS KMS
2. **In Transit**: HTTPS/TLS 1.3
3. **Application**: Input validation, parameterized queries
4. **Access Control**: Role-based authorization

## AI Integration Architecture

### Future AI Service Layer

```
┌─────────────────────────────────────────────────────────┐
│                   AI Service Layer                       │
│                                                           │
│  ┌─────────────────────────────────────────┐            │
│  │      IAIKnowledgeService                │            │
│  │                                          │            │
│  │  - GenerateEducationalSummary()         │            │
│  │  - ExplainTimelineWeek()                │            │
│  │  - ExplainDrugInteraction()             │            │
│  └──────────────┬──────────────────────────┘            │
│                 │                                         │
│  ┌──────────────▼──────────────────────────┐            │
│  │      AWS Bedrock Integration            │            │
│  │                                          │            │
│  │  - Claude 3 for medical explanations    │            │
│  │  - Prompt templates with safety rules   │            │
│  │  - Automatic disclaimer injection       │            │
│  └─────────────────────────────────────────┘            │
└─────────────────────────────────────────────────────────┘
```

### AI Safety Rules

1. **Always include disclaimer**: "This information is educational and does not replace medical advice from your oncology team."
2. **No diagnosis**: Never suggest diagnoses
3. **No treatment decisions**: Never recommend treatment changes
4. **Educational tone**: Explain, don't prescribe
5. **Cite consensus**: Reference general medical knowledge
6. **Simplified language**: Parent-friendly

## Knowledge Hub Content Structure

### Categories

1. **Treatment Phases**
   - Induction (what it is, duration, goals)
   - Consolidation (purpose, typical drugs)
   - Interim Maintenance
   - Delayed Intensification
   - Maintenance (long-term care)

2. **Side Effects & Management**
   - Common side effects by drug
   - When to call the doctor
   - Home management tips
   - Expected vs concerning symptoms

3. **Lab Values Explained**
   - WBC, ANC, Platelets, Hemoglobin
   - What numbers mean
   - Why they fluctuate
   - Transfusion thresholds

4. **Recovery & Life After Treatment**
   - Survivorship care
   - Long-term monitoring
   - School reintegration
   - Emotional support

5. **Procedures**
   - Spinal taps (lumbar punctures)
   - Bone marrow aspirations
   - Port access
   - Blood transfusions

### Audience Separation

**Non-Technical (Parent-Friendly)**
- Simple language, analogies
- "What to expect" focus
- Practical tips
- Emotional support context
- Visual aids and diagrams

**Technical (Medical Detail)**
- Clinical terminology
- Mechanism of action
- Pharmacology details
- Protocol references (COG, DFCI)
- Research citations
- Lab value ranges with units

## Premium Timeline Technical Specs

### Frontend Libraries
- React + TypeScript
- D3.js or Recharts for timeline visualization
- Framer Motion for animations
- React Query for data fetching
- Zustand for state management

### Timeline Data Structure
```typescript
interface TimelineEvent {
  id: string;
  patientId: string;
  date: Date;
  category: 'chemo' | 'lab' | 'hospital' | 'symptom' | 'note';
  title: string;
  drugs?: DrugAdministration[];
  labResults?: LabResult[];
  notes: string;
  icon: string;
  color: string;
}
```

### Performance Considerations
- Virtual scrolling for large datasets
- Lazy loading of event details
- Optimistic UI updates
- Offline support with service workers
- Image compression for attachments explanations

## Performance Considerations

### Database Optimization

1. **Indexes**:
   - TimelineEvent.EventDate
   - TimelineEvent.PatientId
   - LabResult (PatientId, LabDate)
   - SymptomEntry (PatientId, EntryDate)

2. **Query Optimization**:
   - Eager loading for related entities
   - Pagination for large result sets
   - Caching for drug database (rarely changes)

### Frontend Optimization

1. **Code Splitting**: Route-based lazy loading
2. **Memoization**: React.memo for expensive components
3. **Virtual Scrolling**: For long timelines
4. **Image Optimization**: WebP format, lazy loading

## Scalability

### Horizontal Scaling

```
┌─────────────┐
│   ALB       │  (Application Load Balancer)
└──────┬──────┘
       │
   ┌───┴───┐
   │       │
┌──▼──┐ ┌──▼──┐
│ API │ │ API │  (Multiple ECS tasks)
│  1  │ │  2  │
└──┬──┘ └──┬──┘
   │       │
   └───┬───┘
       │
   ┌───▼───┐
   │  RDS  │  (Read replicas for scaling reads)
   └───────┘
```

### Caching Strategy

1. **Drug Database**: Redis cache (rarely changes)
2. **Timeline Events**: Short TTL cache per patient
3. **AI Responses**: Cache by prompt hash

## Monitoring & Observability

### Metrics to Track

1. **Application**:
   - API response times
   - Error rates
   - Request throughput

2. **Database**:
   - Query performance
   - Connection pool usage
   - Slow query log

3. **User Experience**:
   - Page load times
   - Timeline rendering performance
   - API call latency

### Logging Strategy

1. **Structured Logging**: JSON format
2. **Log Levels**: Debug, Info, Warning, Error, Critical
3. **Correlation IDs**: Track requests across services
4. **PII Redaction**: Never log patient data

## Deployment Pipeline

```
┌──────────┐     ┌──────────┐     ┌──────────┐     ┌──────────┐
│   Git    │────►│  Build   │────►│   Test   │────►│  Deploy  │
│  Commit  │     │  (CI)    │     │  (CI)    │     │  (CD)    │
└──────────┘     └──────────┘     └──────────┘     └──────────┘
                      │                 │                 │
                      ▼                 ▼                 ▼
                 Compile Code      Run Tests        Push to ECR
                 Run Linters       Security Scan    Update ECS
                                                     Update S3
```

## Technology Stack Summary

**Backend**
- .NET 8 Web API
- Entity Framework Core 8
- PostgreSQL 14+
- Clean Architecture

**Frontend**
- React 18
- Vite
- React Router
- Axios
- date-fns
- Recharts (for future charts)

**Cloud (Production)**
- AWS ECS/Fargate (API hosting)
- AWS RDS PostgreSQL (database)
- AWS S3 + CloudFront (frontend)
- AWS Cognito (authentication)
- AWS Bedrock (AI features)
- AWS KMS (encryption)

**Development**
- Git version control
- Docker (optional local development)
- Swagger/OpenAPI (API documentation)
