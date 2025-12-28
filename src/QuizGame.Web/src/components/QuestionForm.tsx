import { useState, useEffect } from 'react';
import { Question, QuestionType, CreateQuestionDto, McqChoice, ListQuestionAnswer } from '../types';
import './QuestionForm.css';

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
    if (question) {
      setType(question.type);
      setDifficulty(question.difficulty);
      setIsActive(question.isActive);
      setCategory(question.category || '');
      setTags(question.tags || '');
      setTextFr(question.textFr);
      setTextNl(question.textNl);

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

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const dto: CreateQuestionDto = {
      type,
      difficulty,
      isActive,
      category: category || undefined,
      tags: tags || undefined,
      textFr,
      textNl,
    };

    if (type === QuestionType.Regular) {
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
    <form onSubmit={handleSubmit} className="question-form">
      <div className="form-section">
        <h3>Question Type & Settings</h3>

        <div className="form-group">
          <label>Question Type *</label>
          <select value={type} onChange={e => setType(Number(e.target.value))} required>
            <option value={QuestionType.Regular}>Regular (Q&A)</option>
            <option value={QuestionType.List}>List (Multiple Answers)</option>
            <option value={QuestionType.Mcq}>MCQ (3 Choices)</option>
          </select>
        </div>

        <div className="form-row">
          <div className="form-group">
            <label>Difficulty (1-3) *</label>
            <select value={difficulty} onChange={e => setDifficulty(Number(e.target.value))} required>
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
            </select>
          </div>

          <div className="form-group">
            <label>
              <input type="checkbox" checked={isActive} onChange={e => setIsActive(e.target.checked)} />
              Active
            </label>
          </div>
        </div>

        <div className="form-row">
          <div className="form-group">
            <label>Category</label>
            <input type="text" value={category} onChange={e => setCategory(e.target.value)} />
          </div>

          <div className="form-group">
            <label>Tags (semicolon-separated)</label>
            <input type="text" value={tags} onChange={e => setTags(e.target.value)} />
          </div>
        </div>
      </div>

      <div className="form-section">
        <h3>Question Text (Bilingual)</h3>

        <div className="form-group">
          <label>Text (French) *</label>
          <textarea value={textFr} onChange={e => setTextFr(e.target.value)} required rows={3} />
        </div>

        <div className="form-group">
          <label>Text (Dutch) *</label>
          <textarea value={textNl} onChange={e => setTextNl(e.target.value)} required rows={3} />
        </div>
      </div>

      {type === QuestionType.Regular && (
        <div className="form-section">
          <h3>Answer (Regular)</h3>

          <div className="form-group">
            <label>Answer (French) *</label>
            <input type="text" value={answerFr} onChange={e => setAnswerFr(e.target.value)} required />
          </div>

          <div className="form-group">
            <label>Answer (Dutch) *</label>
            <input type="text" value={answerNl} onChange={e => setAnswerNl(e.target.value)} required />
          </div>
        </div>
      )}

      {type === QuestionType.Mcq && (
        <div className="form-section">
          <h3>Multiple Choice Options</h3>

          <div className="mcq-choice">
            <h4>Choice A</h4>
            <div className="form-row">
              <div className="form-group">
                <label>Choice A (French) *</label>
                <input type="text" value={choiceAFr} onChange={e => setChoiceAFr(e.target.value)} required />
              </div>
              <div className="form-group">
                <label>Choice A (Dutch) *</label>
                <input type="text" value={choiceANl} onChange={e => setChoiceANl(e.target.value)} required />
              </div>
            </div>
          </div>

          <div className="mcq-choice">
            <h4>Choice B</h4>
            <div className="form-row">
              <div className="form-group">
                <label>Choice B (French) *</label>
                <input type="text" value={choiceBFr} onChange={e => setChoiceBFr(e.target.value)} required />
              </div>
              <div className="form-group">
                <label>Choice B (Dutch) *</label>
                <input type="text" value={choiceBNl} onChange={e => setChoiceBNl(e.target.value)} required />
              </div>
            </div>
          </div>

          <div className="mcq-choice">
            <h4>Choice C</h4>
            <div className="form-row">
              <div className="form-group">
                <label>Choice C (French) *</label>
                <input type="text" value={choiceCFr} onChange={e => setChoiceCFr(e.target.value)} required />
              </div>
              <div className="form-group">
                <label>Choice C (Dutch) *</label>
                <input type="text" value={choiceCNl} onChange={e => setChoiceCNl(e.target.value)} required />
              </div>
            </div>
          </div>

          <div className="form-group">
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
        <div className="form-section">
          <h3>List Answers</h3>

          {listAnswers.map((answer, index) => (
            <div key={answer.id} className="list-answer-item">
              <div className="form-row">
                <div className="form-group">
                  <label>Answer {index + 1} (French) *</label>
                  <input
                    type="text"
                    value={answer.answerFr}
                    onChange={e => updateListAnswer(index, 'answerFr', e.target.value)}
                    required
                  />
                </div>
                <div className="form-group">
                  <label>Answer {index + 1} (Dutch) *</label>
                  <input
                    type="text"
                    value={answer.answerNl}
                    onChange={e => updateListAnswer(index, 'answerNl', e.target.value)}
                    required
                  />
                </div>
              </div>
              <div className="form-group">
                <label>Alternative Spellings</label>
                <input
                  type="text"
                  value={answer.altSpellings || ''}
                  onChange={e => updateListAnswer(index, 'altSpellings', e.target.value)}
                />
              </div>
              <button type="button" onClick={() => removeListAnswer(index)} className="btn-remove">
                Remove
              </button>
            </div>
          ))}

          <button type="button" onClick={addListAnswer} className="btn-add">
            Add Answer
          </button>
        </div>
      )}

      <div className="form-actions">
        <button type="submit" className="btn-submit">
          {question ? 'Update Question' : 'Create Question'}
        </button>
      </div>
    </form>
  );
}
