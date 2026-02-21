# OncoTimeline Frontend Implementation Guide (Razor Pages + Alpine.js)

## Overview
This guide provides step-by-step prompts to implement the OncoTimeline Razor Pages frontend with Alpine.js for interactivity. Services are injected directly into PageModels. Follow these prompts in order with Amazon Q.

---

## Phase 1: Project Setup ✅ COMPLETE

The Razor Pages project is set up with:
- ✅ .NET 10 Razor Pages
- ✅ TailwindCSS via CDN
- ✅ Shared layout with navigation
- ✅ Home page with feature cards
- ✅ Services injected (Timeline, Drugs, Knowledge, Phases)
- ✅ In-memory database with drug seed data

**To run:**
```bash
cd src/OncoTimeline.Web
dotnet run
```
Visit: http://localhost:5173

---

## Phase 2: Premium Timeline (PRIMARY FEATURE)

### Prompt 2.1: Create Timeline Page Structure
```
Create Pages/Timeline.cshtml and Timeline.cshtml.cs with:

**Timeline.cshtml.cs (PageModel):**
- Inject TimelineService and TreatmentPhaseService
- Property: List<TimelineEventDto> Events
- Property: List<TreatmentPhaseDto> Phases
- Property: string DemoPatientId (use fixed GUID: "00000000-0000-0000-0000-000000000001")
- OnGet() method:
  - Fetch events for demo patient
  - Fetch treatment phases for demo patient
  - Sort events by date

**Timeline.cshtml (View):**
- Full-width container
- Timeline controls section (zoom, filters)
- Horizontal scrollable timeline canvas
- Phase bars (colored by phase)
- Event markers positioned by date
- Quick add floating button

Use TailwindCSS classes. Timeline should be min-h-screen.
```

### Prompt 2.2: Build Horizontal Scrollable Timeline
```
In Timeline.cshtml, create the timeline visualization:

**Phase Bar Section:**
- Display phases as horizontal colored bars
- Induction: bg-red-500
- Consolidation: bg-amber-500
- Maintenance: bg-green-500
- Show phase name, start date, end date
- Position phases by date on timeline

**Timeline Grid:**
- Horizontal scrollable container (overflow-x-auto)
- Date markers every day/week/month (based on zoom)
- Grid lines for visual reference
- Min-width based on date range

**Event Markers:**
- Position events on timeline by date
- Icon based on category:
  - Chemotherapy: 💉
  - Hospital: 🏥
  - Lab: 🔬
  - Note: 📝
  - Symptom: 😷
- Colored dot matching phase color
- Clickable to open modal

Use Alpine.js x-data for timeline state (zoom level, scroll position).
```

### Prompt 2.3: Add Timeline Controls
```
In Timeline.cshtml, add controls above timeline:

**Zoom Controls:**
- Buttons: Day, Week, Month, Full
- Alpine.js x-data: { zoom: 'week' }
- x-on:click to change zoom level
- Active button highlighted with bg-blue-600

**Category Filter:**
- Dropdown: All, Chemotherapy, Lab, Hospital, Symptom, Note
- Alpine.js x-model for selected category
- Filter events client-side using x-show

**Date Range Picker:**
- Start date and end date inputs
- Filter events by date range
- Use HTML5 date inputs

Style with TailwindCSS: flex gap-4, rounded buttons, shadow.
```

### Prompt 2.4: Create Event Detail Modal
```
In Timeline.cshtml, add modal for event details:

**Modal Structure:**
- Alpine.js x-data: { showModal: false, selectedEvent: null }
- x-show="showModal" with backdrop
- Click event marker sets selectedEvent and showModal=true
- Click backdrop or X button closes modal

**Modal Content:**
- Event title (text-2xl font-bold)
- Date and category badges
- Associated drugs section:
  - Drug name, dosage, route
  - Link to drug detail page
- Notes section (if present)
- Edit and Delete buttons (for future)
- Close button (X in top right)

Use TailwindCSS: fixed inset-0, bg-white rounded-lg shadow-xl, max-w-2xl mx-auto.
```

