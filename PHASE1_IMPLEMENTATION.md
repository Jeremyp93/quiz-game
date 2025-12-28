# Phase 1 (Fast Buzzer) Implementation Guide

This document describes the Phase 1 (Fast Buzzer) implementation on top of the MVP setup.

## ✅ What's Been Implemented

### Backend (ASP.NET Core + SignalR + EF Core)

#### 1. Domain & DTOs
- **CurrentQuestionDto**: Holds bilingual question + answer (id, textFr, textNl, answerFr, answerNl, difficulty)
- **GameStateDto** extended with Phase 1 fields:
  - `CurrentQuestion?` - Currently displayed question (GM sees answer, display hides until revealed)
  - `LastQuestionId?` - ID of last shown question (for avoiding immediate repeats)
  - `BlockedNextQuestionTeamIds` - Team indices blocked for the NEXT question
  - `BlockedTeamIdsForCurrentQuestion` - Team indices blocked for CURRENT question

#### 2. Question Selection Logic (`QuestionService`)
- **GetRandomRegularQuestionAsync(excludeQuestionId)**:
  - Filters by `Type == Regular AND IsActive == true`
  - Difficulty is stored but **NOT used for filtering** (pure random)
  - Avoids immediate repeat:
    - If multiple active Regular questions exist, excludes the last shown question
    - If only one exists, allows repeat
  - Returns null if no active Regular questions

#### 3. Game Session Service (`GameSessionService`)
- **StartPhase1()**: Initialize Fast Buzzer phase
- **ShowQuestion()**:
  - Selects random Regular question (avoids last if possible)
  - Applies `blockedNextQuestionTeamIds` to `blockedTeamIdsForCurrentQuestion`
  - Clears `blockedNextQuestionTeamIds`
  - Switches to Question scene
  - Works even if previous answer wasn't shown
- **ShowAnswer()**: Switches to Answer scene
- **ApplyBlocksForNextQuestion(teamIndices)**: Sets teams to block for next question
- **EndPhase()**: Ends phase, shows scoreboard

#### 4. SignalR Hub (`GameHub`)
- All Phase 1 methods broadcast `GameStateUpdated` to all clients
- New clients receive current state on connection
- Hub methods:
  - `StartPhase1()`
  - `ShowQuestion()`
  - `ShowAnswer()`
  - `ApplyBlocksForNextQuestion(List<int> teamIndices)`
  - `EndPhase()`

### Frontend (React + TypeScript + Framer Motion)

#### 1. New Scenes (`/display`)

**QuestionScene Component**
- Displays bilingual question (FR + NL simultaneously)
- Shows difficulty badge
- Animated card entry with spring physics
- Blocked teams section with badges (if any teams blocked)
- 🚫 emoji indicator on blocked team badges

**AnswerRevealScene Component**
- Shows question section (same as QuestionScene)
- Animated answer reveal section (spring animation)
- Both FR and NL answers displayed simultaneously
- Color-coded sections (green gradient for answer)
- Blocked teams indicator (smaller, persistent)

#### 2. Control Page Updates (`/control`)

**Phase Selector** (visible when teams created and phase = Setup)
- Button: "Start Fast Buzzer Phase"

**Phase 1 Control Panel** (visible when phase = FastBuzzer)
- **Current Question (GM View)**: Shows bilingual question + answer (FR + NL)
- **Phase Controls**:
  - "Show Question" button (green) - Always works, selects NEW question each time
  - "Show Answer" button (cyan) - Disabled until question shown
- **Block Team(s) For Next Question**:
  - Multi-select checkboxes for all teams
  - "Apply Blocks" button (shows count of selected teams)
  - Pending blocks indicator (shows which teams will be blocked next)
- **End Phase** button (red) - Ends phase and shows scoreboard

**Always-On Features**:
- Teams & Scores panel (right panel, always visible)
- +1/-1 score buttons per team
- Global "Show Scoreboard" button
- Score changes broadcast in real-time

#### 3. Display Page Updates (`/display`)
- Renders QuestionScene when `currentScene == Scene.Question`
- Renders AnswerRevealScene when `currentScene == Scene.Answer`
- All scenes use Framer Motion for smooth transitions

## 🎮 How to Use Phase 1

### Setup Flow

1. **Start Game** (`/control`)
2. **Enter Players** (any count, typical 12)
3. **Create Teams** (Random 4-way split)
4. **Rename Teams** if desired
5. **Start Fast Buzzer Phase** (button appears after teams created)

### Gameplay Flow

1. **Show Question**:
   - Click "Show Question" on `/control`
   - GM sees question + answer (bilingual)
   - `/display` shows ONLY question (bilingual)
   - Blocked teams appear on `/display` with 🚫 badges

2. **Show Answer**:
   - Click "Show Answer" on `/control`
   - `/display` reveals answer with animation
   - Answer stays visible until next question

3. **Adjust Scores**:
   - Use +1/-1 buttons in Teams & Scores panel
   - Scores update on `/display` in real-time

4. **Block Teams** (optional, for next question):
   - Check teams to block in "Block Team(s) For Next Question" section
   - Click "Apply Blocks"
   - Blocking applies automatically when next "Show Question" is clicked
   - Blocked teams cannot be changed once applied (until next question)

