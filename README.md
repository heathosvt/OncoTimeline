# OncoTimeline

Private pediatric oncology companion application for tracking B-ALL leukemia treatment progress.

## 🎯 MVP Status: COMPLETE ✅

All three core features are fully functional and deployed to GitHub.

## Architecture

- **Backend**: .NET 10 Web API, EF Core, In-Memory Database (PostgreSQL-ready)
- **Frontend**: Razor Pages with Alpine.js and TailwindCSS
- **Cloud**: AWS/Azure-ready deployment

## Project Structure

```
OncoTimeline/
├── src/
│   ├── OncoTimeline.Domain/          # Core entities and interfaces
│   ├── OncoTimeline.Application/     # Business logic and services
│   ├── OncoTimeline.Infrastructure/  # Data access and repositories
│   ├── OncoTimeline.API/             # REST API endpoints (port 5000)
│   └── OncoTimeline.Web/             # Razor Pages frontend (port 5174)
└── tests/
```

## ✅ Implemented Features

### 1. Premium Timeline ⭐ PRIMARY FEATURE
- ✅ Horizontal timeline with visual line
- ✅ Color-coded treatment phases (Induction, Consolidation, Maintenance)
- ✅ Event markers with emoji icons (💉 🏥 🔬 😷 📝)
- ✅ Click markers to view event details in modal
- ✅ Phase bars showing date ranges
- ✅ Zoom controls (Day, Week, Month, Full)
- ✅ Category filters (All, Chemo, Lab, Hospital, Symptom, Note)
- ✅ Floating action button for quick add
- ✅ 4 demo events seeded

### 2. B-ALL Knowledge Hub 📚 CORE EDUCATION
- ✅ Audience toggle (Parent-Friendly / Medical Detail)
- ✅ Category filter (Treatment Phases, Side Effects, Lab Values, Procedures, Recovery)
- ✅ Search functionality
- ✅ Article cards in responsive grid
- ✅ Click cards to open article modal
- ✅ AI-generated content disclaimers
- ✅ 3 demo articles seeded

### 3. Drug Database 💊
- ✅ Search by name or drug class
- ✅ Drug cards in responsive grid
- ✅ Click cards to open drug detail modal
- ✅ Dual tabs: Parent Info & Technical
- ✅ Parent tab: What it does, what to watch, common side effects
- ✅ Technical tab: Mechanism, timing, lab changes, neurological impacts
- ✅ Side effects with severity badges (Common, Moderate, Severe)
- ✅ 3 drugs seeded (Vincristine, Daunorubicin, L-Asparaginase)

### 4. Design & UX
- ✅ TailwindCSS styling
- ✅ Lucide icons throughout
- ✅ Card hover effects and animations
- ✅ Modal transitions
- ✅ Responsive design (mobile-friendly)
- ✅ Professional color scheme

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or higher
- Git

### Run Locally

```bash
# Clone repository
git clone https://github.com/heathosvt/OncoTimeline.git
cd OncoTimeline

# Run Web application
cd src/OncoTimeline.Web
dotnet run
```

Visit: http://localhost:5174

### Demo Data

The application seeds demo data on startup:
- **Patient**: Demo Patient (8 years old, B-ALL, Standard risk)
- **Phases**: 3 treatment phases with color coding
- **Events**: 4 timeline events (chemo, lab, hospital, note)
- **Articles**: 3 knowledge articles (technical & parent-friendly)
- **Drugs**: 3 chemotherapy drugs with full details

**Note**: In-memory database resets on restart. For persistent data, configure PostgreSQL.

## 🌐 Deployment

### Quick Deploy to Azure (Recommended)

```bash
# Install Azure CLI: https://aka.ms/installazurecli
az login
az group create --name OncoTimeline-RG --location eastus
az appservice plan create --name OncoTimeline-Plan --resource-group OncoTimeline-RG --sku B1 --is-linux
az webapp create --name oncotimeline-demo --resource-group OncoTimeline-RG --plan OncoTimeline-Plan --runtime "DOTNETCORE:8.0"
az webapp deployment source config --name oncotimeline-demo --resource-group OncoTimeline-RG --repo-url https://github.com/heathosvt/OncoTimeline --branch main --manual-integration
```

**Cost**: ~$13/month (B1 tier with SSL)

### Docker Deployment

```bash
docker build -t oncotimeline .
docker run -p 8080:80 oncotimeline
```

Visit: http://localhost:8080

### Deploy to:
- Azure App Service (recommended)
- AWS Elastic Beanstalk
- Google Cloud Run
- DigitalOcean App Platform
- Any Docker-compatible platform

## 🔧 Configuration

### Switch to PostgreSQL (Production)

Update `src/OncoTimeline.Web/Program.cs`:

```csharp
// Replace in-memory database
builder.Services.AddDbContext<OncoTimelineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

Add to `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-host;Database=oncotimeline;Username=user;Password=pass"
  }
}
```

### Environment Variables

For production deployment:
```
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=<your-db-connection>
```

## 📦 Tech Stack

- **.NET 10**: Backend framework
- **EF Core**: ORM and data access
- **Razor Pages**: Server-side rendering
- **Alpine.js**: Lightweight JavaScript framework
- **TailwindCSS**: Utility-first CSS
- **Lucide Icons**: Icon library
- **PostgreSQL**: Database (production)
- **In-Memory DB**: Development/demo

## 🔐 Privacy & Safety

- HIPAA-conscious design
- No public data exposure
- Educational disclaimers on all AI content
- Encrypted storage ready
- Local-first architecture

## 📋 Future Enhancements (Phase 2)

- [ ] Add/Edit/Delete events (CRUD forms)
- [ ] Lab tracking with charts (WBC, ANC, Platelets, Hemoglobin)
- [ ] Symptom tracking and pattern visualization
- [ ] User authentication (AWS Cognito / Azure AD)
- [ ] Multi-patient support
- [ ] Export timeline to PDF
- [ ] Mobile app (React Native)

## 🤝 Contributing

This is a private project for pediatric oncology support. For questions or suggestions, please open an issue.

## 📄 License

Private - All rights reserved

## 🆘 Support

- **GitHub Issues**: https://github.com/heathosvt/OncoTimeline/issues
- **Azure Docs**: https://docs.microsoft.com/azure/app-service/
- **Docker Docs**: https://docs.docker.com/

---

**Built with ❤️ for families navigating pediatric B-ALL treatment**
