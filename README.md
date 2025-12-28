# Quiz Game MVP - TV-Style Quiz Application

A full-stack bilingual (FR/NL) TV-style quiz application built with Clean Architecture principles.

## Tech Stack

### Backend
- **ASP.NET Core 8** - Web API
- **SignalR** - Real-time communication
- **Entity Framework Core** - ORM
- **SQLite** - Database

### Frontend
- **React 18** - UI Framework
- **TypeScript** - Type Safety
- **Vite** - Build Tool
- **Framer Motion** - Animations
- **SignalR Client** - Real-time updates

## Architecture

The backend follows Clean Architecture with clear separation of concerns:

```
src/
├── QuizGame.Domain/          # Entities, Enums (no dependencies)
├── QuizGame.Application/     # DTOs, Interfaces (depends on Domain)
├── QuizGame.Infrastructure/  # EF Core, Services (implements Application)
├── QuizGame.API/            # Controllers, SignalR Hub, Startup
└── QuizGame.Web/            # React Frontend
```

## Features Implemented (MVP)

### 1. Question Management (`/questions`)
- Full CRUD operations for questions
- Support for 3 question types:
  - **Regular**: Simple Q&A with bilingual answer
  - **List**: Question with multiple accepted answers
  - **MCQ**: Multiple choice with 3 options (A/B/C)
- Bilingual support (French & Dutch)
- Difficulty levels (1-3)
- Active/Inactive toggle
- Filtering by type, difficulty, status
- Search functionality

### 2. Game Control (`/control`)
- Start new game
- Enter player names (any count)
- Create 4 teams with random distribution
- Rename teams
- Move players between teams
- Adjust team scores (+1/-1)
- Control display scenes:
  - Show Teams
  - Show Scoreboard
  - Back to Game
- Real-time synchronization with display

### 3. Display (`/display`)
- Read-only projector view
- Animated scene transitions (Framer Motion)
- **Teams Scene**: Shows all teams with player lists
- **Scoreboard Scene**: Shows ranked teams with scores
  - Animated score changes
  - Special styling for 1st place
- Auto-reconnection on refresh

### 4. Game State Management
- Server-authoritative state
- In-memory singleton service
- SignalR real-time broadcasting
- Supports future phases (enum structure ready):
  - Setup, FastBuzzer, List, Sabotage, Chrono

## Project Structure

### Backend

```
QuizGame.Domain/
├── Entities/
│   ├── Question.cs
│   ├── RegularQuestionDetails.cs
│   ├── McqQuestionDetails.cs
│   └── ListQuestionAnswer.cs
└── Enums/
    ├── QuestionType.cs
    ├── McqChoice.cs
    ├── Phase.cs
    └── Scene.cs

QuizGame.Application/
├── DTOs/
│   ├── QuestionDto.cs
│   ├── CreateQuestionDto.cs
│   └── GameStateDto.cs
└── Interfaces/
    ├── IQuestionService.cs
    └── IGameSessionService.cs

QuizGame.Infrastructure/
├── Data/
│   └── QuizGameDbContext.cs
└── Services/
    ├── QuestionService.cs
    └── GameSessionService.cs

QuizGame.API/
├── Controllers/
│   └── QuestionsController.cs
├── Hubs/
│   └── GameHub.cs
├── Program.cs
└── appsettings.json
```

### Frontend

```
QuizGame.Web/
├── src/
│   ├── components/
│   │   ├── QuestionForm.tsx
│   │   ├── TeamsScene.tsx
│   │   └── ScoreboardScene.tsx
│   ├── pages/
│   │   ├── QuestionsPage.tsx
│   │   ├── ControlPage.tsx
│   │   └── DisplayPage.tsx
│   ├── services/
│   │   ├── questionService.ts
│   │   └── gameService.ts
│   ├── hooks/
│   │   └── useGameState.ts
│   ├── types.ts
│   ├── App.tsx
│   └── main.tsx
├── package.json
└── vite.config.ts
```

## Setup Instructions

### Prerequisites

1. Install .NET 8 SDK: https://dotnet.microsoft.com/download
2. Install Node.js 18+: https://nodejs.org/

### Backend Setup

1. Navigate to the API project:
```bash
cd src/QuizGame.API
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the API (it will auto-create the database):
```bash
dotnet run
```

The API will start on `http://localhost:5000`

