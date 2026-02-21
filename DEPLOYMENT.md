# OncoTimeline Deployment Guide

## Current Status
✅ MVP deployed to GitHub: https://github.com/heathosvt/OncoTimeline
✅ All features working locally with in-memory database

## Deployment Options

### Option 1: Quick Deploy to Azure App Service (Recommended for MVP)

**Prerequisites:**
- Azure account
- Azure CLI installed

**Steps:**

1. **Create Azure Resources**
```bash
# Login to Azure
az login

# Create resource group
az group create --name OncoTimeline-RG --location eastus

# Create App Service plan (Free tier for testing)
az appservice plan create --name OncoTimeline-Plan --resource-group OncoTimeline-RG --sku F1 --is-linux

# Create web app
az webapp create --name oncotimeline-app --resource-group OncoTimeline-RG --plan OncoTimeline-Plan --runtime "DOTNETCORE:8.0"
```

2. **Deploy from GitHub**
```bash
# Configure GitHub deployment
az webapp deployment source config --name oncotimeline-app --resource-group OncoTimeline-RG --repo-url https://github.com/heathosvt/OncoTimeline --branch main --manual-integration
```

3. **Configure App Settings**
```bash
# Set .NET version
az webapp config set --name oncotimeline-app --resource-group OncoTimeline-RG --linux-fx-version "DOTNETCORE|8.0"

# Set startup command
az webapp config set --name oncotimeline-app --resource-group OncoTimeline-RG --startup-file "dotnet OncoTimeline.Web.dll"
```

**URL:** https://oncotimeline-app.azurewebsites.net

**Cost:** Free tier (F1) - $0/month for testing

---

### Option 2: Deploy to AWS Elastic Beanstalk

**Prerequisites:**
- AWS account
- AWS CLI installed
- EB CLI installed

**Steps:**

1. **Initialize EB**
```bash
cd src/OncoTimeline.Web
eb init -p "64bit Amazon Linux 2023 v3.1.0 running .NET 8" oncotimeline --region us-east-1
```

2. **Create Environment**
```bash
eb create oncotimeline-env --instance-type t2.micro
```

3. **Deploy**
```bash
dotnet publish -c Release
eb deploy
```

**URL:** http://oncotimeline-env.elasticbeanstalk.com

**Cost:** t2.micro - ~$8/month

---

### Option 3: Docker Container (Any Platform)

**Create Dockerfile:**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OncoTimeline.Web/OncoTimeline.Web.csproj", "OncoTimeline.Web/"]
COPY ["src/OncoTimeline.Application/OncoTimeline.Application.csproj", "OncoTimeline.Application/"]
COPY ["src/OncoTimeline.Domain/OncoTimeline.Domain.csproj", "OncoTimeline.Domain/"]
COPY ["src/OncoTimeline.Infrastructure/OncoTimeline.Infrastructure.csproj", "OncoTimeline.Infrastructure/"]
RUN dotnet restore "OncoTimeline.Web/OncoTimeline.Web.csproj"
COPY src/ .
WORKDIR "/src/OncoTimeline.Web"
RUN dotnet build "OncoTimeline.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OncoTimeline.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OncoTimeline.Web.dll"]
```

**Build and Run:**
```bash
docker build -t oncotimeline .
docker run -p 8080:80 oncotimeline
```

**Deploy to:**
- AWS ECS/Fargate
- Azure Container Instances
- Google Cloud Run
- DigitalOcean App Platform

---

## Production Considerations

### 1. Database Migration (Required for Production)

**Switch from In-Memory to PostgreSQL:**

Update `Program.cs`:
```csharp
// Replace in-memory database with PostgreSQL
builder.Services.AddDbContext<OncoTimelineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

Add to `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-db-host;Database=oncotimeline;Username=your-user;Password=your-password"
  }
}
```

**Create Database:**
- Azure Database for PostgreSQL
- AWS RDS PostgreSQL
- Supabase (free tier available)

### 2. Environment Variables

Set these in your hosting platform:
```
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=<your-db-connection-string>
```

### 3. HTTPS Configuration

Ensure HTTPS is enabled in production:
```csharp
// Already in Program.cs
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseHttpsRedirection();
```

### 4. Security Headers

Add security middleware:
```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});
```

### 5. Logging

Configure logging for production:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

---

## Quick Start: Deploy to Azure (5 minutes)

**Easiest option for immediate deployment:**

1. Install Azure CLI: https://aka.ms/installazurecli
2. Run deployment script:

```bash
#!/bin/bash
az login
az group create --name OncoTimeline-RG --location eastus
az appservice plan create --name OncoTimeline-Plan --resource-group OncoTimeline-RG --sku B1 --is-linux
az webapp create --name oncotimeline-$(date +%s) --resource-group OncoTimeline-RG --plan OncoTimeline-Plan --runtime "DOTNETCORE:8.0"
az webapp deployment source config --name oncotimeline-$(date +%s) --resource-group OncoTimeline-RG --repo-url https://github.com/heathosvt/OncoTimeline --branch main --manual-integration
```

3. Visit your app at: https://oncotimeline-[timestamp].azurewebsites.net

**Cost:** B1 tier - ~$13/month (includes SSL, custom domain, always-on)

---

## Monitoring & Maintenance

### Health Checks
Add health check endpoint:
```csharp
builder.Services.AddHealthChecks();
app.MapHealthChecks("/health");
```

### Application Insights (Azure)
```bash
az monitor app-insights component create --app oncotimeline-insights --location eastus --resource-group OncoTimeline-RG
```

### Backup Strategy
- Database: Automated daily backups
- Code: GitHub repository
- Configuration: Store in Azure Key Vault or AWS Secrets Manager

---

## Next Steps After Deployment

1. ✅ Test all features in production
2. ✅ Set up custom domain (optional)
3. ✅ Configure SSL certificate (auto with Azure/AWS)
4. ✅ Set up monitoring and alerts
5. ✅ Create backup strategy
6. ✅ Document API endpoints
7. ✅ Set up CI/CD pipeline (GitHub Actions)

---

## Support

For deployment issues:
- Azure: https://docs.microsoft.com/azure/app-service/
- AWS: https://docs.aws.amazon.com/elasticbeanstalk/
- Docker: https://docs.docker.com/

For application issues:
- GitHub Issues: https://github.com/heathosvt/OncoTimeline/issues
