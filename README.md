# 🏋️ Gym OS - Modern Gym Management System

![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-purple)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blue)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-green)
![Status](https://img.shields.io/badge/Status-Completed-success)

**Gym OS** is a comprehensive, web-based management solution designed for modern fitness centers. It streamlines member onboarding, subscription plan management, and revenue tracking through a secure, role-based dashboard.

## Screenshots
> <img width="1896" height="473" alt="image" src="https://github.com/user-attachments/assets/b32b528b-50fc-406b-82d7-1d3d4d4c95a1" />
<img width="1898" height="520" alt="image" src="https://github.com/user-attachments/assets/8cf7c704-aa0c-4678-b4d8-758872a51c6e" />
<img width="1859" height="870" alt="image" src="https://github.com/user-attachments/assets/3127e59b-c40d-41c3-a16b-a1e4129d1bd3" />





## 🚀 Key Features

### 1. 📊 Admin Dashboard
* **Real-Time Analytics:** Instantly view total active members and projected monthly revenue.
* **Visual Metrics:** Clean, card-based layout for quick operational insights.

### 2. 👥 Member Management
* **Relational Onboarding:** Dynamic dropdowns link members to specific pricing tiers (Plans) stored in the database.
* **Profile Management:** Full CRUD (Create, Read, Update, Delete) capabilities.
* **Image Uploads:** Custom profile photo support handled via server-side file management.
* **Instant Search:** JavaScript-powered live filtering to find members by name without page reloads.

### 3. 💳 Plan & Subscription Management
* **Dynamic Pricing Tiers:** Create and modify subscription plans (e.g., Gold, Silver, Platinum).
* **Automatic Formatting:** Fees are handled with currency precision (`decimal(18,2)`) for financial accuracy.

### 4. 🔒 Security & Architecture
* **Role-Based Access Control:** Public landing page for visitors; secure dashboard for Admins.
* **Tech Stack:** Built on **ASP.NET Core MVC 8.0** using **Entity Framework Core** and **SQL Server LocalDB**.
* **Validation:** robust client-side and server-side validation to prevent data errors.

---

## 🛠️ Technical Stack

* **Framework:** ASP.NET Core 8.0 (MVC Pattern)
* **Database:** Microsoft SQL Server (LocalDB)
* **ORM:** Entity Framework Core (Code-First approach)
* **Frontend:** HTML5, CSS3, Bootstrap 5, JavaScript
* **Tools:** Visual Studio 2022

---

## 💻 How to Run Locally

Since this project uses a "Code-First" approach, the database will create itself automatically.

1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/AsadShibli/Gym-OS-Management-System.git](https://github.com/AsadShibli/Gym-OS-Management-System.git)
    ```

2.  **Open in Visual Studio**
    * Double-click `GrindhouseGym.sln`.

3.  **Restore Dependencies**
    * Build the solution (Ctrl+Shift+B) to restore NuGet packages.

4.  **Run the Application**
    * Press **F5** (Start Debugging).
    * *Note: The application will automatically create the local SQL database on the first run via `EnsureCreated()`.*

5.  **First-Time Admin Setup**
    * The database starts empty.
    * Navigate to `/Identity/Account/Register` in your browser URL bar or click register link in the login page.
    * Create an Admin account (e.g., `admin@gymos.com`).
    * Once logged in, go to **Plans** to create your first subscription tier, then start adding **Members**.

---


---
*© 2025 Gym OS. All Rights Reserved.*
