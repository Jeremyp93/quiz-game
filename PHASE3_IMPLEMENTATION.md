# Phase 3 (Sabotage) Implementation Plan

## Overview
Phase 3 consists of:
1. **Theme Management System** (Database + Admin UI)
2. **Subphase 1**: Theme Assignment (8 themes → 4 teams, 2 each)
3. **Subphase 2**: MCQ Questions (each team plays their 2 themes)

## A. Theme Management (COMPLETED)

### Database
- ✅ Theme entity created
- ✅ Question.ThemeId FK added
- ✅ DbContext updated with Theme DbSet
- ✅ OnDelete: SetNull (safe deletion)
- ✅ Unique constraint on Theme.Code
- Migration needed: AddThemeTable

### DTOs & Services
- ✅ ThemeDto, CreateThemeDto
- ✅ IThemeService interface
- ✅ ThemeService implementation
  - CRUD operations
  - GetRandomActiveThemesAsync(count)
  - Validation (duplicate code check, linked questions check)

### Remaining Theme Work
- [ ] Update QuestionService.MapToDto to include Theme
- [ ] Create ThemesController API endpoints
- [ ] Create ThemesPage admin UI (/themes route)
- [ ] Update QuestionsPage to include theme dropdown
- [ ] Add theme validation for MCQ questions

## B. Phase 3 State Machine

```
Setup → [Start Sabotage] → ThemeAssignment → [Start Questions] → Mcq → [End Phase] → Setup
                              ↓ (8 themes selected, assignments tracked)      ↓ (question sets generated)
```

### Scenes
- **SabotageThemeAssignmentScene**: 4 teams with 2 slots each, 8 theme tiles
- **SabotageMcqScene**: Current team, theme, question, choices A/B/C

## C. Sabotage Game State Structure

```typescript
sabotageState: {
  subphase: "ThemeAssignment" | "Mcq",

  // Theme Assignment
  selectedThemeIds: Guid[],  // 8 themes
  teamAssignments: {
    [teamId]: Guid[]  // 2 theme IDs per team
  },
  recommendedTurnOrder: number[],  // team indices by score
  currentTurnIndex: number,
  assignmentHistory: { themeId, teamId, slotIndex }[],

  // MCQ Subphase
  mcqState: {
    currentTeamIndex: number,  // 0-3
    currentThemeIndex: number,  // 0-1 (each team has 2 themes)
    currentQuestionIndex: number,  // 0-3 (4 questions per theme)
    selectedAnswer: "A"|"B"|"C"|null,
    isRevealed: boolean,
    currentQuestion: McqQuestionDto,
    questionSets: {
      [teamIndex]: {
        [themeIndex]: Guid[]  // 4 question IDs (2xDiff1, 1xDiff2, 1xDiff3)
      }
    }
  }
}
```

## D. Phase 3 Logic Requirements

### Theme Assignment Rules
1. Exactly 8 active themes auto-selected when phase starts
2. Turn order: highest score first (stable tie-breaking)
3. Each team gets exactly 2 themes
4. Special case: when 2 themes left, active team picks 1, last goes to remaining team
5. Constraints (with warnings):
   - Team can't exceed 2 themes
   - Can't assign to self when selecting "other team"
   - Theme can't be assigned twice
6. Undo: revert last assignment

### MCQ Question Requirements
1. Per theme: exactly 4 questions
   - 2 easy (difficulty 1)
   - 1 medium (difficulty 2)
   - 1 hard (difficulty 3)
2. Availability check BEFORE MCQ starts:
   - For each assigned theme, verify count(diff=X) >= required
   - If insufficient: show error, allow theme replacement
3. Question selection:
   - Type == Mcq AND IsActive AND ThemeId == themeId
   - Avoid repeats within sabotage phase
4. Team order: best team first (same as assignment)
5. Each team plays Theme 1, then Theme 2
6. Progress: Team X/4, Theme Y/2, Question Z/4

### MCQ Interaction
- GM selects answer A/B/C → highlights on display
- Reveal Answer → correct=GREEN, wrong selected=RED
- Manual scoring via global +1/-1
- Next Question/Theme/Team with warnings if unfinished

## E. SignalR Commands

```csharp
// Phase 3 Start
StartPhase3() // auto-picks 8 themes, enters ThemeAssignment

// Theme Assignment
AssignTheme(themeId, targetTeamId)
UndoLastThemeAssignment()
ReplaceTheme(oldThemeId, newThemeId)
ValidateThemesAndStartQuestions() // checks availability, generates question sets

// MCQ
SelectMcqAnswer("A"|"B"|"C")
RevealMcqAnswer()
NextMcqQuestion()
NextMcqTheme(bool force) // with warning if unfinished
NextMcqTeam(bool force) // with warning if unfinished
EndPhase()
```

## F. Frontend Components

### Admin UI
- ThemesPage.tsx: Theme CRUD with usage count
- Update QuestionsPage.tsx: Theme dropdown (required for MCQ)

### Display Scenes
- SabotageThemeAssignmentScene.tsx
  - 4 team slots (2 theme boxes each)
  - 8 theme tiles (bilingual names)
  - Framer Motion: tile slide animation
- SabotageMcqScene.tsx
  - Team + Theme names (bilingual)
  - Question text (bilingual)
  - Choices A/B/C (bilingual)
  - Answer reveal animations

### Control Panel
- Phase 3 panel (visible only when Phase == Sabotage)
- Theme Assignment UI:
  - Theme selector
  - Team selector (with "assign to active" vs "other team")
  - Undo button
  - Start Questions button (enabled when all assigned)
  - Warning if < 8 active themes
- MCQ UI:
  - Current progress display
  - Answer selector (A/B/C)
  - Reveal button
  - Next Question/Theme/Team (with warnings)
  - End Phase button

## G. Implementation Phases

### Phase 1: Theme Infrastructure ✅
- Database, DTOs, Service

### Phase 2: Theme Admin UI (IN PROGRESS)
- ThemesController
- ThemesPage component
- Update QuestionsPage

### Phase 3: Backend Game Logic
- Update GameStateDto with sabotage state
- Implement GameSessionService methods:
  - StartPhase3
  - Theme assignment logic
  - Question availability checks
  - Question set generation
  - MCQ progression logic
- Update GameHub

### Phase 4: Frontend Game UI
- Types and services
- Display scenes
- Control panel
- Animations

### Phase 5: Testing & Polish
- Test all workflows
- Edge cases
- Commit

## H. Edge Cases to Handle

1. **Insufficient themes** (< 8 active): Block phase start, show error
2. **Insufficient questions**: Detect before MCQ, allow theme replacement
3. **GM overrides**: Allow but warn (skip questions, change order)
4. **Scoreboard navigation**: Preserve sabotage state when showing scoreboard
5. **Reconnect**: Full state restoration mid-assignment or mid-question
6. **Undo limits**: Can't undo if already in MCQ subphase

## I. Next Steps

1. ✅ Update QuestionService to map Theme
2. Create ThemesController
3. Add migration for Theme table
4. Create ThemesPage UI
5. Update QuestionsPage with theme dropdown
6. Add Sabotage DTOs to GameStateDto
7. Implement all GameSessionService sabotage methods
8. Add SignalR hub methods
9. Create frontend types
10. Build display scenes
11. Build control panel
12. Test end-to-end
13. Commit

---

**Status**: Infrastructure complete, moving to API & UI implementation
