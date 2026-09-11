# 🚗 DVLD Web API

Backend for the web version of my **Driving & Vehicle License Department (DVLD)** system.

## 📖 About The Project

This project is a continuation of my original **DVLD Desktop Application**, which I built using **C#, Windows Forms, ADO.NET, and SQL Server**.

After completing the desktop version, I decided to rebuild the project as a **Full-Stack Web Application**, starting with the backend.

The goal is to keep the same core DVLD system while rebuilding its backend using **ASP.NET Core Web API, Entity Framework Core, and Clean Architecture**.

## 🔄 From Desktop to Web

| Desktop Version 🖥️    | Web Version 🌐        |
| ---------------------- | --------------------- |
| Windows Forms          | ASP.NET Core Web API  |
| ADO.NET                | Entity Framework Core |
| 3-Tier Architecture    | Clean Architecture    |
| Desktop UI             | REST API              |
| Synchronous operations | Async/Await           |

## 🏗️ Clean Architecture

The backend is divided into four projects:

```text
DVLD
│
├── DVLD.Domain
├── DVLD.Application
├── DVLD.Infrastructure
└── DVLD.API
```

| Layer              | Responsibility                                      |
| ------------------ | --------------------------------------------------- |
| 🧠 Domain          | Entities and business rules                         |
| ⚙️ Application     | DTOs, interfaces, and services                      |
| 🗄️ Infrastructure | EF Core, repositories, database access, and mapping |
| 🌐 API             | Controllers and HTTP endpoints                      |

The main idea is to keep the **Domain and Application layers independent from infrastructure and framework-specific details**.

## ✨ Current Features

Currently implemented backend modules include:

* 👤 People & Users
* 🌍 Countries
* 🏷️ Application Types
* 📄 Applications
* 🚨 Detained Licenses

The remaining modules are being migrated from the original desktop application gradually.

## 🛠️ Technologies

| Category        | Technology            |
| --------------- | --------------------- |
| Language        | C#                    |
| Framework       | ASP.NET Core          |
| ORM             | Entity Framework Core |
| Database        | SQL Server            |
| API             | RESTful Web API       |
| Architecture    | Clean Architecture    |
| Documentation   | Swagger / OpenAPI     |
| Version Control | Git / GitHub          |

## 🚀 Roadmap

* [x] Clean Architecture setup
* [x] Domain entities
* [x] DTOs and application services
* [x] Repository pattern
* [x] Entity mapping
* [x] REST API controllers
* [x] Async database operations
* [ ] Migrate remaining DVLD modules
* [ ] JWT Authentication
* [ ] Authorization
* [ ] Build the frontend
* [ ] Connect frontend with the API

## 📌 Project Status

🚧 **Backend in development**

This is the backend stage of rebuilding my original DVLD Desktop Application into a Full-Stack Web Application.

---

Made with C#, ASP.NET Core, EF Core, and SQL Server.
