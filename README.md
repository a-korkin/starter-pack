## Starter project: React + ASP NET Core + PostgreSQL

#### dotnet ef migrations add InitialMigration --project src/Infrastructure/ --startup-project src/WebApi/ --output-dir Persistence/Migrations

#### dotnet ef migrations script --project src/Infrastructure/ --startup-project src/WebApi/ --output src/Infrastructure/Persistence/Migrations/InitialMigration.sql