### Prompt 2.5: Create Quick Add Event Form
```
In Timeline.cshtml, add floating action button and form modal:

**Floating Action Button:**
- Fixed bottom-right position
- Large circular button with + icon
- bg-blue-600 hover:bg-blue-700
- Alpine.js x-on:click opens form modal

**Form Modal:**
- Alpine.js x-data: { showAddForm: false }
- Form with asp-page-handler="AddEvent"
- Fields:
  - Title (text input, required)
  - Date (date input, required)
  - Category (select dropdown)
  - Treatment Phase (select dropdown, optional)
  - Notes (textarea)
- Save and Cancel buttons
- POST to OnPostAddEvent() handler

**Timeline.cshtml.cs:**
- Add OnPostAddEvent() method
- Bind form properties with [BindProperty]
- Call TimelineService.CreateEventAsync()
- Return RedirectToPage() to refresh

Style form with TailwindCSS: space-y-4, labels, inputs with border.
```

---

## Phase 3: Knowledge Hub (CORE FEATURE)

### Prompt 3.1: Create Knowledge Hub Page Structure
```
Create Pages/Knowledge/Index.cshtml and Index.cshtml.cs with:

**Index.cshtml.cs (PageModel):**
- Inject KnowledgeService
- Property: List<KnowledgeArticleDto> Articles
- Property: string SelectedAudience (default: "NonTechnical")
- Property: string? SelectedCategory
- OnGet(string? audience, string? category) method:
  - Fetch articles filtered by audience and category
  - Use query parameters for filtering

**Index.cshtml (View):**
- 2-column layout: sidebar (20%) + content (80%)
- Audience toggle at top
- Category navigation in sidebar
- Article cards in main content area
- Search bar above articles

Use TailwindCSS grid: grid-cols-5, sidebar col-span-1, content col-span-4.
```

### Prompt 3.2: Build Audience Toggle
```
In Knowledge/Index.cshtml, create audience toggle:

**Toggle Component:**
- Two buttons: "Non-Technical" and "Technical"
- Alpine.js x-data: { audience: '@Model.SelectedAudience' }
- x-on:click updates audience and reloads page with query param
- Active button: bg-blue-600 text-white
- Inactive button: bg-gray-200 text-gray-700

**Implementation:**
```html
<div x-data="{ audience: '@Model.SelectedAudience' }" class="flex gap-2 mb-6">
    <a href="?audience=NonTechnical&category=@Model.SelectedCategory" 
       class="px-4 py-2 rounded-lg @(Model.SelectedAudience == "NonTechnical" ? "bg-blue-600 text-white" : "bg-gray-200")">
        Non-Technical
    </a>
    <a href="?audience=Technical&category=@Model.SelectedCategory"
       class="px-4 py-2 rounded-lg @(Model.SelectedAudience == "Technical" ? "bg-blue-600 text-white" : "bg-gray-200")">
        Technical
    </a>
</div>
```

Style with smooth transitions.
```

### Prompt 3.3: Create Category Navigation Sidebar
```
In Knowledge/Index.cshtml, add category sidebar:

**Categories List:**
1. Treatment Phases
2. Side Effects & Management
3. Lab Values Explained
4. Procedures
5. Recovery & Survivorship

**Implementation:**
- Vertical list of links
- Each link: ?audience=@Model.SelectedAudience&category={categoryName}
- Active category: bg-blue-50 border-l-4 border-blue-600
- Inactive: hover:bg-gray-50
- Icon for each category (emoji or SVG)

**Styling:**
- space-y-2
- Each item: px-4 py-3 rounded-r-lg
- Text: text-gray-700 font-medium

Add "All Categories" option at top to clear filter.
```

