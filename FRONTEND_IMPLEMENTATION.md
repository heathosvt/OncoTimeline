# OncoTimeline Frontend Implementation Guide

## Overview
This guide provides step-by-step prompts to implement the OncoTimeline React frontend. The backend API is complete and running. Follow these prompts in order with Amazon Q.

---

## Phase 1: Project Setup & Foundation

### Prompt 1.1: Initialize React + Vite Project
```
In /Users/tvenere/Desktop/Repos/OncoTimeline/src/OncoTimeline.Web/, initialize a new React + Vite project with TypeScript. Use the following stack:
- React 18+
- TypeScript
- Vite
- TailwindCSS for styling
- React Router for navigation
- Axios for API calls
- Zustand for state management

Configure the project to run on port 5173 and proxy API calls to http://localhost:5000.
Create a clean folder structure:
- src/components/ (reusable UI components)
- src/pages/ (page components)
- src/services/ (API service layer)
- src/store/ (Zustand stores)
- src/types/ (TypeScript interfaces)
- src/utils/ (helper functions)
```

### Prompt 1.2: Create TypeScript Types
```
Create TypeScript interfaces in src/types/ for all API entities based on the backend DTOs:
- TimelineEvent (with drugs, phase info)
- Drug (with side effects)
- KnowledgeArticle (with audience: Technical/NonTechnical)
- TreatmentPhase (with color, dates)
- Patient

Match the exact structure returned by these API endpoints:
- GET /api/timeline/patient/{id}
- GET /api/drugs
- GET /api/knowledge
- GET /api/phases/patient/{id}
```

### Prompt 1.3: Create API Service Layer
```
Create an API service in src/services/api.ts using Axios with:
- Base URL: http://localhost:5000/api
- Methods for all endpoints:
  - Timeline: getPatientTimeline, getTimelineByDateRange, createEvent, updateEvent, deleteEvent
  - Drugs: getAllDrugs, getDrugById, getDrugByName
  - Knowledge: getAllArticles, getByCategory, getByAudience
  - Phases: getPatientPhases, createPhase
- Error handling with try/catch
- TypeScript return types for all methods
```

---

## Phase 2: Premium Timeline (PRIMARY FEATURE)

### Prompt 2.1: Create Timeline Page Layout
```
Create src/pages/TimelinePage.tsx with:
- Full-width layout
- TimelineControls component at top (zoom buttons, date range picker, category filters)
- PremiumTimeline component in center (horizontal scrollable canvas)
- QuickAddEvent floating action button (bottom right)

Use TailwindCSS for a clean, modern design. The timeline should take up most of the viewport height.
```

### Prompt 2.2: Build Premium Timeline Component
```
Create src/components/PremiumTimeline.tsx with:
- Horizontal scrollable container (overflow-x-auto)
- PhaseBar showing treatment phases (Induction, Consolidation, Maintenance) with color coding
- EventMarkers positioned on timeline by date
- Smooth scroll behavior
- Click on event marker opens EventDetail modal
- Zoom levels: Day, Week, Month, Full Treatment (controlled by parent)

Use these colors for phases:
- Induction: #EF4444 (red)
- Consolidation: #F59E0B (amber)
- Maintenance: #10B981 (green)
```

### Prompt 2.3: Create Event Markers with Icons
```
Create src/components/EventMarker.tsx with:
- Icon based on category (💉 chemo, 🏥 hospital, 🔬 labs, 📝 notes, 😷 symptom)
- Colored dot indicator matching phase color
- Tooltip on hover showing event title and date
- Click handler to open detail modal
- Smooth animations on hover

Use Lucide React for icons if emojis don't work well.
```

### Prompt 2.4: Build Event Detail Modal
```
Create src/components/EventDetail.tsx modal with:
- Event title, date, and category
- Associated drugs with dosages and routes
- Lab results (if applicable)
- Notes section
- Edit and Delete buttons
- Close button (X in top right)

Use a clean card design with proper spacing. Modal should overlay the timeline with a backdrop.
```

### Prompt 2.5: Create Quick Add Event Form
```
Create src/components/QuickAddEvent.tsx with:
- Floating action button (+ icon, bottom right, fixed position)
- Click opens modal with form:
  - Title (text input)
  - Date (date picker)
  - Category (dropdown: Chemotherapy, Lab, Hospital, Symptom, Note)
  - Treatment Phase (dropdown, optional)
  - Drugs (multi-select with dosage/route inputs)
  - Notes (textarea)
  - Save and Cancel buttons

Form should POST to /api/timeline and refresh the timeline on success.
```

