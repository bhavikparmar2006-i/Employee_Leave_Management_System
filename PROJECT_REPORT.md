# Project Report: Employee Leave Management System

## Abstract
The Employee Leave Management System is a database-driven web application that supports employee leave applications and administrative review. It demonstrates ASP.NET Core MVC, Razor Views, Entity Framework Core, SQL Server, role-based authorization and REST API development.

## Problem Statement
Manual leave tracking can result in lost requests, unclear approval status and duplicated records. A centralized system improves visibility and consistency.

## Objectives
- Digitize leave applications and approvals.
- Store employee and leave data in a relational database.
- Enforce role-based access to administrative and employee operations.
- Demonstrate MVC architecture and REST APIs.

## Scope
The system covers employee records, leave requests, status tracking, administrator decisions and basic reporting. Payroll, attendance, email notifications and advanced HR analytics are outside the initial scope.

## Modules
1. Authentication and authorization.
2. Employee management.
3. Employee dashboard.
4. Leave application and history.
5. Leave review and decisions.
6. REST API.

## Architecture
The browser communicates with ASP.NET Core MVC controllers and Razor Views. Controllers use dependency-injected services from EF Core through `ApplicationDbContext`. SQL Server stores Identity accounts, employee profiles and leave requests. API controllers expose selected operations as JSON.

## Validation and Security
The application uses Identity password hashing, role authorization, anti-forgery validation for MVC forms, model validation, employee-specific data filtering, and protected decision endpoints. Production deployment requires HTTPS, secure secret management, restricted account provisioning and security review.

## Testing
Use `TEST_CASES.md` as the manual test checklist. Build and database tests must be executed in an environment with the .NET SDK and SQL Server installed.

## Limitations and Future Enhancements
The initial version does not implement email alerts, leave balances, holiday calendars, audit logs, or advanced concurrency controls. These can be added after the core workflow is validated.