### Prompt 3.4: Build Article Cards Grid
```
In Knowledge/Index.cshtml, display articles as cards:

**Article Card:**
- Grid layout: grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6
- Each card shows:
  - Title (text-xl font-bold)
  - Category badge (bg-blue-100 text-blue-800 rounded-full px-3 py-1)
  - Audience badge (Technical: bg-purple-100, NonTechnical: bg-green-100)
  - Summary (first 150 chars, text-gray-600)
  - "Read More" link to Article detail page
  - AI disclaimer if IsAIGenerated

**Card Styling:**
- bg-white rounded-lg shadow hover:shadow-lg transition
- p-6
- Clickable: cursor-pointer

**Empty State:**
- If no articles: "No articles found for this category"
- Suggest changing filters
```

### Prompt 3.5: Create Article Detail Page
```
Create Pages/Knowledge/Article.cshtml and Article.cshtml.cs with:

**Article.cshtml.cs:**
- Inject KnowledgeService
- Property: KnowledgeArticleDto Article
- OnGet(Guid id) method:
  - Fetch article by ID
  - Return NotFound() if null

**Article.cshtml:**
- Back button to Knowledge Hub
- Article title (text-4xl font-bold)
- Category and audience badges
- Full content (whitespace-pre-line for paragraphs)
- Disclaimer at bottom if AI-generated
- Related articles section (same category)

**Styling:**
- Max-width: max-w-4xl mx-auto
- Content: prose prose-lg (if using Tailwind Typography)
- Spacing: space-y-6
- Disclaimer: bg-yellow-50 border-l-4 border-yellow-400 p-4
```

---

## Phase 4: Drugs Page

### Prompt 4.1: Create Drugs List Page
```
Create Pages/Drugs/Index.cshtml and Index.cshtml.cs with:

**Index.cshtml.cs:**
- Inject DrugService
- Property: List<DrugDetailDto> Drugs
- Property: string? SearchQuery
- OnGet(string? search) method:
  - Fetch all drugs
  - Filter by search query if provided (name contains search)

**Index.cshtml:**
- Search bar at top
- Drug cards in responsive grid
- Each card clickable to detail page

**Search Bar:**
- Form with GET method
- Input: name="search", placeholder="Search drugs..."
- Search button
- Clear button if search active

**Grid:**
- grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6
- Cards show: name, class, parent explanation (truncated)
```

### Prompt 4.2: Build Drug Cards
```
In Drugs/Index.cshtml, create drug cards:

**Card Structure:**
- Link to asp-page="Detail" asp-route-id="@drug.Id"
- Drug name (text-2xl font-bold text-gray-900)
- Drug class (text-sm text-gray-500 uppercase)
- Parent explanation (text-gray-600, line-clamp-2)
- "View Details" button (text-blue-600 hover:text-blue-800)
- Side effects count badge

**Styling:**
- bg-white rounded-lg shadow hover:shadow-xl transition
- p-6
- cursor-pointer
- Transform on hover: hover:scale-105

**Empty State:**
- If no drugs match search: "No drugs found"
- Show all drugs button
```

### Prompt 4.3: Create Drug Detail Page with Tabs
```
Create Pages/Drugs/Detail.cshtml and Detail.cshtml.cs with:

**Detail.cshtml.cs:**
- Inject DrugService
- Property: DrugDetailDto Drug
- OnGet(Guid id) method:
  - Fetch drug with side effects
  - Return NotFound() if null

**Detail.cshtml with Alpine.js Tabs:**
```html
<div x-data="{ activeTab: 'parent' }">
    <!-- Tab Buttons -->
    <div class="flex border-b">
        <button @click="activeTab = 'parent'" 
                :class="activeTab === 'parent' ? 'border-blue-600 text-blue-600' : 'border-transparent'"
                class="px-6 py-3 border-b-2 font-medium">
            Parent-Friendly
        </button>
        <button @click="activeTab = 'technical'"
                :class="activeTab === 'technical' ? 'border-blue-600 text-blue-600' : 'border-transparent'"
                class="px-6 py-3 border-b-2 font-medium">
            Technical
        </button>
    </div>

    <!-- Parent Tab Content -->
    <div x-show="activeTab === 'parent'" class="py-6">
        <h3>What This Drug Does</h3>
        <p>@Model.Drug.ParentFriendlyExplanation</p>
        
        <h3>Common Side Effects</h3>
        <ul>
            @foreach(var se in Model.Drug.SideEffects.Where(s => s.Severity == "Common"))
            {
                <li>@se.EffectName - @se.Description</li>
            }
        </ul>
    </div>

    <!-- Technical Tab Content -->
    <div x-show="activeTab === 'technical'" class="py-6">
        <h3>Mechanism of Action</h3>
        <p>@Model.Drug.MechanismOfAction</p>
        
        <h3>All Side Effects</h3>
        @foreach(var se in Model.Drug.SideEffects)
        {
            <div class="mb-4">
                <span class="badge-@se.Severity.ToLower()">@se.Severity</span>
                <strong>@se.EffectName</strong>
                <p>@se.Description</p>
                <small>Onset: @se.TypicalOnset</small>
            </div>
        }
        
        <h3>Expected Lab Changes</h3>
        <p>@Model.Drug.ExpectedLabChanges</p>
        
        <h3>Neurological Impacts</h3>
        <p>@Model.Drug.NeurologicalImpacts</p>
    </div>
