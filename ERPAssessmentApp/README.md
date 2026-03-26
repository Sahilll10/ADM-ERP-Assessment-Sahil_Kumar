# ADM Systems - ERP Development Assessment

## Project Overview
This is a Full Stack .NET Core MVC application developed for the ADM Systems ERP assessment. It features a secure authentication system, complete CRUD functionality for item inventory, and a parent/child processing engine with a recursive traceability tree.

## Technology Stack
* **Framework:** .NET 8.0 (ASP.NET Core MVC)
* **Language:** C#
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Frontend:** HTML5, CSS3, Bootstrap 5, Vanilla JS

## Steps to Run the Project Locally

### 1. Database Setup
1. Open SQL Server Management Studio (SSMS).
2. Connect to your local SQL Server instance.
3. Open a New Query window and execute the provided `DatabaseScript.sql` file to generate the `ERP_Assessment_DB` database and tables.
4. The script automatically seeds a default admin user:
   * **Email:** admin@admsystems.net
   * **Password:** password123

### 2. Application Setup
1. Open `ERPAssessmentApp.sln` in Visual Studio 2022.
2. Open the `appsettings.json` file.
3. Update the `DefaultConnection` string with your specific local SQL Server instance name (if different from standard local setups).
4. Open the Package Manager Console (`Tools > NuGet Package Manager > Package Manager Console`).
5. Run the following command to ensure Entity Framework tools are synced (optional if DB is already created via script, but good practice): `Update-Database`
6. Press `F5` or click the "Play" button in Visual Studio to build and run the application.

### 3. Usage Flow
* Log in using the seeded admin credentials.
* Navigate to the **Item Inventory** to Add, Edit, or Delete raw items.
* Click **Process** on an unprocessed item to break it down into multiple child items with specific weights.
* Click **View Hierarchy Tree** in the top navigation bar to view the recursive visual breakdown of all processed items.