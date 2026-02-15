# BulgarianPhotoSpots

BulgarianPhotoSpots is an ASP.NET Core MVC web application for managing and exploring photo locations in Bulgaria.

The project demonstrates clean layered architecture, Service layer abstraction, Dependency Injection and asynchronous database access using Entity Framework Core.

---

## Features

- Create, Edit, Delete and View Photo Spots
- Categories for organizing photo locations
- Strongly typed ViewModels
- Clean Service Layer implementation
- Dependency Injection
- Fully asynchronous data operations
- Optimized read queries using AsNoTracking
- Proper PRG (Post-Redirect-Get) pattern
- MVC pattern separation of concerns

---

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Dependency Injection
- Layered Architecture
- Razor Views

---

## Architecture

The project follows a layered structure:

- Controllers → Handle HTTP requests
- Services → Business logic and data access abstraction
- Data Layer → Entity Framework DbContext
- ViewModels → UI-specific data models

No direct DbContext usage inside Controllers.

---

## Database

The database is managed with Entity Framework Core Migrations.

To create the database locally:

Add-Migration InitialCreate
Update-Database


---

## Project Status

Completed implementation of:

- Controllers & Routing
- Razor Views & Layouts
- Model Binding & Validation
- URLs and Views
- Layers, Services, DI and Asynchronous Processing

The project structure is prepared for future extension such as Authentication and Role-based Authorization.
