# BulgarianPhotoSpots

## 📌 Description

A web application for managing photography locations in Bulgaria.  
Allows creating, editing, deleting, and viewing categories and photo spots.

The project demonstrates the use of layered architecture, separation of concerns, and best practices in ASP.NET Core MVC.

---

## 🚀 Running the Project

1. Clone the repository:


git clone <repo-url>


2. Open the project in Visual Studio

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
- Register
- Login
- Logout

---

## 🧱 Architecture

The project is structured into layers:

- **BulgarianPhotoSpots (Web)**  
  Controllers, Views, UI logic

- **BulgarianPhotoSpots.Core**  
  Interfaces, business logic, ViewModels

- **BulgarianPhotoSpots.Infrastructure**  
  DbContext, Entity Framework, services

---

## ⚙️ Features

### Categories
- Create
- Edit
- Delete
- Details

### PhotoSpots
- Create (with category dropdown)
- Edit
- Delete
- Details

---

## 🧪 Testing

The application has been tested manually by:

- creating records
- editing
- deleting
- validating input data
- testing invalid IDs (returns NotFound)

---

## 📦 Technologies

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Identity

---

## ✅ Status

The application is fully functional and ready for demonstration.
