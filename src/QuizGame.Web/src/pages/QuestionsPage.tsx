import { useState, useEffect, useRef } from 'react';
import { Question, QuestionType, CreateQuestionDto, BulkImportResult } from '../types';
import { questionService } from '../services/questionService';
import QuestionForm from '../components/QuestionForm';
import styles from './QuestionsPage.module.css';

const SAMPLE_JSON = `[
  {
    "type": 0,
    "difficulty": 1,
    "isActive": true,
    "isPriority": false,
    "category": "Geography",
    "textFr": "Quelle est la capitale de la Belgique?",
    "textNl": "Wat is de hoofdstad van België?",
    "regularDetails": {
      "answerFr": "Bruxelles",
      "answerNl": "Brussel"
    }
  },
  {
    "type": 1,
    "difficulty": 2,
    "isActive": true,
    "isPriority": true,
    "category": "Belgian Culture",
    "textFr": "Nommez les 3 régions de la Belgique",
    "textNl": "Noem de 3 gewesten van België",
    "listAnswers": [
      {
        "answerFr": "Flandre",
        "answerNl": "Vlaanderen"
      },
      {
        "answerFr": "Wallonie",
        "answerNl": "Wallonië"
      },
      {
        "answerFr": "Bruxelles-Capitale",
        "answerNl": "Brussels Hoofdstedelijk Gewest"
      }
    ]
  },
  {
    "type": 2,
    "difficulty": 1,
    "isActive": true,
    "isPriority": false,
    "category": "Science",
    "textFr": "Combien d'os y a-t-il dans le corps humain adulte?",
    "textNl": "Hoeveel botten zijn er in het volwassen menselijk lichaam?",
    "themeId": null,
    "mcqDetails": {
      "choiceAFr": "186",
      "choiceANl": "186",
      "choiceBFr": "206",
      "choiceBNl": "206",
      "choiceCFr": "226",
      "choiceCNl": "226",
      "correctChoice": 1
    }
  },
  {
    "type": 3,
    "difficulty": 3,
    "isActive": true,
    "isPriority": true,
    "category": "Belgian History",
    "textFr": "En quelle année la Belgique a-t-elle obtenu son indépendance?",
    "textNl": "In welk jaar werd België onafhankelijk?",
    "regularDetails": {
      "answerFr": "1830",
      "answerNl": "1830"
    }
  }
]`;

