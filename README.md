# OncoTimeline

Private pediatric oncology companion application for tracking leukemia treatment progress.

## Architecture

- **Backend**: .NET 8 Web API, EF Core, PostgreSQL
- **Frontend**: React with timeline visualization
- **Cloud**: AWS-ready (Cognito, RDS, encrypted storage)

## Project Structure

```
OncoTimeline/
├── src/
│   ├── OncoTimeline.Domain/          # Core entities and interfaces
│   ├── OncoTimeline.Application/     # Business logic and services
│   ├── OncoTimeline.Infrastructure/  # Data access and external services
│   ├── OncoTimeline.API/             # REST API endpoints
│   └── OncoTimeline.Web/             # React frontend
└── tests/
```

## Core Features (MVP)

### 1. Premium Timeline ⭐ PRIMARY FEATURE
- Horizontal scrollable, zoomable treatment timeline
- Color-coded treatment phases (Induction → Consolidation → Maintenance)
- Daily event tracking (chemo, labs, hospital visits, symptoms, notes)
- Icon-based visual markers
- Quick add functionality
- Filter by category
- Beautiful, professional UI/UX

### 2. B-ALL Knowledge Hub 📚 CORE EDUCATION
- **Audience Toggle**: Technical vs Non-Technical content
- **Categories**:
  - Treatment Phases (Induction, Consolidation, Maintenance, etc.)
  - Side Effects & Management
  - Lab Values Explained
  - Procedures (spinal taps, bone marrow, ports)
  - Recovery & Survivorship
- Parent-friendly explanations
- Medical detail for those who want it
- AI-generated content with disclaimers

### 3. Drug Database
- Comprehensive drug information
- Technical tab (mechanism, pharmacology)
- Parent tab (simple explanation, what to watch)
- Side effects by severity
- Timeline integration (link drugs to events)

### 4. Lab Tracking (Phase 2)
- WBC, ANC, Platelets, Hemoglobin trends
- Visual charts over time
- Threshold indicators
- Explanations (technical vs simple)

### 5. Symptom Tracking (Phase 2)
- Daily symptom logging
- Pattern visualization
- Correlation with treatment events

## Privacy & Safety

- HIPAA-conscious design
- No public data exposure
- Educational disclaimers on all AI content
- Encrypted storage ready

## Getting Started

See individual project READMEs for setup instructions.
