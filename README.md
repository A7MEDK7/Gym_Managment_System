# 🏋️ Power Fitness — Gym Management System

A full-featured **Gym Management System** built with **ASP.NET Core MVC (.NET 8)**, designed around clean, layered architecture and solid backend design patterns. The system manages gym members, trainers, subscription plans, class sessions, membership contracts, and session bookings — with a live analytics dashboard on top.

---

## 📖 Overview

Power Fitness is split into distinct modules, each responsible for one part of the gym's business:

| Module | Description |
|---|---|
| 🏠 **Dashboard** | Landing page with live gym stats: total members, active memberships, trainers count, and session status breakdown. |
| 👥 **Members** | Register and manage gym members, including photo, contact info, and a personal health record. |
| 🏋️ **Trainers** | Trainer profiles with a specialty (Yoga, CrossFit, Bodybuilding, Weight Loss, etc.). |
| 📅 **Sessions** | Gym classes (e.g. Yoga, Boxing) with a category, an assigned trainer, a time window, and a capacity. |
| 💳 **Plans** | Subscription plans (e.g. Basic, Premium, Annual) with price and duration. |
| 🎫 **Memberships** | The contract linking a Member to a Plan — defines when a subscription starts, ends, and whether it's active. |
| 📋 **Session Bookings** | The junction between Members and Sessions — members reserve a seat in a session, and admins track it. |
| 📊 **Analytics** | Real-time stats computed directly from the database (not static numbers). |

---

## 🏗️ Architecture

The solution follows an **N-Layer / Clean Architecture** approach, so that every layer has a single, clear responsibility and dependencies only ever point inward.

```
Gym_Managment_System/
│
├── GMS.Core/
│   ├── Domin/                      → Domain layer: entities, enums, repository & UoW contracts
│   ├── Service/                    → Business logic, implementations, AutoMapper profiles, Specifications
│   └── Service.Abstraction/        → Service interfaces (contracts) consumed by the presentation layer
│
├── GMS.Infrastructure/
│   ├── Presistence/                → EF Core DbContext, entity configurations, migrations, repositories
│   └── Presentation/                → Shared MVC presentation-layer controllers/helpers
│
├── Shared/
│   └── DTOs/                       → Data Transfer Objects grouped per module (Members, Trainers, Plans, Sessions, Analytics)
│
└── GMS.MVC/
    ├── Controllers/                → ASP.NET Core MVC controllers (talk only to the Service layer)
    ├── Views/                      → Razor views (CRUD pages per module)
    └── Program.cs                  → App startup, DI registration, middleware pipeline
```

**Why this structure?**
- The **Domain** layer has zero dependency on EF Core, ASP.NET, or any external framework — it only defines entities and contracts.
- The **Service** layer implements business rules against the abstractions, never against EF Core directly.
- The **MVC** layer never touches the database or entities directly — it only knows about DTOs and service interfaces.
- **DTOs** live in a shared project so the Domain entities never leak outside the Core.

---

## 🧩 Design Patterns & Techniques

- **Repository + Unit of Work** — a generic repository (`IGenericRepository<T>`) plus specific repositories (Plan, Session) coordinated through a single `IUnitOfWork`, so every request commits as one atomic operation.
- **Specification Pattern** — encapsulates filtering/including/ordering logic (e.g. `MemberWithHealthRecordSpecification`, `MemberWitSessionSpecification`) so repositories stay generic and query logic stays testable and reusable.
- **Service Manager Pattern** — a single `IServiceManger` exposes `MemberService`, `TrainerService`, `PlanService`, `SessionService`, and `AnalyticsService`, so controllers depend on one entry point instead of five separate services.
- **DTO Pattern** — dedicated DTOs per operation (e.g. `CreateMemberDTO`, `MemberToUpdateDTO`, `MemberDetailsDTO`) instead of exposing entities to the views.
- **AutoMapper** — maps entities ↔ DTOs through dedicated profiles (`MemberProfile`, `TrainerProfile`, `PlanProfile`, `SessionProfile`).
- **Fluent API Configurations** — each entity has its own `IEntityTypeConfiguration<T>` class instead of data annotations, keeping the entities clean.
- **Attachment Service** — a standalone service (`IAttachmentService`) responsible for uploading and deleting files (used for member/trainer photos), decoupled from the rest of the business logic.
- **DB Initializer** — seeds the database on startup through `IDbInitilazer`, applying pending migrations automatically.

