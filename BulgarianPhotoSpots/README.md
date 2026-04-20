# BulgarianPhotoSpots 📸

BulgarianPhotoSpots is an advanced ASP.NET Core MVC web application designed for discovering, sharing, and reviewing the most beautiful photography locations across Bulgaria.

## 🌟 Key Features
- **Photo Spot Management**: Full CRUD operations for photography locations with category organization.
- **Reviews & Ratings**: Users can leave feedback and rate locations to help others.
- **Favorites System**: Authenticated users can save spots to their personal "My Favorites" list.
- [cite_start]**Search & Filtering**: Advanced filtering by location, category, and keyword search with built-in pagination. [cite: 39, 40]
- [cite_start]**Error Handling**: Custom user-friendly pages for 404 (Not Found) and 500 (Internal Server Error) status codes. [cite: 33, 34, 35]

## 🔐 Identity & Authorization
[cite_start]The system implements a robust **Role-Based Access Control (RBAC)** using ASP.NET Identity: [cite: 25, 26]
- **User**: Can manage their own spots, write reviews, and save favorites.
- **Administrator**: Has full access to manage all photo spots, reviews, and categories.

## 🏗 Architecture & Design
[cite_start]The project follows a **Clean Layered Architecture** to ensure loose coupling and high cohesion: [cite: 48, 52]
- [cite_start]**Web Layer**: Controllers, Razor Views, and UI logic. [cite: 12, 14]
- [cite_start]**Core Layer**: Business logic, Service interfaces, and ViewModels. [cite: 37]
- [cite_start]**Infrastructure Layer**: Data access with Entity Framework Core and SQL Server. [cite: 17, 19]

## 🛠 Setup & Database
1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Open **Package Manager Console** and run:
   ```powershell
   Update-Database