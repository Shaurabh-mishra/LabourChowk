# LabourChowk

LabourChowk is a **.NET 8 Web API** project designed to connect **Work Posters** (people posting jobs) with **Workers** (laborers looking for work).  
It follows a **Controller-based layered architecture** with support for **DTOs, AutoMapper, SQLite, and Entity Framework Core**.

---

## 🚀 Features
- 👷 **Worker Management** – Register, update, and fetch workers.
- 📝 **Work Poster Management** – Create and manage work posters.
- 💼 **Job Management** – Create and assign jobs between workers and posters.
- 🔄 **DTO + AutoMapper** – Clean mapping between Entities and DTOs.
- 🗄️ **SQLite Database** – Lightweight local database (stored in `/Database` folder).
- 🏗️ **Layered Architecture** – Separation of concerns:
  - **Controllers** → handle HTTP requests  
  - **Services** → business logic  
  - **Repositories** → data access  
  - **DbContext** → database  

---

## 🛠️ Tech Stack
- **.NET 8 Web API (Controller-based)**  
- **Entity Framework Core 8** (Code First + Migrations)  
- **SQLite** (Database stored in `/Database`)  
- **AutoMapper** (DTO ↔ Model mapping)  
- **Dependency Injection** for services & repositories  

---

## 📂 Project Structure
LabourChowk/
┣ Controllers/ # API Controllers (Worker, WorkPoster, Job)
┣ DTOs/ # Data Transfer Objects
┣ Models/ # Entity Models
┣ Services/ # Business Logic Layer
┣ Repositories/ # Data Access Layer
┣ Database/ # SQLite database & migrations
┣ Data/ # EF Core DbContext (LabourChowkContext)
┗ Program.cs # Application startup

---

## ▶️ Getting Started

### 1️⃣ Clone the repository
```bash
git clone https://github.com/YourUsername/LabourChowk.git
cd LabourChowk

2️⃣ Run EF Core Migrations
dotnet ef database update

3️⃣ Run the project
dotnet run

4️⃣ Example API Endpoints

GET /api/worker → Get all workers

POST /api/worker → Add new worker

GET /api/workposter → Get all work posters

POST /api/workposter → Add new work poster

GET /api/job → Get all jobs

POST /api/job → Add new job

📌 Notes

..Database file is stored inside the /Database folder (ignored in Git via .gitignore).

..Built with Controllers for clean separation of HTTP logic.

..Ready for future expansion (Authentication, Role-based Access, React Native frontend).

🤝 Contributing
Contributions are welcome! Feel free to open issues or submit PRs.

---