---

## Phase 3: Knowledge Hub (CORE FEATURE)

### Prompt 3.1: Create Knowledge Hub Page Layout
```
Create src/pages/KnowledgeHubPage.tsx with:
- AudienceToggle at top (Technical / Non-Technical switch)
- CategoryNav sidebar (Treatment Phases, Side Effects, Lab Values, Procedures, Recovery)
- ContentArea main section showing filtered articles
- Search bar to filter articles by title

Use a 2-column layout: sidebar (20%) + content (80%).
```

### Prompt 3.2: Build Audience Toggle Component
```
Create src/components/AudienceToggle.tsx with:
- Toggle switch (Technical ↔ Non-Technical)
- Clear visual indication of selected audience
- Updates Zustand store on change
- Filters articles by audience field

Use a pill-style toggle with smooth transition animation.
```

### Prompt 3.3: Create Category Navigation
```
Create src/components/CategoryNav.tsx with:
- Vertical list of categories:
  1. Treatment Phases
  2. Side Effects & Management
  3. Lab Values Explained
  4. Procedures
  5. Recovery & Survivorship
- Active category highlighted
- Click updates selected category in store
- Icon for each category

Use clean, accessible design with hover states.
```

### Prompt 3.4: Build Article Card Component
```
Create src/components/ArticleCard.tsx with:
- Article title
- Category badge
- Audience badge (Technical/Non-Technical)
- Summary (first 150 characters)
- "Read More" button
- AI-generated disclaimer if applicable

Cards should be in a responsive grid (2-3 columns depending on screen size).
```

### Prompt 3.5: Create Article Detail View
```
Create src/components/ArticleDetail.tsx with:
- Full article title
- Category and audience badges
- Complete content (formatted with proper paragraphs)
- Disclaimer at bottom (if AI-generated)
- Back button to return to list

Use readable typography with proper line height and spacing.
```

---

## Phase 4: Drugs Page

### Prompt 4.1: Create Drugs Page Layout
```
Create src/pages/DrugsPage.tsx with:
- Search bar at top
- DrugList component showing all drugs in cards
- Click on drug card opens DrugDetail modal

Use a responsive grid layout for drug cards.
```

### Prompt 4.2: Build Drug Card Component
```
Create src/components/DrugCard.tsx with:
- Drug name (large, bold)
- Drug class (subtitle)
- Parent-friendly explanation (truncated to 2 lines)
- "View Details" button
- Clean card design with hover effect

Cards should be clickable to open detail modal.
```

### Prompt 4.3: Create Drug Detail Modal with Tabs
```
Create src/components/DrugDetail.tsx modal with:
- Two tabs: "Parent-Friendly" and "Technical"
- Parent-Friendly tab shows:
  - Simple explanation
  - What to watch for
  - Common side effects (non-technical language)
- Technical tab shows:
  - Mechanism of action
  - Pharmacology details
  - All side effects with severity
  - Expected lab changes
  - Neurological impacts

Use tab navigation with smooth transitions between views.
```

---

## Phase 5: Navigation & Layout

### Prompt 5.1: Create Main Navigation
```
Create src/components/Navigation.tsx with:
- Logo/App name on left
- Navigation links: Timeline, Knowledge Hub, Drugs
- Active route highlighted
- Responsive design (hamburger menu on mobile)

Use a clean, modern navbar with proper spacing.
```

### Prompt 5.2: Create App Layout Component
```
Create src/components/Layout.tsx with:
- Navigation at top
- Main content area
- Consistent padding and max-width
- Footer with disclaimer

Wrap all pages in this layout component.
```

### Prompt 5.3: Setup React Router
```
In src/App.tsx, setup React Router with routes:
- / → TimelinePage
- /knowledge → KnowledgeHubPage
- /drugs → DrugsPage

Use Layout component to wrap all routes.
```

---

## Phase 6: State Management

### Prompt 6.1: Create Timeline Store
```
Create src/store/timelineStore.ts using Zustand with:
- events: TimelineEvent[]
- selectedPatientId: string
- zoomLevel: 'day' | 'week' | 'month' | 'full'
- selectedCategory: string | null
- Actions: fetchEvents, addEvent, updateEvent, deleteEvent, setZoomLevel, setCategory
```

