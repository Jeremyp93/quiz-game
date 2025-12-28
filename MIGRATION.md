# Database Migration Instructions

This document describes how to create and apply EF Core migrations for the Quiz Game application.

## Prerequisites

- .NET 8 SDK installed
- EF Core CLI tools installed

## Installing EF Core Tools

If you don't have the EF Core tools installed, run:

```bash
dotnet tool install --global dotnet-ef
```

Or update to the latest version:

```bash
dotnet tool update --global dotnet-ef
```

## Creating Initial Migration

Navigate to the Infrastructure project directory and create the initial migration:

```bash
cd src/QuizGame.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../QuizGame.API
```

This will create migration files in the `Migrations` folder.

## Applying Migrations

The database will be automatically created when the API starts (see `Program.cs`), but you can also manually apply migrations:

```bash
cd src/QuizGame.Infrastructure
dotnet ef database update --startup-project ../QuizGame.API
```

## Creating Additional Migrations

If you modify the entity models, create a new migration:

```bash
cd src/QuizGame.Infrastructure
dotnet ef migrations add YourMigrationName --startup-project ../QuizGame.API
dotnet ef database update --startup-project ../QuizGame.API
```

## Database Location

The SQLite database file (`quizgame.db`) will be created in the `src/QuizGame.API` directory.

## Removing Migrations

To remove the last migration (if not applied to the database):

```bash
cd src/QuizGame.Infrastructure
dotnet ef migrations remove --startup-project ../QuizGame.API
```

## Database Schema

### Tables

1. **Questions**
   - Id (Guid, PK)
   - Type (string: Regular, List, Mcq)
   - Difficulty (int: 1-3)
   - IsActive (bool)
   - Category (string, nullable)
   - Tags (string, nullable)
   - TextFr (string, required)
   - TextNl (string, required)

2. **RegularQuestionDetails** (1:1 with Questions)
   - QuestionId (Guid, PK/FK)
   - AnswerFr (string, required)
   - AnswerNl (string, required)

3. **McqQuestionDetails** (1:1 with Questions)
   - QuestionId (Guid, PK/FK)
   - ChoiceAFr, ChoiceANl (string, required)
   - ChoiceBFr, ChoiceBNl (string, required)
   - ChoiceCFr, ChoiceCNl (string, required)
   - CorrectChoice (string: A, B, C)

4. **ListQuestionAnswers** (many:1 with Questions)
   - Id (Guid, PK)
   - QuestionId (Guid, FK)
   - AnswerFr (string, required)
   - AnswerNl (string, required)
   - AltSpellings (string, nullable)

## Notes

- The application uses EnsureCreated() in development, which automatically creates the database schema
- For production, use proper migrations instead of EnsureCreated()
- All foreign key relationships use CASCADE delete