### Frontend Setup

1. Navigate to the Web project:
```bash
cd src/QuizGame.Web
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```

The frontend will start on `http://localhost:3000`

## Running the Application

1. **Start Backend** (Terminal 1):
```bash
cd src/QuizGame.API
dotnet run
```

2. **Start Frontend** (Terminal 2):
```bash
cd src/QuizGame.Web
npm run dev
```

3. **Access the Application**:
   - Home: http://localhost:3000
   - Questions Admin: http://localhost:3000/questions
   - Game Control: http://localhost:3000/control
   - Display (Projector): http://localhost:3000/display

## Usage Flow

1. **Setup Questions** (`/questions`):
   - Create questions of different types
   - Set difficulty levels
   - Add bilingual content

2. **Start Game** (`/control`):
   - Click "Start Game"
   - Enter player names (e.g., 12 players)
   - Click "Create Teams (Random)" to distribute players
   - Rename teams if desired
   - Move players between teams if needed

3. **Control Display** (`/control`):
   - Click "Display Teams" to show teams on projector
   - Adjust scores during gameplay using +1/-1 buttons
   - Click "Show Scoreboard" to display rankings
   - Click "Back to Game" to return to previous scene

4. **Projector View** (`/display`):
   - Open on second screen/projector
   - Displays current scene with animations
   - Updates automatically when control changes state

## SignalR Hub Methods

### Client → Server
- `StartGame()` - Initialize new game session
- `SetPlayers(playerNames: string[])` - Set player list
- `CreateTeams()` - Create 4 teams randomly
- `RenameTeam(teamIndex: int, newName: string)` - Rename team
- `MovePlayer(playerName: string, toTeamIndex: int)` - Move player
- `AdjustScore(teamIndex: int, delta: int)` - Adjust team score
- `ShowTeamsScene()` - Display teams scene
- `ShowScoreboard()` - Display scoreboard
- `BackToGame()` - Return to previous scene

### Server → Clients
- `GameStateUpdated(state: GameStateDto)` - Broadcast current state

## Database

- **Type**: SQLite
- **Location**: `src/QuizGame.API/quizgame.db`
- **Migrations**: See `MIGRATION.md` for details

## API Endpoints

### Questions API

- `GET /api/questions` - Get all questions (with optional filters)
- `GET /api/questions/{id}` - Get question by ID
- `POST /api/questions` - Create new question
- `PUT /api/questions/{id}` - Update question
- `DELETE /api/questions/{id}` - Delete question
- `POST /api/questions/{id}/toggle-active` - Toggle active status

Query parameters for filtering:
- `type` (0=Regular, 1=List, 2=Mcq)
- `difficulty` (1-3)
- `isActive` (true/false)
- `searchText` (searches FR and NL text)

## Future Extensions (Not in MVP)

The architecture is designed to support:

1. **Phase 1 Gameplay**:
   - Fast Buzzer phase with question flow
   - Answer reveal animations
   - Blocking mechanics

2. **Additional Phases**:
   - List phase
   - Sabotage phase
   - Chrono phase

3. **Enhanced Features**:
   - Player statistics
   - Question import/export
   - Sound effects
   - Custom themes

## Development Notes

### SOLID Principles Applied

- **Single Responsibility**: Each class has one reason to change
- **Open/Closed**: Extensible for new question types/phases
- **Liskov Substitution**: Interface-based design
- **Interface Segregation**: Focused interfaces
- **Dependency Inversion**: All dependencies use abstractions

### Clean Architecture Benefits

- **Testable**: Business logic isolated from infrastructure
- **Independent of UI**: Backend can work with any client
- **Independent of Database**: Can swap SQLite for SQL Server
- **Maintainable**: Clear separation of concerns

## Troubleshooting

### Backend won't start
- Ensure .NET 8 SDK is installed: `dotnet --version`
- Check port 5000 is available

### Frontend won't start
- Ensure Node.js 18+ is installed: `node --version`
- Run `npm install` in the Web directory

### SignalR not connecting
- Ensure backend is running on port 5000
- Check CORS settings in `Program.cs`
- Verify the SignalR URL in `gameService.ts`

### Database issues
- Delete `quizgame.db` and restart the API
- Check connection string in `appsettings.json`

## License

This is an MVP implementation for a TV-style quiz game application.