### Prompt 6.2: Create Knowledge Store
```
Create src/store/knowledgeStore.ts using Zustand with:
- articles: KnowledgeArticle[]
- selectedAudience: 'Technical' | 'NonTechnical'
- selectedCategory: string | null
- Actions: fetchArticles, setAudience, setCategory, getFilteredArticles
```

### Prompt 6.3: Create Drugs Store
```
Create src/store/drugsStore.ts using Zustand with:
- drugs: Drug[]
- searchQuery: string
- Actions: fetchDrugs, setSearchQuery, getFilteredDrugs
```

---

## Phase 7: Polish & UX

### Prompt 7.1: Add Loading States
```
Add loading spinners to all data-fetching components:
- Timeline loading skeleton
- Article cards loading skeleton
- Drug cards loading skeleton

Use TailwindCSS animate-pulse for skeleton screens.
```

### Prompt 7.2: Add Error Handling
```
Create src/components/ErrorMessage.tsx component and add error states to:
- API call failures
- Form validation errors
- Network errors

Show user-friendly error messages with retry options.
```

### Prompt 7.3: Add Animations
```
Add smooth animations using Framer Motion or CSS transitions:
- Page transitions
- Modal open/close
- Card hover effects
- Timeline scroll
- Tab switches

Keep animations subtle and fast (200-300ms).
```

### Prompt 7.4: Make Responsive
```
Ensure all components are responsive:
- Timeline: horizontal scroll on mobile, full width on desktop
- Knowledge Hub: stack sidebar on mobile
- Drug cards: 1 column mobile, 2-3 columns desktop
- Navigation: hamburger menu on mobile

Test on mobile (375px), tablet (768px), and desktop (1440px) viewports.
```

---

## Phase 8: Testing & Integration

### Prompt 8.1: Test API Integration
```
Test all API endpoints work correctly:
1. Start backend: cd src/OncoTimeline.API && dotnet run
2. Start frontend: cd src/OncoTimeline.Web && npm run dev
3. Verify:
   - Timeline loads events
   - Knowledge Hub filters by audience/category
   - Drugs page shows all drugs
   - Forms submit successfully
   - Modals open/close properly
```

### Prompt 8.2: Add Sample Data
```
Create a script to populate the API with sample data:
- 3 treatment phases for a test patient
- 10-15 timeline events across different categories
- 5-10 knowledge articles (mix of Technical/NonTechnical)
- Drugs are already seeded

This helps demonstrate the full app functionality.
```

### Prompt 8.3: Create Demo Patient
```
Add a demo patient to the in-memory database on startup:
- Patient ID: fixed GUID for easy testing
- Name: "Demo Patient"
- Diagnosis: B-ALL
- Treatment phases: Induction (red), Consolidation (amber), Maintenance (green)

Use this patient ID throughout the app for demo purposes.
```

---

## Success Criteria

✅ **Timeline Page**
- Horizontal scrollable timeline with phase colors
- Event markers with icons
- Click event opens detail modal
- Quick add button creates new events
- Zoom and filter controls work

✅ **Knowledge Hub**
- Audience toggle filters articles
- Category navigation works
- Articles display with proper formatting
- Search filters by title

✅ **Drugs Page**
- All drugs display in cards
- Search filters drugs
- Detail modal shows Technical/Parent tabs
- Side effects listed by severity

✅ **Navigation**
- All routes work
- Active route highlighted
- Responsive on mobile

✅ **Polish**
- Loading states on all data fetches
- Error messages for failures
- Smooth animations
- Responsive design

---

## Implementation Order

1. **Day 1**: Setup project, types, API service (Prompts 1.1-1.3)
2. **Day 2**: Timeline page and components (Prompts 2.1-2.5)
3. **Day 3**: Knowledge Hub (Prompts 3.1-3.5)
4. **Day 4**: Drugs page (Prompts 4.1-4.3)
5. **Day 5**: Navigation, routing, layout (Prompts 5.1-5.3)
6. **Day 6**: State management (Prompts 6.1-6.3)
7. **Day 7**: Polish and UX (Prompts 7.1-7.4)
8. **Day 8**: Testing and integration (Prompts 8.1-8.3)

---

## Notes for Amazon Q

- Backend API is running at http://localhost:5000
- All endpoints are documented at http://localhost:5000/swagger
- Use the exact API response structures for TypeScript types
- Follow the color scheme from ARCHITECTURE.md
- Prioritize Timeline and Knowledge Hub (primary features)
- Keep code clean and minimal
- Use modern React patterns (hooks, functional components)
- TailwindCSS for all styling
- TypeScript for type safety