</div>
```

Add back button and link to timeline events using this drug.
```

---

## Phase 5: Interactivity with Alpine.js

### Prompt 5.1: Add Alpine.js to Layout
```
In Pages/Shared/_Layout.cshtml, add before closing </body>:

```html
<script defer src="https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js"></script>
```

Alpine.js provides:
- x-data: Component state
- x-show: Conditional rendering
- x-on:click: Event handlers
- x-bind: Dynamic attributes
- x-model: Two-way binding
- x-transition: Smooth animations

Use for modals, tabs, dropdowns, filters, toggles.
```

### Prompt 5.2: Add Timeline Interactivity
```
Enhance Timeline.cshtml with Alpine.js:

**Timeline State:**
```html
<div x-data="{
    zoom: 'week',
    selectedCategory: 'all',
    showModal: false,
    selectedEvent: null,
    showAddForm: false,
    events: @Json.Serialize(Model.Events)
}">
```

**Filtered Events:**
```html
<template x-for="event in events.filter(e => selectedCategory === 'all' || e.category === selectedCategory)">
    <div @click="selectedEvent = event; showModal = true">
        <!-- Event marker -->
    </div>
</template>
```

**Modal with Transitions:**
```html
<div x-show="showModal" 
     x-transition:enter="transition ease-out duration-300"
     x-transition:leave="transition ease-in duration-200"
     @click.away="showModal = false">
    <!-- Modal content -->
</div>
```
```

---

## Phase 6: Styling & Polish

