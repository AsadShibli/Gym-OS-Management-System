# 🏋️ Gym OS | Enterprise Gym Management Ecosystem

**Gym OS** is a high-performance, full-stack management platform engineered for modern fitness centers. It replaces fragmented manual tracking with a centralized, data-driven architecture built on **ASP.NET Core 8.0 MVC** and **Entity Framework Core**.

---

## 📊 System Screenshots

> **Strategic Note:** The UI is designed for operational efficiency, minimizing clicks for high-frequency administrative tasks.

| **Executive Dashboard** | **Member Management** |
|:---:|:---:|
| <img width="1373" height="304" alt="image" src="https://github.com/user-attachments/assets/f5471324-41ce-4f8f-890a-7c1662753756" />| <img width="1756" height="338" alt="image" src="https://github.com/user-attachments/assets/62e04830-901f-492f-b502-4fcbdb347a8d" /> | 

| **Member Profile** | **Membership Status** |
|:---:|:---:|
|  <img width="1711" height="837" alt="image" src="https://github.com/user-attachments/assets/358e0473-7247-4f60-aaae-f3e10de4bcf9" /> |<img width="1140" height="678" alt="image" src="https://github.com/user-attachments/assets/4c2313e8-4d77-4e32-a577-cffefd9c2180" /> |



---

| **Trainer Profile & Roster** |
|:---:|
| <img width="1724" height="756" alt="image" src="https://github.com/user-attachments/assets/63fa950a-4044-43dd-b7c4-57ed1c97f559" />
 |


---

## 🚀 Strategic Key Features

### 1. Executive Intelligence Dashboard
* **Financial Analytics:** Real-time summation of projected revenue and active member counts.
* **Operational Overview:** A centralized command center for monitoring the gym's total ecosystem at a glance.

### 2. Member & Subscription Lifecycle
* **Relational Onboarding:** Members are linked to specific pricing tiers (Silver, Gold, Platinum) via robust database-driven relationships.
* **Instant Search:** Implementation of client-side JavaScript filtering for rapid member retrieval, bypassing unnecessary server round-trips.

### 3. Staff & Trainer Roster System
* **Relational Mapping:** Employs a **One-to-Many** data structure where trainers are assigned specific members.
* **Print Utility:** Integrated professional roster generator with optimized CSS for physical record-keeping.

---

## 🧠 Engineering Logic (Core Intelligence)

This system moves beyond basic CRUD by implementing four critical automated logic layers:

### 3.1 Real-Time Status Engine
The system calculates membership validity dynamically. It compares the `(Join Date + Subscription Duration)` against the current system clock to render **Active (Green)** or **Expired (Red)** badges in real-time, eliminating manual status updates.

### 3.2 Weighted Revenue & Discount Engine
Projected revenue is processed using a business-weighted formula:  
**`Revenue = (Plan Fee * Duration) - Applied Discount`** The system automatically applies a **10% discount** for 6-month commitments and **20%** for 12-month annual plans to reflect real-world retention strategies.

### 3.3 Bidirectional Relational Roster
Utilizing EF Core's `.Include()` and `.ThenInclude()` methods, the system generates deep-relational views. This allows administrators to audit trainer workloads and client assignments through a single, high-performance query.

### 3.4 Adaptive Media Handler
The interface dynamically adapts to data availability. If a profile photo exists, the system streams it via the file system; otherwise, it renders branded SVG placeholders to maintain visual consistency and UI integrity.

---

## 🛠️ Technical Stack

* **Framework:** ASP.NET Core 8.0 (MVC Pattern)
* **Database:** SQL Server (LocalDB)
* **ORM:** Entity Framework Core (Code-First approach)
* **Frontend:** Bootstrap 5, Razor Pages, Vanilla JavaScript
* **Security:** ASP.NET Identity (Role-Based Access Control)

---

## 💻 Local Implementation Guide

1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/AsadShibli/Gym-OS-Management-System.git](https://github.com/AsadShibli/Gym-OS-Management-System.git)
    ```

2.  **Database Provisioning**
    Open the solution in **Visual Studio 2022**. The database will be automatically provisioned via the `EnsureCreated()` method on the first application launch.

3.  **Authentication Setup**
    Press **F5** to run. Navigate to `/Identity/Account/Register` to initialize the primary Admin account.

---

**Developed by [MD. Asadullah Shibli](https://github.com/AsadShibli)** | Dhaka, Bangladesh*