---

## 🗂️ Domain Model

**Base entities**
- `BaseEntity` — `Id`, `CreatedAt`, `UpdatedAt` (inherited by every entity).
- `GymUser` (abstract) — shared identity fields for people in the system: `Name`, `Email`, `Phone`, `DateOfBirth`, `Gender`, `Address`. Inherited by both `Member` and `Trainer`.

**Core entities**
- **Member** *(inherits GymUser)* — has a `Photo`, one `HealthRecord`, and many `MemberSession` bookings.
- **Trainer** *(inherits GymUser)* — has a `Specialties` enum value and many `Session`s they lead.
- **HealthRecord** — `Height`, `Weight`, `BloodType`, optional `Note`.
- **Category** — groups sessions (e.g. Boxing, CrossFit).
- **Session** — `Description`, `Capacity`, `StartDate`, `EndDate`, linked to one `Category` and one `Trainer`.
- **Plan** — `Name`, `Description`, `DurationDays`, `Price`, `IsActive` toggle.
- **MemberShip** — the contract between a `Member` and a `Plan`: `EndDate` and a computed `Status` (`Active` / `Expired`, based on `EndDate` vs. now).
- **MemberSession** — the booking record linking a `Member` to a `Session`.

**Enums**
- `Gender` — Male / Female.
- `BloodType` — all 8 blood types (A+, A-, B+, B-, AB+, AB-, O+, O-).
- `Specialties` — 11 trainer specialties (General Fitness, Weight Loss & Fat Burning, Bodybuilding, Powerlifting, Cardio, CrossFit, Rehabilitation, Sports Performance, Nutrition Coaching, Youth Fitness, Seniors Fitness).

**Business rules captured in the model**
- A session's status (Upcoming / Ongoing / Completed) is derived from comparing `StartDate`/`EndDate` to the current time — not stored as a static flag.
- A membership's `Active`/`Expired` status is computed on the fly from `EndDate`, so it's always accurate.
- Sessions have a bounded `Capacity`, enforced at the booking (`MemberSession`) level.

---

## 🛠️ Tech Stack

- **.NET 8** / **ASP.NET Core MVC**
- **Entity Framework Core 8** (Code-First, Fluent API, Migrations)
- **SQL Server**
- **AutoMapper**
- **Razor Views** with Tag Helpers, partial views, and a shared `_Layout`
- **Bootstrap** for the UI

---

## 📊 Analytics Dashboard

The `AnalyticsService` computes live statistics on every dashboard load, straight from the database, with no cached or static values:

- Active members (based on membership `EndDate`)
- Total members
- Total trainers
- Upcoming sessions
- Ongoing sessions
- Completed sessions

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (local or containerized)

## 🧭 Modules & Controllers

| Controller | Responsibilities |
|---|---|
| `HomeController` | Dashboard with live analytics |
| `MembersController` | List, register, view details, view health record, edit, and delete members |
| `TrainersController` | List, register, view details, edit, and delete trainers |
| `PlansController` | List plans, view details, edit, and toggle plan active status |
| `SessionController` | List, create, view details, edit, and delete sessions; populates trainer/category dropdowns |

---

## 🔒 What's Not Included (By Design)

- **Authentication & Authorization** (ASP.NET Core Identity, role-based access, login/logout, access-denied handling) — deliberately left out of this build since it's a flow that's already been implemented extensively in prior projects. The app currently runs without a login wall.

---

> ⚠️ **Note:** This project intentionally does **not** include an Authentication & Authorization module (ASP.NET Core Identity, roles, login/logout). That module was skipped on purpose since it had already been implemented multiple times in previous projects — the focus here is on the domain, architecture, and business logic instead.

## 📌 Possible Next Steps

- Add ASP.NET Core Identity with role-based access (Admin / Trainer / Member)
- Add unit tests for the Service layer
- Add API endpoints alongside the MVC views
- Add pagination & search/filtering across list views
- Add session capacity validation and double-booking prevention at the service layer

---

## 📄 License

This project is open for learning and personal portfolio purposes.
