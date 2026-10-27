# Simple Issue Tracker

A full-stack Issue Tracking application built with **Angular** and **.NET 8**, implementing **Clean Architecture**, **CQRS**, **MediatR**, **FluentValidation**, and **Entity Framework Core**. The application allows users to manage issues/tasks through a modern web interface with filtering, searching, and CRUD operations.

---

## Overview

The Simple Issue Tracker enables users to:

- Create new issues
- View issues in a searchable and filterable grid
- Update existing issues
- Delete issues with confirmation
- Filter by status, priority, and keyword search
- Persist data using SQLite or EF Core In-Memory Database

The solution demonstrates modern enterprise development practices including:

- Clean Architecture
- CQRS Pattern
- MediatR
- FluentValidation
- Entity Framework Core
- RESTful APIs
- Angular Reactive Forms
- Angular Material UI Components

---

## Technology Stack

### Frontend

| Technology | Version |
|------------|---------|
| Angular | Latest Stable |
| TypeScript | Latest Stable |
| Angular Material | Latest Stable |
| RxJS | Latest Stable |
| Reactive Forms | Angular Built-in |

### Backend

| Technology | Version |
|------------|---------|
| .NET | 8 |
| ASP.NET Core Web API | 8 |
| MediatR | Latest |
| FluentValidation | Latest |
| Entity Framework Core | Latest |
| SQLite / InMemory DB | Latest |

---

## Solution Architecture

### Backend Architecture (Clean Architecture)

```text
src/
│
├── IssueTracker.API
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── IssueTracker.Application
│   ├── Features
│   │   └── Issues
│   │       ├── Commands
│   │       │   ├── CreateIssue
│   │       │   ├── UpdateIssue
│   │       │   └── DeleteIssue
│   │       │
│   │       └── Queries
│   │           ├── GetIssues
│   │           └── GetIssueById
│   │
│   ├── DTOs
│   ├── Interfaces
│   ├── Behaviors
│   └── Validators
│
├── IssueTracker.Domain
│   ├── Entities
│   ├── Enums
│   └── Common
│
└── IssueTracker.Infrastructure
    ├── Persistence
    ├── Configurations
    └── Repositories
```

---

## Frontend Architecture

```text
src/app
│
├── core
│   ├── services
│   └── models
│
├── features
│   └── issues
│       ├── issue-list
│       ├── issue-dialog
│       ├── issue-form
│       └── issue-delete-dialog
│
├── shared
│   ├── components
│   └── material
│
└── app-routing.module.ts
```

---

## Domain Model

### IssueItem

```csharp
public class IssueItem
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public Priority Priority { get; set; }

    public Status Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
```

### Priority Enum

```csharp
public enum Priority
{
    Low,
    Medium,
    High
}
```

### Status Enum

```csharp
public enum Status
{
    Open,
    InProgress,
    Done
}
```

---

## Features

### Issue Management

✅ Create Issue

✅ Update Issue

✅ Delete Issue

✅ View Issue Details

✅ List All Issues

---

### Filtering & Search

Users can filter issues using:

- Status
  - Open
  - In Progress
  - Done

- Priority
  - Low
  - Medium
  - High

- Keyword Search
  - Title
  - Description

Example:

```text
Open + High + "login"
```

Returns all high-priority open issues containing "login".

---

## CQRS Implementation

### Commands

| Command | Purpose |
|----------|----------|
| CreateIssueCommand | Create issue |
| UpdateIssueCommand | Update issue |
| DeleteIssueCommand | Delete issue |

### Queries

| Query | Purpose |
|--------|----------|
| GetIssuesQuery | Retrieve issues with filtering |
| GetIssueByIdQuery | Retrieve issue details |

### MediatR Flow

```text
Controller
    ↓
MediatR
    ↓
Command/Query
    ↓
Handler
    ↓
Repository / DbContext
    ↓
Database
```

---

## API Endpoints

### Get All Issues

```http
GET /api/issues
```

Optional Query Parameters:

```http
GET /api/issues?status=Open
GET /api/issues?priority=High
GET /api/issues?search=login
GET /api/issues?status=Open&priority=High&search=bug
```

