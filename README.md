BulgarianPhotoSpots 📸
BulgarianPhotoSpots is an advanced ASP.NET Core MVC web application designed for discovering, sharing, and reviewing the most beautiful photography locations across Bulgaria.

🌟 Key Features
Photo Spot Management: Full CRUD operations for photography locations with category organization.

Personal User Profiles: Users can customize their presence with a DisplayName, personal Bio, and a custom Profile Picture.

Local File Upload System: Integrated image processing that allows users to upload profile photos directly from their computers (stored in wwwroot/images/profiles).

Interactive Favorites: Save and manage locations with a dynamic "Heart" toggle system and a dedicated personal gallery.

Community Reviews: Users can leave ratings (1-10) and feedback. Includes advanced sorting logic (Newest, Highest, and Lowest rating).

Search & Filtering: Advanced filtering by location, category, and keyword search with built-in pagination.

Error Handling: Custom user-friendly pages for 404 (Not Found) and 500 (Internal Server Error) status codes.

🔐 Identity & Authorization
The system implements a robust Role-Based Access Control (RBAC) using ASP.NET Identity:

User: Can manage their own profile, save favorites, and write reviews.

Administrator: Has full access to manage all photo spots, reviews, and categories.

🏗 Architecture & Design
The project follows a Clean Layered Architecture to ensure loose coupling and high cohesion:

Web Layer: Controllers, Razor Views with Bootstrap 5, and custom CSS animations.

Core Layer: Business logic, Service interfaces, and dedicated ViewModels to ensure data integrity.

Infrastructure Layer: Data access with Entity Framework Core, Repository patterns, and SQL Server migrations.

🛠 Setup & Database
Clone the repository.

Update the connection string in appsettings.json.

Ensure the folder wwwroot/images/profiles exists (used for profile picture uploads).

Open Package Manager Console and run the following to apply the latest schema (including the Profile updates):

PowerShell
Update-Database
Run the application.