export default function QuestionsPage() {
  const [questions, setQuestions] = useState<Question[]>([]);
  const [filteredQuestions, setFilteredQuestions] = useState<Question[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingQuestion, setEditingQuestion] = useState<Question | null>(null);
  const [importResult, setImportResult] = useState<BulkImportResult | null>(null);
  const [showBulkImport, setShowBulkImport] = useState(false);
  const [jsonInput, setJsonInput] = useState(SAMPLE_JSON);
  const fileInputRef = useRef<HTMLInputElement>(null);

  // Filters
  const [typeFilter, setTypeFilter] = useState<QuestionType | ''>('');
  const [difficultyFilter, setDifficultyFilter] = useState<number | ''>('');
  const [activeFilter, setActiveFilter] = useState<boolean | ''>('');
  const [priorityFilter, setPriorityFilter] = useState<boolean | ''>('');
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    loadQuestions();
  }, []);

  useEffect(() => {
    applyFilters();
  }, [questions, typeFilter, difficultyFilter, activeFilter, priorityFilter, searchText]);

  const loadQuestions = async () => {
    setLoading(true);
    const data = await questionService.getAll();
    setQuestions(data);
    setLoading(false);
  };

  const applyFilters = () => {
    let filtered = [...questions];

    if (typeFilter !== '') {
      filtered = filtered.filter(q => q.type === typeFilter);
    }
    if (difficultyFilter !== '') {
      filtered = filtered.filter(q => q.difficulty === difficultyFilter);
    }
    if (activeFilter !== '') {
      filtered = filtered.filter(q => q.isActive === activeFilter);
    }
    if (priorityFilter !== '') {
      filtered = filtered.filter(q => q.isPriority === priorityFilter);
    }
    if (searchText) {
      const search = searchText.toLowerCase();
      filtered = filtered.filter(
        q => q.textFr.toLowerCase().includes(search) || q.textNl.toLowerCase().includes(search)
      );
    }

    setFilteredQuestions(filtered);
  };

  const handleCreate = () => {
    setEditingQuestion(null);
    setShowForm(true);
  };

  const handleEdit = (question: Question) => {
    setEditingQuestion(question);
    setShowForm(true);
  };

  const handleFormSubmit = async (dto: CreateQuestionDto) => {
    if (editingQuestion) {
      await questionService.update(editingQuestion.id, dto);
    } else {
      await questionService.create(dto);
    }
    setShowForm(false);
    setEditingQuestion(null);
    loadQuestions();
  };

  const handleDelete = async (id: string) => {
    if (confirm('Are you sure you want to delete this question?')) {
      await questionService.delete(id);
      loadQuestions();
    }
  };

  const handleToggleActive = async (id: string) => {
    await questionService.toggleActive(id);
    loadQuestions();
  };

  const handleBulkImportClick = () => {
    setShowBulkImport(true);
    setJsonInput(SAMPLE_JSON);
  };

  const handleImportFromJson = async () => {
    try {
      const questions = JSON.parse(jsonInput) as CreateQuestionDto[];

      if (!Array.isArray(questions)) {
        alert('JSON must be an array of questions');
        return;
      }

      const result = await questionService.bulkImport(questions);
      setImportResult(result);
      setShowBulkImport(false);
      loadQuestions();
    } catch (error) {
      alert(`Import failed: ${error instanceof Error ? error.message : 'Invalid JSON format'}`);
    }
  };

  const handleFileSelect = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (!file) return;

    try {
      const text = await file.text();
      setJsonInput(text);
      setShowBulkImport(true);

      // Clear file input
      if (fileInputRef.current) {
        fileInputRef.current.value = '';
      }
    } catch (error) {
      alert(`File read failed: ${error instanceof Error ? error.message : 'Unknown error'}`);
    }
  };

  const getTypeLabel = (type: QuestionType) => {
    switch (type) {
      case QuestionType.Regular:
        return 'Regular';
      case QuestionType.Regular4:
        return 'Regular4';
      case QuestionType.List:
        return 'List';
      case QuestionType.Mcq:
        return 'MCQ';
      default:
        return 'Unknown';
    }
  };

  if (showBulkImport) {
    return (
      <div className={styles['questions-page']}>
        <div className={styles['page-header']}>
          <h1>Bulk Import Questions (JSON)</h1>
          <button onClick={() => setShowBulkImport(false)}>Cancel</button>
        </div>

        <div style={{ padding: '20px' }}>
          <div style={{ marginBottom: '15px' }}>
            <h3>Instructions</h3>
            <p>Paste your JSON array of questions below. The sample shows all 4 question types:</p>
            <ul>
              <li><strong>Type 0:</strong> Regular (Phase 1 - Fast Buzzer)</li>
              <li><strong>Type 1:</strong> List (Phase 2 - Multiple answers)</li>
              <li><strong>Type 2:</strong> MCQ (Phase 3 - Multiple choice, requires themeId)</li>
              <li><strong>Type 3:</strong> Regular4 (Phase 4 - Chrono)</li>
            </ul>
            <p><strong>Note:</strong> For MCQ questions, set <code>themeId</code> to <code>null</code> or get a valid theme ID from the Themes page.</p>
          </div>

          <div style={{ marginBottom: '15px' }}>
            <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
              JSON Input:
            </label>
            <textarea
              value={jsonInput}
              onChange={(e) => setJsonInput(e.target.value)}
              style={{
                width: '100%',
                minHeight: '400px',
                fontFamily: 'monospace',
                fontSize: '12px',
                padding: '10px',
                border: '1px solid #ccc',
                borderRadius: '4px'
              }}
              placeholder="Paste your JSON array here..."
            />
          </div>

          <div style={{ display: 'flex', gap: '10px' }}>
            <button onClick={handleImportFromJson} className={styles['btn-primary']}>
              Import Questions
            </button>
            <button onClick={() => setJsonInput(SAMPLE_JSON)} className={styles['btn-secondary']}>
              Reset to Sample
            </button>
            <label className={styles['btn-secondary']} style={{ cursor: 'pointer', display: 'inline-block', padding: '8px 16px' }}>
              Load from File
              <input
                ref={fileInputRef}
                type="file"
                accept=".json"
                style={{ display: 'none' }}
                onChange={handleFileSelect}
              />
            </label>
          </div>
        </div>
      </div>
    );
  }

  if (showForm) {
    return (
      <div className={styles['questions-page']}>
        <div className={styles['page-header']}>
          <h1>{editingQuestion ? 'Edit Question' : 'Create Question'}</h1>
          <button onClick={() => setShowForm(false)}>Cancel</button>
        </div>
        <QuestionForm question={editingQuestion} onSubmit={handleFormSubmit} />
      </div>
    );
  }

  return (
    <div className={styles['questions-page']}>
      <div className={styles['page-header']}>
        <h1>Question Management</h1>
        <div>
          <button onClick={handleCreate} className={styles['btn-primary']}>Create New Question</button>
          <button onClick={handleBulkImportClick} className={styles['btn-secondary']} style={{ marginLeft: '10px' }}>
            Bulk Import JSON
          </button>
        </div>
      </div>

      {importResult && (
        <div className={styles['import-result']} style={{
          padding: '15px',
          margin: '15px 0',
          border: '1px solid #ddd',
          borderRadius: '4px',
          backgroundColor: importResult.failureCount === 0 ? '#d4edda' : importResult.successCount === 0 ? '#f8d7da' : '#fff3cd'
        }}>
          <h3>Bulk Import Result</h3>
          <p>Total: {importResult.totalQuestions} | Success: {importResult.successCount} | Failed: {importResult.failureCount}</p>
          {importResult.errors.length > 0 && (
            <div style={{ marginTop: '10px' }}>
              <strong>Errors:</strong>
              <ul>
                {importResult.errors.map((error, index) => (
                  <li key={index}>
                    Question #{error.questionIndex + 1}: {error.errorMessage}
                    {error.questionTextFr && ` (${error.questionTextFr})`}
                  </li>
                ))}
              </ul>
            </div>
          )}
          <button onClick={() => setImportResult(null)} style={{ marginTop: '10px' }}>Close</button>
        </div>
      )}

      <div className={styles['filters']}>
        <select value={typeFilter} onChange={e => setTypeFilter(e.target.value === '' ? '' : Number(e.target.value))}>
          <option value="">All Types</option>
          <option value={QuestionType.Regular}>Regular (Phase 1)</option>
          <option value={QuestionType.Regular4}>Regular4 (Phase 4)</option>
          <option value={QuestionType.List}>List</option>
          <option value={QuestionType.Mcq}>MCQ</option>
        </select>

        <select value={difficultyFilter} onChange={e => setDifficultyFilter(e.target.value === '' ? '' : Number(e.target.value))}>
          <option value="">All Difficulties</option>
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
        </select>

        <select value={activeFilter.toString()} onChange={e => setActiveFilter(e.target.value === '' ? '' : e.target.value === 'true')}>
          <option value="">All Status</option>
          <option value="true">Active</option>
          <option value="false">Inactive</option>
        </select>

        <select value={priorityFilter.toString()} onChange={e => setPriorityFilter(e.target.value === '' ? '' : e.target.value === 'true')}>
          <option value="">All Priorities</option>
          <option value="true">Priority</option>
          <option value="false">Non Priority</option>
        </select>

        <input
          type="text"
          placeholder="Search text..."
          value={searchText}
          onChange={e => setSearchText(e.target.value)}
        />
      </div>

      {loading ? (
        <div>Loading...</div>
      ) : (
        <div className={styles['questions-list']}>
          {filteredQuestions.length === 0 ? (
            <div className={styles['empty-state']}>No questions found</div>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>Type</th>
                  <th>Difficulty</th>
                  <th>Theme</th>
                  <th>Text (FR)</th>
                  <th>Text (NL)</th>
                  <th>Status</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredQuestions.map(question => (
                  <tr key={question.id}>
                    <td>{getTypeLabel(question.type)}</td>
                    <td>{question.difficulty}</td>
                    <td>
                      {question.theme ? (
                        <span className={styles['theme-badge']} title={`${question.theme.nameFr} / ${question.theme.nameNl}`}>
                          {question.theme.nameFr}
                        </span>
                      ) : (
                        <span className={styles['no-theme']}>-</span>
                      )}
                    </td>
                    <td>{question.textFr}</td>
                    <td>{question.textNl}</td>
                    <td>
                      <span className={`${styles['status']} ${question.isActive ? styles['active'] : styles['inactive']}`}>
                        {question.isActive ? 'Active' : 'Inactive'}
                      </span>
                    </td>
                    <td>
                      <div className={styles['actions']}>
                        <button onClick={() => handleEdit(question)} className={styles['btn-edit']}>Edit</button>
                        <button onClick={() => handleToggleActive(question.id)} className={styles['btn-toggle']}>
                          {question.isActive ? 'Deactivate' : 'Activate'}
                        </button>
                        <button onClick={() => handleDelete(question.id)} className={styles['btn-delete']}>Delete</button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      )}
    </div>
  );
}
