# 📘 Devices API

## 🧩 Overview

The Devices API is a RESTful service built with ASP.NET Core (.NET 9) that manages a collection of devices and their states. It enforces domain rules such as preventing deletion or modification of in-use devices, and includes full Swagger documentation for easy testing and exploration.

---

## 🚀 Features

- CRUD operations for devices
- Domain validation (e.g., prevent deletion of in-use devices)
- SQLite persistence
- Swagger UI for documentation and testing
- Dockerized for reproducible deployment

---

## 🛠️ Technologies

- ASP.NET Core (.NET 9)
- Entity Framework Core
- SQLite
- Docker

---

## 📦 Running Locally

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Run with .NET CLI

```bash
dotnet run --project DevicesApi.Api
```

Then open:

```
http://localhost:5239/swagger/index.html
```
---
## 🐳 Running with Docker

### Build the image

```bash
docker build -t devices-api .
```

### Run the container

```bash
docker run -p <host-port>:80 --name devices-api-dev -e ASPNETCORE_ENVIRONMENT=Development devices-api
```

Replace `<host-port>` with any available port on your machine (e.g., `5000`, `8080`, etc.).

Then open:

```
http://localhost:<host-port>/swagger/index.html
```

> Note: `http://localhost:<host-port>` returns 404 — Swagger is available at `/swagger/index.html`.

---

## 🗃️ Database

The API uses a pre-seeded SQLite database (`devices.db`) included in the image.

### Mount your own DB (optional)

```bash
docker run -p 5000:80 --name devices-api-dev -e ASPNETCORE_ENVIRONMENT=Development -v ${PWD}/DevicesApi.Api/devices.db:/app/devices.db devices-api
```

> On Windows PowerShell, use:

```powershell
docker run -p 5000:80 --name devices-api-dev -e ASPNETCORE_ENVIRONMENT=Development -v ${PWD}\DevicesApi.Api\devices.db:/app/devices.db devices-api
```

---

## ✅ Domain Rules

- Devices marked as `InUse` cannot be deleted
- `Name` and `Brand` cannot be updated for `InUse` devices
- All endpoints return appropriate status codes and validation messages

---

## 📈 Future Improvements

- Add authentication and role-based access
- Expand DTO validation
- Add Service layer after implementing role based access
- Migrate to PostgreSQL or SQL Server for production
- Add brand entity or brand management
