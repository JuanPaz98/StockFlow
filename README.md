# StockFlow - Inventory & Sales Management System 
StockFlow is a modern inventory and sales management system designed for small businesses. It helps users track stock levels, manage customer orders, and monitor pending payments in an efficient and organized way.

Features
✅ Product Inventory Management – Add, update, and track stock levels.
✅ Sales & Orders Tracking – Record and manage customer purchases.
✅ Customer Management – Store customer details and order history.
✅ Payment Status Tracking – Identify pending, paid, and partially paid orders.
✅ User Authentication – Secure login system with credentials.
✅ Reports & Insights – Generate reports for low stock, overdue payments, and sales performance.

Tech Stack
Backend: .NET 9 (C#), ASP.NET Core, Entity Framework Core
Database: SQL Server
Architecture: Clean Architecture, Repository Pattern, Unit of Work

Setup & Installation

1️⃣ Clone the repository:
  git clone https://github.com/your-username/StockFlow.git
  cd StockFlow
2️⃣ Configure the database connection string in appsettings.json.
3️⃣ Run the migrations and start the project:
  dotnet ef database update
  dotnet run