### Prompt 6.1: Enhance CSS
```
In wwwroot/css/site.css, add custom styles:

**Timeline Styles:**
```css
.timeline-container {
    position: relative;
    min-height: 600px;
    background: linear-gradient(to right, #f9fafb 0%, #ffffff 100%);
}

.phase-bar {
    height: 60px;
    border-radius: 8px;
    display: flex;
    align-items: center;
    padding: 0 16px;
    color: white;
    font-weight: 600;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.event-marker {
    position: absolute;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    transition: transform 0.2s, box-shadow 0.2s;
    background: white;
    box-shadow: 0 2px 8px rgba(0,0,0,0.15);
}

.event-marker:hover {
    transform: scale(1.2);
    box-shadow: 0 4px 12px rgba(0,0,0,0.25);
}
```

**Modal Animations:**
```css
.modal-backdrop {
    background: rgba(0, 0, 0, 0.5);
    backdrop-filter: blur(4px);
}

.modal-content {
    animation: slideUp 0.3s ease-out;
}

@keyframes slideUp {
    from {
        opacity: 0;
        transform: translateY(20px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
```

**Card Hover Effects:**
```css
.card-hover {
    transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}

.card-hover:hover {
    transform: translateY(-4px);
    box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.1);
}
```
```

### Prompt 6.2: Add Icons with Lucide
```
In Pages/Shared/_Layout.cshtml, add before Alpine.js:

```html
<script src="https://unpkg.com/lucide@latest"></script>
<script>
    document.addEventListener('alpine:init', () => {
        lucide.createIcons();
    });
</script>
```

Use Lucide icons:
- Timeline: <i data-lucide="calendar"></i>
- Knowledge: <i data-lucide="book-open"></i>
- Drugs: <i data-lucide="pill"></i>
- Add: <i data-lucide="plus"></i>
- Edit: <i data-lucide="edit"></i>
- Delete: <i data-lucide="trash-2"></i>
- Search: <i data-lucide="search"></i>
- Filter: <i data-lucide="filter"></i>

Replace emojis with Lucide icons for cleaner look.
```

---

## Phase 7: Demo Data & Testing

### Prompt 7.1: Create Demo Patient with Data
```
In OncoTimeline.Web/Program.cs, after seeding drugs, add:

```csharp
// Create demo patient
var demoPatientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
if (!context.Set<Patient>().Any(p => p.Id == demoPatientId))
{
    var patient = new Patient
    {
        Id = demoPatientId,
        FirstName = "Demo",
        LastName = "Patient",
        DateOfBirth = DateTime.UtcNow.AddYears(-8),
        DiagnosisDate = DateTime.UtcNow.AddMonths(-6),
        DiagnosisType = "B-ALL",
        RiskCategory = "Standard",
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };
    context.Set<Patient>().Add(patient);
    
    // Add treatment phases
    var phases = new List<TreatmentPhase>
    {
        new() {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Induction",
            Description = "Initial intensive treatment phase",
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow.AddMonths(-5),
            DisplayOrder = 1,
            Color = "#EF4444"
        },
        new() {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Consolidation",
            Description = "Strengthening treatment phase",
            StartDate = DateTime.UtcNow.AddMonths(-5),
            EndDate = DateTime.UtcNow.AddMonths(-2),
            DisplayOrder = 2,
            Color = "#F59E0B"
        },
        new() {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            Name = "Maintenance",
            Description = "Long-term maintenance therapy",
            StartDate = DateTime.UtcNow.AddMonths(-2),
            EndDate = null,
            DisplayOrder = 3,
            Color = "#10B981"
        }
    };
    context.Set<TreatmentPhase>().AddRange(phases);
    
    // Add sample timeline events
    var events = new List<TimelineEvent>
    {
        new() {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            TreatmentPhaseId = phases[0].Id,
            Title = "Vincristine Infusion",
            EventDate = DateTime.UtcNow.AddMonths(-6),
            Category = "Chemotherapy",
            Notes = "Day 1 of induction",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new() {
            Id = Guid.NewGuid(),
            PatientId = demoPatientId,
            TreatmentPhaseId = phases[0].Id,
            Title = "Lab Work - CBC",
            EventDate = DateTime.UtcNow.AddMonths(-6).AddDays(7),
            Category = "Lab",
            Notes = "Weekly blood count check",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        // Add 10-15 more events...
    };
    context.Set<TimelineEvent>().AddRange(events);
    
    // Add knowledge articles
    var articles = new List<AIKnowledgeArticle>
    {
        new() {
            Id = Guid.NewGuid(),
            Title = "Understanding Induction Therapy",
            Category = "TreatmentPhase",
            Audience = "NonTechnical",
            Content = "Induction therapy is the first phase...",
            Summary = "Learn about the first phase of treatment",
            IsAIGenerated = true,
            GeneratedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow
        },
        // Add 5-10 more articles...
    };
    context.Set<AIKnowledgeArticle>().AddRange(articles);
    
    context.SaveChanges();
}
```
```

### Prompt 7.2: Add Loading States
```
Create Pages/Shared/_LoadingSpinner.cshtml partial:

```html
<div class="flex items-center justify-center p-8">
    <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
</div>
```

Use in pages while data loads:
```html
@if (Model.Events == null)
{
    <partial name="_LoadingSpinner" />
}
else
{
    <!-- Content -->
}
```

Add skeleton screens for cards:
```html
<div class="animate-pulse">
    <div class="h-4 bg-gray-200 rounded w-3/4 mb-2"></div>
    <div class="h-4 bg-gray-200 rounded w-1/2"></div>
</div>
```
```

### Prompt 7.3: Add Error Handling
```
Create Pages/Shared/_ErrorMessage.cshtml partial:

```html
@model string

<div class="bg-red-50 border-l-4 border-red-400 p-4 mb-4">
    <div class="flex">
        <div class="flex-shrink-0">
            <i data-lucide="alert-circle" class="text-red-400"></i>
        </div>
        <div class="ml-3">
            <p class="text-sm text-red-700">@Model</p>
        </div>
    </div>
</div>
```

Use in PageModels:
```csharp
[TempData]
public string? ErrorMessage { get; set; }

public async Task<IActionResult> OnGetAsync()
{
    try
    {
        // Fetch data
    }
    catch (Exception ex)
    {
        ErrorMessage = "Failed to load data. Please try again.";
        return Page();
    }
}
```

Display in pages:
```html
@if (TempData["ErrorMessage"] != null)
{
    <partial name="_ErrorMessage" model="@TempData["ErrorMessage"]" />
}
```
```

---

## Success Criteria

✅ **Timeline Page**
- Horizontal scrollable timeline with phase colors
- Event markers with icons positioned by date
- Click event opens modal with full details
- Quick add floating button creates new events
- Zoom and filter controls work with Alpine.js
- Smooth animations and transitions

✅ **Knowledge Hub**
- Audience toggle filters articles (Technical/NonTechnical)
- Category sidebar navigation with active states
- Articles display in responsive grid with badges
- Article detail page shows full formatted content
- Search functionality works

✅ **Drugs Page**
- All drugs display in responsive card grid
- Search filters drugs by name
- Detail page has Technical/Parent tabs with Alpine.js
- Side effects listed and grouped by severity
- Clean, professional design

✅ **Navigation**
- All pages accessible from main nav
- Active page highlighted
- Responsive hamburger menu on mobile
- Footer with disclaimer on all pages

✅ **Polish**
- TailwindCSS styling throughout
- Alpine.js for all interactivity
- Lucide icons for visual elements
- Loading states and error handling
- Smooth animations (200-300ms)
- Responsive design (mobile, tablet, desktop)

---

## Implementation Order

1. **Phase 2**: Timeline page (Prompts 2.1-2.5) - 2-3 hours
2. **Phase 3**: Knowledge Hub (Prompts 3.1-3.5) - 2-3 hours
3. **Phase 4**: Drugs pages (Prompts 4.1-4.3) - 1-2 hours
4. **Phase 5**: Alpine.js interactivity (Prompts 5.1-5.2) - 1 hour
5. **Phase 6**: Styling polish (Prompts 6.1-6.2) - 1 hour
6. **Phase 7**: Demo data and testing (Prompts 7.1-7.3) - 1 hour

**Total: 8-12 hours of implementation**

---

## Running the Complete App

```bash
# Run the Razor Pages app (includes all services)
cd src/OncoTimeline.Web
dotnet run

# Visit: http://localhost:5173
```

The app uses:
- In-memory database (data resets on restart)
- Seeded drugs, demo patient, phases, events, articles
- Services injected directly into PageModels
- No separate API calls needed

---

## Key Advantages of Razor Pages

✅ **Simpler Architecture**: No separate API, services injected directly
✅ **Server-Side Rendering**: Fast initial page load, SEO-friendly
✅ **Type Safety**: C# throughout, compile-time checking
✅ **Less Code**: No API client, no state management library needed
✅ **Alpine.js**: Lightweight interactivity without heavy JS framework
✅ **Integrated**: Everything in one project, easier to deploy

---

## Notes for Amazon Q

- Services are already registered in Program.cs
- Use @inject directive in .cshtml files if needed
- PageModel properties are available in views via @Model
- Use asp-page, asp-route-* for navigation
- Alpine.js handles all client-side interactivity
- TailwindCSS via CDN (no build step needed)
- Lucide icons for professional look
- Demo patient ID: 00000000-0000-0000-0000-000000000001
