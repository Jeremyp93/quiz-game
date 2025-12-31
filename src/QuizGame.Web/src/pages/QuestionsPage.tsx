import { useState, useEffect } from 'react';
import { Question, QuestionType, CreateQuestionDto } from '../types';
import { questionService } from '../services/questionService';
import QuestionForm from '../components/QuestionForm';
import styles from './QuestionsPage.module.css';

export default function QuestionsPage() {
  const [questions, setQuestions] = useState<Question[]>([]);
  const [filteredQuestions, setFilteredQuestions] = useState<Question[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingQuestion, setEditingQuestion] = useState<Question | null>(null);

  // Filters
  const [typeFilter, setTypeFilter] = useState<QuestionType | ''>('');
  const [difficultyFilter, setDifficultyFilter] = useState<number | ''>('');
  const [activeFilter, setActiveFilter] = useState<boolean | ''>('');
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    loadQuestions();
  }, []);

  useEffect(() => {
    applyFilters();
  }, [questions, typeFilter, difficultyFilter, activeFilter, searchText]);

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
        <button onClick={handleCreate} className={styles['btn-primary']}>Create New Question</button>
      </div>

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
