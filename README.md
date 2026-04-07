# BulgarianPhotoSpots

## 📌 Description

A web application for managing photography locations in Bulgaria.
It allows users to create, edit, delete, and explore photo spots and categories.

The project demonstrates clean layered architecture, separation of concerns, and good practices in ASP.NET Core MVC.

---

## 🚀 Running the Project

1. Clone the repository:

git clone <repo-url>

2. Open the solution in Visual Studio

3. Set up the database:

* Open **Package Manager Console**
* Run:

Add-Migration InitialCreate
Update-Database

4. Run the project:

Ctrl + F5

---

## 🔐 Authentication

The application uses ASP.NET Identity.

Supported features:

* Register
* Login
* Logout

---

## 🧱 Architecture

The project follows a layered architecture:

* **BulgarianPhotoSpots (Web)**
  Controllers, Views, UI logic

* **BulgarianPhotoSpots.Core**
  Interfaces, business logic, ViewModels

* **BulgarianPhotoSpots.Infrastructure**
  DbContext, Entity Framework, services

---

## ⚙️ Features

### Categories

* Create
* Edit
* Delete
* Details

### PhotoSpots

* Create (with category selection)
* Edit
* Delete
* Details

### Additional

* Search and filtering
* Pagination
* Favorites
* Reviews and ratings
* Admin area
* Error handling (404 / 500 pages)

---

## 🧪 Testing

The project includes unit tests for the service layer:

* PhotoSpotService tests
* In-memory database usage
* Covers basic CRUD operations

Manual testing includes:

* creating records
* editing
* deleting
* validation checks
* handling invalid IDs (NotFound)

---

## 📦 Technologies

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* xUnit (Unit Testing)

---

## ✅ Status

The application is fully functional and ready for demonstration.
