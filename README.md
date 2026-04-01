# Task Manager API

This project is a Task Management API built with ASP.NET Core and Entity Framework Core. It implements the assignment requirements for entity relationships, dependency injection, DTO validation, JWT authentication, role-based authorization, LINQ projections, and `AsNoTracking()` for read-only queries.

## Features
- Task manager domain with users, tasks, tags, and user profiles
- One-to-one relationship: `AppUser` ↔ `UserProfile`
- One-to-many relationship: `AppUser` → `TaskItem`
- Many-to-many relationship: `TaskItem` ↔ `Tag` via `TaskTag`
- DTOs for create/update/read operations
- Validation with data annotations
- JWT authentication and role-based authorization
- API documented with Swagger
- LocalDB database named `task`

## Technologies
- ASP.NET Core Web API: builds the HTTP API and middleware pipeline
- Entity Framework Core: manages database access and object-relational mapping
- Microsoft SQL Server LocalDB: local development database engine
- JWT Authentication: issues signed JSON Web Tokens for user login
- Swagger / OpenAPI: documents available API endpoints
- C# / .NET 10: modern runtime and language features

## Running the project
1. Open the solution in Visual Studio or use a terminal.
2. Restore packages:
   ```powershell
   dotnet restore WebApplication1.csproj
   ```
3. Build the project:
   ```powershell
   dotnet build WebApplication1.csproj
   ```
4. Run the API:
   ```powershell
   dotnet run --project WebApplication1.csproj
   ```
5. Open Swagger UI at `https://localhost:5001` or `http://localhost:5000` in development.

## Default seeded accounts
- Admin: `admin@task.local` / `Admin123!`
- User: `user@task.local` / `User123!`

## API Endpoints
- `POST /api/auth/login` - login and receive JWT token
- `GET /api/tasks` - list all tasks
- `GET /api/tasks/{id}` - get task details
- `POST /api/tasks` - create a task
- `PUT /api/tasks/{id}` - update a task
- `PATCH /api/tasks/{id}/status?isDone=true` - update completion status
- `DELETE /api/tasks/{id}` - delete a task (Admin only)
- `GET /api/tags` - list tags
- `POST /api/tags` - create a tag (Admin only)
- `GET /api/users` - list users (Admin only)
- `POST /api/users` - create a new user (Admin only)
- `PUT /api/users/{id}` - update user data (Admin only)
- `DELETE /api/users/{id}` - delete user (Admin only)

## Why HTTP-only cookies are commonly used
HTTP-only cookies are a common industry standard for authentication because they are not accessible from JavaScript. That reduces the risk of token theft from cross-site scripting (XSS) attacks. When authentication tokens are stored in HTTP-only cookies, the browser still sends them automatically with each request, but malicious client-side scripts cannot read or modify the token directly.

## Notes
- The database connection string is kept as LocalDB and points to `Database=task`.
- All read-only queries use `AsNoTracking()` for performance.
- The project includes generated EF Core migrations in the `Migrations` folder.
