import { useState, useEffect } from 'react';
import { Question, QuestionType, CreateQuestionDto, McqChoice, ListQuestionAnswer, Theme } from '../types';
import { themeService } from '../services/themeService';
import styles from './QuestionForm.module.css';

interface Props {
  question: Question | null;
  onSubmit: (dto: CreateQuestionDto) => void;
}

export default function QuestionForm({ question, onSubmit }: Props) {
  const [type, setType] = useState<QuestionType>(QuestionType.Regular);
  const [difficulty, setDifficulty] = useState(1);
  const [isActive, setIsActive] = useState(true);
  const [category, setCategory] = useState('');
  const [tags, setTags] = useState('');
  const [textFr, setTextFr] = useState('');
  const [textNl, setTextNl] = useState('');
  const [themeId, setThemeId] = useState<string>('');
  const [themes, setThemes] = useState<Theme[]>([]);

  // Regular
  const [answerFr, setAnswerFr] = useState('');
  const [answerNl, setAnswerNl] = useState('');

  // MCQ
  const [choiceAFr, setChoiceAFr] = useState('');
  const [choiceANl, setChoiceANl] = useState('');
  const [choiceBFr, setChoiceBFr] = useState('');
  const [choiceBNl, setChoiceBNl] = useState('');
  const [choiceCFr, setChoiceCFr] = useState('');
  const [choiceCNl, setChoiceCNl] = useState('');
  const [correctChoice, setCorrectChoice] = useState<McqChoice>(McqChoice.A);

  // List
  const [listAnswers, setListAnswers] = useState<ListQuestionAnswer[]>([]);

  useEffect(() => {
    loadThemes();
  }, []);

  useEffect(() => {
    if (question) {
      setType(question.type);
      setDifficulty(question.difficulty);
      setIsActive(question.isActive);
      setCategory(question.category || '');
      setTags(question.tags || '');
      setTextFr(question.textFr);
      setTextNl(question.textNl);
      setThemeId(question.themeId || '');

      if (question.regularDetails) {
        setAnswerFr(question.regularDetails.answerFr);
        setAnswerNl(question.regularDetails.answerNl);
      }

      if (question.mcqDetails) {
        setChoiceAFr(question.mcqDetails.choiceAFr);
        setChoiceANl(question.mcqDetails.choiceANl);
        setChoiceBFr(question.mcqDetails.choiceBFr);
        setChoiceBNl(question.mcqDetails.choiceBNl);
        setChoiceCFr(question.mcqDetails.choiceCFr);
        setChoiceCNl(question.mcqDetails.choiceCNl);
        setCorrectChoice(question.mcqDetails.correctChoice);
      }

      if (question.listAnswers) {
        setListAnswers(question.listAnswers);
      }
    }
  }, [question]);

  const loadThemes = async () => {
    const data = await themeService.getAll();
    setThemes(data);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    // Validate theme for MCQ questions
    if (type === QuestionType.Mcq && !themeId) {
      alert('Theme is required for MCQ questions (Phase 3 requirement)');
      return;
    }

    const dto: CreateQuestionDto = {
      type,
      difficulty,
      isActive,
      category: category || undefined,
      tags: tags || undefined,
      textFr,
      textNl,
      themeId: themeId || undefined,
    };

    if (type === QuestionType.Regular || type === QuestionType.Regular4) {
      dto.regularDetails = { answerFr, answerNl };
    } else if (type === QuestionType.Mcq) {
      dto.mcqDetails = {
        choiceAFr,
        choiceANl,
        choiceBFr,
        choiceBNl,
        choiceCFr,
        choiceCNl,
        correctChoice,
      };
    } else if (type === QuestionType.List) {
      dto.listAnswers = listAnswers;
    }

    onSubmit(dto);
  };

  const addListAnswer = () => {
    setListAnswers([
      ...listAnswers,
      {
        id: crypto.randomUUID(),
        answerFr: '',
        answerNl: '',
        altSpellings: '',
      },
    ]);
  };

  const updateListAnswer = (index: number, field: keyof ListQuestionAnswer, value: string) => {
    const updated = [...listAnswers];
    updated[index] = { ...updated[index], [field]: value };
    setListAnswers(updated);
  };

  const removeListAnswer = (index: number) => {
    setListAnswers(listAnswers.filter((_, i) => i !== index));
  };

  return (
    <form onSubmit={handleSubmit} className={styles['question-form']}>
      <div className={styles['form-section']}>
        <h3>Question Type & Settings</h3>

        <div className={styles['form-group']}>
          <label>Question Type *</label>
          <select value={type} onChange={e => setType(Number(e.target.value))} required>
            <option value={QuestionType.Regular}>Regular (Q&A) - Phase 1</option>
            <option value={QuestionType.Regular4}>Regular4 (Q&A) - Phase 4</option>
            <option value={QuestionType.List}>List (Multiple Answers)</option>
            <option value={QuestionType.Mcq}>MCQ (3 Choices)</option>
          </select>
        </div>

        <div className={styles['form-group']}>
          <label>
            Theme {type === QuestionType.Mcq && <span className={styles['required-mark']}>*</span>}
          </label>
          <select
            value={themeId}
            onChange={e => setThemeId(e.target.value)}
            required={type === QuestionType.Mcq}
          >
            <option value="">-- No Theme --</option>
            {themes.map(theme => (
              <option key={theme.id} value={theme.id}>
                {theme.nameFr} / {theme.nameNl}
              </option>
            ))}
          </select>
          {type === QuestionType.Mcq && (
            <small style={{ color: '#666', fontSize: '0.875rem' }}>
              Required for MCQ questions (Phase 3)
            </small>
          )}
        </div>

        <div className={styles['form-row']}>
          <div className={styles['form-group']}>
            <label>Difficulty (1-3) *</label>
            <select value={difficulty} onChange={e => setDifficulty(Number(e.target.value))} required>
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
            </select>
          </div>

          <div className={styles['form-group']}>
            <label>
              <input type="checkbox" checked={isActive} onChange={e => setIsActive(e.target.checked)} />
              Active
            </label>
          </div>
        </div>

        <div className={styles['form-row']}>
          <div className={styles['form-group']}>
            <label>Category</label>
            <input type="text" value={category} onChange={e => setCategory(e.target.value)} />
          </div>

          <div className={styles['form-group']}>
            <label>Tags (semicolon-separated)</label>
            <input type="text" value={tags} onChange={e => setTags(e.target.value)} />
          </div>
        </div>
      </div>

      <div className={styles['form-section']}>
        <h3>Question Text (Bilingual)</h3>

        <div className={styles['form-group']}>
          <label>Text (French) *</label>
          <textarea value={textFr} onChange={e => setTextFr(e.target.value)} required rows={3} />
        </div>

        <div className={styles['form-group']}>
          <label>Text (Dutch) *</label>
          <textarea value={textNl} onChange={e => setTextNl(e.target.value)} required rows={3} />
        </div>
      </div>

      {(type === QuestionType.Regular || type === QuestionType.Regular4) && (
        <div className={styles['form-section']}>
          <h3>Answer (Regular)</h3>

          <div className={styles['form-group']}>
            <label>Answer (French) *</label>
            <input type="text" value={answerFr} onChange={e => setAnswerFr(e.target.value)} required />
          </div>

          <div className={styles['form-group']}>
            <label>Answer (Dutch) *</label>
            <input type="text" value={answerNl} onChange={e => setAnswerNl(e.target.value)} required />
          </div>
        </div>
      )}

      {type === QuestionType.Mcq && (
        <div className={styles['form-section']}>
          <h3>Multiple Choice Options</h3>

          <div className={styles['mcq-choice']}>
            <h4>Choice A</h4>
            <div className={styles['form-row']}>
              <div className={styles['form-group']}>
                <label>Choice A (French) *</label>
                <input type="text" value={choiceAFr} onChange={e => setChoiceAFr(e.target.value)} required />
              </div>
              <div className={styles['form-group']}>
                <label>Choice A (Dutch) *</label>
                <input type="text" value={choiceANl} onChange={e => setChoiceANl(e.target.value)} required />
              </div>
            </div>
          </div>

          <div className={styles['mcq-choice']}>
            <h4>Choice B</h4>
            <div className={styles['form-row']}>
              <div className={styles['form-group']}>
                <label>Choice B (French) *</label>
                <input type="text" value={choiceBFr} onChange={e => setChoiceBFr(e.target.value)} required />
              </div>
              <div className={styles['form-group']}>
                <label>Choice B (Dutch) *</label>
                <input type="text" value={choiceBNl} onChange={e => setChoiceBNl(e.target.value)} required />
              </div>
            </div>
          </div>

          <div className={styles['mcq-choice']}>
            <h4>Choice C</h4>
            <div className={styles['form-row']}>
              <div className={styles['form-group']}>
                <label>Choice C (French) *</label>
                <input type="text" value={choiceCFr} onChange={e => setChoiceCFr(e.target.value)} required />
              </div>
              <div className={styles['form-group']}>
                <label>Choice C (Dutch) *</label>
                <input type="text" value={choiceCNl} onChange={e => setChoiceCNl(e.target.value)} required />
              </div>
            </div>
          </div>

          <div className={styles['form-group']}>
            <label>Correct Choice *</label>
            <select value={correctChoice} onChange={e => setCorrectChoice(Number(e.target.value))} required>
              <option value={McqChoice.A}>A</option>
              <option value={McqChoice.B}>B</option>
              <option value={McqChoice.C}>C</option>
            </select>
          </div>
        </div>
      )}

      {type === QuestionType.List && (
        <div className={styles['form-section']}>
          <h3>List Answers</h3>

          {listAnswers.map((answer, index) => (
            <div key={answer.id} className={styles['list-answer-item']}>
              <div className={styles['form-row']}>
                <div className={styles['form-group']}>
                  <label>Answer {index + 1} (French) *</label>
                  <input
                    type="text"
                    value={answer.answerFr}
                    onChange={e => updateListAnswer(index, 'answerFr', e.target.value)}
                    required
                  />
                </div>
                <div className={styles['form-group']}>
                  <label>Answer {index + 1} (Dutch) *</label>
                  <input
                    type="text"
                    value={answer.answerNl}
                    onChange={e => updateListAnswer(index, 'answerNl', e.target.value)}
                    required
                  />
                </div>
              </div>
              <div className={styles['form-group']}>
                <label>Alternative Spellings</label>
                <input
                  type="text"
                  value={answer.altSpellings || ''}
                  onChange={e => updateListAnswer(index, 'altSpellings', e.target.value)}
                />
              </div>
              <button type="button" onClick={() => removeListAnswer(index)} className={styles['btn-remove']}>
                Remove
              </button>
            </div>
          ))}

          <button type="button" onClick={addListAnswer} className={styles['btn-add']}>
            Add Answer
          </button>
        </div>
      )}

      <div className={styles['form-actions']}>
        <button type="submit" className={styles['btn-submit']}>
          {question ? 'Update Question' : 'Create Question'}
        </button>
      </div>
    </form>
  );
}