---

### Get Issue By Id

```http
GET /api/issues/{id}
```

---

### Create Issue

```http
POST /api/issues
```

Request:

```json
{
  "title": "Login page error",
  "description": "Error while submitting login form",
  "priority": "High",
  "status": "Open"
}
```

---

### Update Issue

```http
PUT /api/issues/{id}
```

Request:

```json
{
  "title": "Updated title",
  "description": "Updated description",
  "priority": "Medium",
  "status": "InProgress"
}
```

---

### Delete Issue

```http
DELETE /api/issues/{id}
```

---

## Validation Rules

Implemented using **FluentValidation**.

### Create Issue

| Field | Rule |
|---------|---------|
| Title | Required |
| Title | Max 100 chars |
| Description | Max 500 chars |

### Update Issue

| Field | Rule |
|---------|---------|
| Id | Required |
| Title | Required |
| Title | Max 100 chars |
| Description | Max 500 chars |

---

## Error Handling

Global exception middleware handles:

| Status Code | Description |
|-------------|------------|
| 200 | Success |
| 201 | Created |
| 400 | Validation Error |
| 404 | Resource Not Found |
| 500 | Unhandled Exception |

Example Error Response:

```json
{
  "message": "Validation failed",
  "errors": [
    "Title is required"
  ]
}
```

---

## Frontend Features

### Issue Grid

Angular Material Table displaying:

- Title
- Description
- Priority
- Status
- Created Date
- Updated Date
- Actions

---

### Actions

| Action | Description |
|---------|-------------|
| Add | Create Issue |
| Edit | Update Issue |
| Delete | Remove Issue |
| Filter | Filter Issues |
| Search | Search Issues |

---

### Forms

Implemented using:

- Angular Reactive Forms
- Angular Material Inputs
- Angular Material Select
- Angular Material Dialog

Validation:

- Required fields
- Client-side validation messages
- Disable submit when invalid

---

## Database

### Option 1: InMemory Database

Suitable for:

- Coding assessments
- Demo environments
- Rapid development

### Option 2: SQLite

Suitable for:

- Local persistence
- Portfolio projects
- Demonstrations

Connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=IssueTracker.db"
  }
}
```

---

## Running the Application

### Prerequisites

- .NET 8 SDK
- Node.js 20+
- Angular CLI
- Visual Studio 2022 / VS Code

---

### Backend Setup

Navigate to API project:

```bash
cd src/IssueTracker.API
```

Restore packages:

```bash
dotnet restore
```

Run application:

```bash
dotnet run
```

Backend URL:

```text
https://localhost:5001
```

Swagger:

```text
https://localhost:5001/swagger
```

---

### Frontend Setup

Navigate to Angular application:

```bash
cd issue-tracker-ui
```

Install dependencies:

```bash
npm install
```

Run Angular:

```bash
ng serve
```

Frontend URL:

```text
http://localhost:4200
```

---

## CORS Configuration

Backend must allow Angular development server:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
        policy =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithOrigins("http://localhost:4200");
        });
});
```

---

## Future Enhancements

- Authentication & Authorization (JWT)
- Pagination
- Sorting
- Unit Tests
- Integration Tests
- Audit Logs
- Comments
- Attachments
- User Assignment
- Dashboard Metrics
- Dark Mode

---

## Evaluation Criteria

### Frontend

- Angular best practices
- Component design
- Reactive forms
- API integration
- Filtering implementation
- Clean UI/UX
- Readable code

### Backend

- Proper CQRS implementation
- MediatR usage
- Clean Architecture adherence
- Validation handling
- API design
- Repository pattern usage
- Error handling

### General

- Working end-to-end functionality
- Separation of concerns
- Code quality
- Maintainability
- Naming conventions
- Documentation quality

---

## Expected Deliverables

- Source code repository
- Angular frontend application
- .NET 8 Web API
- README documentation
- Sample data seeding
- Working CRUD functionality
- Filtering/search implementation
- CQRS with MediatR implementation

---

## License

This project is intended as a technical assessment or learning exercise demonstrating Angular and .NET 8 full-stack development using Clean Architecture and CQRS principles.
