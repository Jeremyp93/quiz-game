import { Question, CreateQuestionDto, QuestionType } from '../types';

const API_BASE = 'http://localhost:5000/api';

export const questionService = {
  async getAll(
    type?: QuestionType,
    difficulty?: number,
    isActive?: boolean,
    searchText?: string
  ): Promise<Question[]> {
    const params = new URLSearchParams();
    if (type !== undefined) params.append('type', type.toString());
    if (difficulty !== undefined) params.append('difficulty', difficulty.toString());
    if (isActive !== undefined) params.append('isActive', isActive.toString());
    if (searchText) params.append('searchText', searchText);

    const url = `${API_BASE}/questions${params.toString() ? '?' + params.toString() : ''}`;
    const response = await fetch(url);
    return response.json();
  },

  async getById(id: string): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions/${id}`);
    return response.json();
  },

  async create(dto: CreateQuestionDto): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return response.json();
  },

  async update(id: string, dto: CreateQuestionDto): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dto),
    });
    return response.json();
  },

  async delete(id: string): Promise<void> {
    await fetch(`${API_BASE}/questions/${id}`, {
      method: 'DELETE',
    });
  },

  async toggleActive(id: string): Promise<void> {
    await fetch(`${API_BASE}/questions/${id}/toggle-active`, {
      method: 'POST',
    });
  },
};