5. **Show Scoreboard**:
   - Click "Show Scoreboard" (always available)
   - `/display` shows ranked teams with scores
   - Click "Back to Game" to return to last scene

6. **End Phase**:
   - Click "End Phase"
   - Automatically shows scoreboard
   - Returns to Setup phase

## 📋 Phase 1 Rules (Implemented)

### Question Selection
- ✅ Uses ONLY `Type == Regular AND IsActive == true`
- ✅ Difficulty stored but **NOT used for filtering**
- ✅ Avoids immediate repeat when multiple questions exist
- ✅ Allows repeat if only one question exists
- ✅ Each "Show Question" selects NEW question (even without showing answer)

### Blocking Behavior
- ✅ GM selects teams to block via checkboxes
- ✅ "Apply" sets `blockedNextQuestionTeamIds`
- ✅ When "Show Question" clicked:
  - `blockedTeamIdsForCurrentQuestion = blockedNextQuestionTeamIds`
  - `blockedNextQuestionTeamIds = []`
  - `/display` shows blocked indicator next to those teams
- ✅ Blocking valid for current question only
- ✅ Resets automatically on next "Show Question" (unless GM blocks again)

### Scoreboard Behavior
- ✅ NOT auto-shown after questions
- ✅ Can be shown anytime via global button
- ✅ "End Phase" shows scoreboard
- ✅ "Back to Game" returns to last non-scoreboard scene
- ✅ "Show Question" or "Display Teams" leaves scoreboard automatically

### Always-On Features
- ✅ Teams & Scores panel always visible on `/control`
- ✅ Score adjustment (+1/-1) always available
- ✅ Score changes broadcast to `/display` in real-time
- ✅ Global "Show Scoreboard" always available

## 🔧 Technical Details

### Server-Authoritative State
- All game logic in `GameSessionService` (singleton with IServiceProvider injection)
- SignalR broadcasts `GameStateUpdated` snapshots
- Clients render based on server state
- `/control` and `/display` stay synchronized
- Reconnection receives full state snapshot

### Edge Cases Handled
1. **Multiple rapid "Show Question" clicks**: Each click selects a new question
2. **No active Regular questions**: Throws `InvalidOperationException` with clear message
3. **Reconnect mid-question**: Snapshot includes current question + blocked teams
4. **Only one question exists**: Allows repeat selection
5. **Block application**: Clears pending blocks after applying to question

### Bilingual Support
- **Control** (`/control`): Shows FR + NL for question and answer (GM view)
- **Display** (`/display`): Shows FR + NL simultaneously in both Question and Answer scenes

### Animations (Framer Motion)
- **QuestionScene**: Card entry with spring physics, staggered blocked team badges
- **AnswerRevealScene**: Smooth transition from question, answer reveal with height animation
- **Scoreboard**: Score changes animate with tick-up/down effect (from MVP)

## 🚀 Running Phase 1

1. **Backend**:
   ```bash
   cd src/QuizGame.API
   dotnet run
   ```
   API runs on http://localhost:5000

2. **Frontend**:
   ```bash
   cd src/QuizGame.Web
   npm install
   npm run dev
   ```
   Web runs on http://localhost:3000

3. **Create Some Questions** (`/questions`):
   - Create Regular questions with FR + NL text and answers
   - Set difficulty 1-3
   - Mark as Active
   - (MCQ and List questions will be ignored by Phase 1)

4. **Play Phase 1** (`/control` + `/display`):
   - Open `/control` on GM device (e.g., iPad)
   - Open `/display` on projector/TV
   - Follow setup flow above

## 📝 Sample Questions for Testing

Create these Regular questions in `/questions`:

**Question 1** (Difficulty 1):
- FR: "Quelle est la capitale de la France?"
- NL: "Wat is de hoofdstad van Frankrijk?"
- Answer FR: "Paris"
- Answer NL: "Parijs"

**Question 2** (Difficulty 2):
- FR: "En quelle année a eu lieu la Révolution française?"
- NL: "In welk jaar vond de Franse Revolutie plaats?"
- Answer FR: "1789"
- Answer NL: "1789"

**Question 3** (Difficulty 3):
- FR: "Qui a peint la Joconde?"
- NL: "Wie heeft de Mona Lisa geschilderd?"
- Answer FR: "Léonard de Vinci"
- Answer NL: "Leonardo da Vinci"

## 🎯 Next Steps (Future Phases)

The architecture supports additional phases:
- **Phase 2**: List phase (question with multiple accepted answers)
- **Phase 3**: Sabotage phase
- **Phase 4**: Chrono phase

All enums and extensibility points are in place.

## 🐛 Troubleshooting

**No questions appearing:**
- Ensure you have Regular questions marked as Active in `/questions`
- Check backend console for errors

**Blocking not working:**
- Ensure you clicked "Apply Blocks" before "Show Question"
- Pending blocks show which teams will be blocked next

**Display not updating:**
- Check SignalR connection status
- Refresh `/display` page to reconnect
- Check backend is running on port 5000

**Score changes not visible:**
- Use +1/-1 buttons in Teams & Scores panel on `/control`
- Changes broadcast immediately to `/display`
