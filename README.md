# Financial Goals Manager API

A .NET 10 API for managing financial goals and tracking transactions.

**Status**: 🟢 Core Features Complete | **Updated**: January 27, 2026

---

## 🚀 Quick Start

### Clone Repository

git clone https://github.com/archeraf/FinancialGoalsManager.git cd FinancialGoalsManager

### Run with Docker

docker-compose up -d

**Services**:
- API: http://localhost:5000
- SQLPad (Database Admin): http://localhost:3000
- SQL Server: localhost:1433 (User: `sa`, Password: `MY_SENHA123!`)

---

## 📚 API Endpoints

### Goals

GET    /api/goals → List all goals </br>
GET    /api/goals/{id} → Get goal by id  </br>
POST   /api/goals → Create goal  </br>
PUT    /api/goals/{id} → Update goal  </br>
DELETE /api/goals/{id} → Delete goal </br>

### Transactions

GET    /api/transaction        → List all transactions  </br>
GET    /api/transaction/{id}   → Get transaction by id  </br>
POST   /api/transaction        → Create transaction  </br>
PUT    /api/transaction/{id}   → Update transaction  </br>
DELETE /api/transaction/{id}   → Delete transaction </br>

---

## ✅ Features

- ✅ Complete CRUD for Goals and Transactions
- ✅ FluentValidation with business rules
- ✅ Global error handling middleware
- ✅ Domain-driven design entities
- ✅ Swagger/OpenAPI documentation
- ✅ Auto-migrations on startup
- ✅ Soft delete support

---

## 📋 Business Rules

**Goals (Caixa)**
- Title: 3-200 characters
- AmountGoal: > 0, max 2 decimal places
- Deadline: Optional, must be future
- Status: InProgress, Complete (auto), Canceled, Paused

**Transactions (Transações)**
- Amount: > 0, max 2 decimal places (never negative)
- Type: Deposit or Withdraw
- TransactionDate: Optional, cannot be future

---

## 🏗️ Architecture

- **API Layer**: Controllers, Filters, Middleware
- **Application Layer**: Services, Validators, DTOs
- **Domain Layer**: Rich entities, business logic
- **Infrastructure Layer**: EF Core, Repository, Migrations

---

## 🛠️ Tech Stack

- ASP.NET Core 10
- C# 14.0
- Entity Framework Core
- SQL Server 2022
- FluentValidation
- Swagger/OpenAPI

---

## 📊 Progress
Core Features:  ██████████ 100% /
Validation:     ██████████ 100% /
Error Handling: ██████████ 100% /
PLUS Features:             0% /
Overall: 65%
