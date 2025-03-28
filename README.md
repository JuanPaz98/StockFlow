# StockFlow - Inventory & Sales Management System 
StockFlow is a modern inventory and sales management system designed for small businesses. It helps users track stock levels, manage customer orders, and monitor pending payments in an efficient and organized way.

Features
<ul>
  <li>✅ Product Inventory Management – Add, update, and track stock levels. </li>
  <li>✅ Sales & Orders Tracking – Record and manage customer purchases.</li>
  <li>✅ Customer Management – Store customer details and order history.</li>
  <li>✅ Payment Status Tracking – Identify pending, paid, and partially paid orders.</li>
  <li>✅ User Authentication – Secure login system with credentials.</li>
  <li>✅ Reports & Insights – Generate reports for low stock, overdue payments, and sales performance.</li>
</ul>

Tech Stack
<ul>
  <li>Backend: .NET 9 (C#), ASP.NET Core, Entity Framework Core</li>
  <li>Database: SQL Server</li>
  <li>Architecture: Clean Architecture, Repository Pattern, Unit of Work</li>
</ul>

Setup & Installation

<ul>
  <li>
    1️⃣ Clone the repository: 
    - git clone https://github.com/your-username/StockFlow.git
    - cd StockFlow
  </li>
  <li>2️⃣ Configure the database connection string in appsettings.json.</li>
  <li>
    3️⃣ Run the migrations and start the project:
      - dotnet ef database update
      - dotnet run
  </li>
</ul>
