# Bulk Import Questions

This directory contains example JSON files for bulk importing questions into the quiz game.

## API Endpoint

**POST** `/api/questions/bulk-import`

**Content-Type**: `application/json`

## Question Types

The `type` field uses numeric enum values:

- `0` = Regular (Phase 1 - Fast Buzzer)
- `1` = List (Phase 2 - List)
- `2` = MCQ (Phase 3 - Sabotage)
- `3` = Regular4 (Phase 4 - Chrono)

## MCQ Choice Enum

The `correctChoice` field for MCQ questions uses:

- `0` = Choice A
- `1` = Choice B
- `2` = Choice C

## Field Descriptions

### Common Fields (All Question Types)

- **type** (number, required): Question type (0-3)
- **difficulty** (number, required): Difficulty level (1=Easy, 2=Medium, 3=Hard)
- **isActive** (boolean, required): Whether the question is active
- **isPriority** (boolean, required): Whether this is a priority question (selected first in each phase)
- **category** (string, optional): Question category
- **tags** (string, optional): Semicolon-separated tags
- **textFr** (string, required): Question text in French
- **textNl** (string, required): Question text in Dutch
- **themeId** (string, optional): UUID of theme (required for MCQ questions)

### Type-Specific Fields

#### Regular Questions (type: 0) and Regular4 Questions (type: 3)

```json
"regularDetails": {
  "answerFr": "French answer",
  "answerNl": "Dutch answer"
}
```

#### List Questions (type: 1)

```json
"listAnswers": [
  {
    "answerFr": "French answer",
    "answerNl": "Dutch answer",
    "altSpellings": "alternative1;alternative2"  // optional
  }
]
```

#### MCQ Questions (type: 2)

```json
"mcqDetails": {
  "choiceAFr": "Choice A in French",
  "choiceANl": "Choice A in Dutch",
  "choiceBFr": "Choice B in French",
  "choiceBNl": "Choice B in Dutch",
  "choiceCFr": "Choice C in French",
  "choiceCNl": "Choice C in Dutch",
  "correctChoice": 0  // 0=A, 1=B, 2=C
}
```

**Note**: MCQ questions require a `themeId`. You must first get theme IDs from the database or via API:

```bash
GET /api/themes
```

Available theme codes: `animals`, `sports`, `technology`, `movies`, `art`, `music`, `history`, `science`

## Example Files

- **regular-questions-example.json** - Phase 1 Fast Buzzer questions
- **regular4-questions-example.json** - Phase 4 Chrono questions
- **list-questions-example.json** - Phase 2 List questions
- **mcq-questions-example.json** - Phase 3 MCQ questions (requires theme ID replacement)
- **all-types-example.json** - Combined example with all 4 types

## Usage Examples

### Using cURL

```bash
# Import regular questions
curl -X POST http://localhost:5000/api/questions/bulk-import \
  -H "Content-Type: application/json" \
  -d @regular-questions-example.json

# Import all types at once
curl -X POST http://localhost:5000/api/questions/bulk-import \
  -H "Content-Type: application/json" \
  -d @all-types-example.json
```

### Using PowerShell

```powershell
# Import regular questions
$json = Get-Content -Path "regular-questions-example.json" -Raw
Invoke-RestMethod -Uri "http://localhost:5000/api/questions/bulk-import" `
  -Method POST `
  -ContentType "application/json" `
  -Body $json

# Import all types at once
$json = Get-Content -Path "all-types-example.json" -Raw
Invoke-RestMethod -Uri "http://localhost:5000/api/questions/bulk-import" `
  -Method POST `
  -ContentType "application/json" `
  -Body $json
```

### Using JavaScript/Fetch

```javascript
const response = await fetch('http://localhost:5000/api/questions/bulk-import', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify(questions)
});

const result = await response.json();
console.log(`Imported: ${result.successCount}/${result.totalQuestions}`);
console.log(`Errors: ${result.failureCount}`);
```

## Response Format

### Success (200 OK)

```json
{
  "totalQuestions": 10,
  "successCount": 10,
  "failureCount": 0,
  "errors": [],
  "importedQuestions": [
    // Array of imported QuestionDto objects
  ]
}
```

### Partial Success (207 Multi-Status)

```json
{
  "totalQuestions": 10,
  "successCount": 8,
  "failureCount": 2,
  "errors": [
    {
      "questionIndex": 3,
      "errorMessage": "Theme is required for MCQ questions",
      "questionTextFr": "Question text that failed"
    }
  ],
  "importedQuestions": [
    // Array of successfully imported QuestionDto objects
  ]
}
```

### Complete Failure (400 Bad Request)

```json
{
  "totalQuestions": 3,
  "successCount": 0,
  "failureCount": 3,
  "errors": [
    {
      "questionIndex": 0,
      "errorMessage": "French text is required",
      "questionTextFr": null
    }
  ],
  "importedQuestions": []
}
```

## Important Notes for MCQ Questions

MCQ questions (type 2) require a valid theme ID. Follow these steps:

1. Get available themes:
   ```bash
   curl http://localhost:5000/api/themes
   ```

2. Find the theme ID you need (e.g., for "sports", "animals", etc.)

3. Replace `"REPLACE_WITH_ANIMALS_THEME_ID"` in the JSON with the actual UUID

4. Or use `null` for `themeId` if you want the question to be available but not for Phase 3

## Priority Questions

Questions marked with `"isPriority": true` will be randomly selected first within each phase before regular questions:

- **Phase 1 (Regular)**: Priority Regular questions appear first
- **Phase 2 (List)**: Priority List questions appear first
- **Phase 4 (Regular4)**: Priority Regular4 questions appear first
- **Phase 3 (MCQ)**: Priority flag is ignored (theme-based selection)

This is useful for ensuring certain questions (e.g., Belgian content, important topics) are always asked.

## Validation Rules

- French text (`textFr`) is required
- Dutch text (`textNl`) is required
- Difficulty must be 1, 2, or 3
- Question type must match the provided details:
  - Type 0 or 3 requires `regularDetails`
  - Type 1 requires `listAnswers` (at least one answer)
  - Type 2 requires `mcqDetails` and a valid `themeId`
- MCQ `correctChoice` must be 0, 1, or 2
