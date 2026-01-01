import { Question, CreateQuestionDto, QuestionType, BulkImportResult } from '../types';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '';
const API_BASE = `${API_BASE_URL}/api`;

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
    const response = await fetch(url, { credentials: 'include' });
    return response.json();
  },

  async getById(id: string): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions/${id}`, { credentials: 'include' });
    return response.json();
  },

  async create(dto: CreateQuestionDto): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify(dto),
    });
    return response.json();
  },

  async update(id: string, dto: CreateQuestionDto): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify(dto),
    });
    return response.json();
  },

  async delete(id: string): Promise<void> {
    await fetch(`${API_BASE}/questions/${id}`, {
      method: 'DELETE',
      credentials: 'include',
    });
  },

  async toggleActive(id: string): Promise<void> {
    await fetch(`${API_BASE}/questions/${id}/toggle-active`, {
      method: 'POST',
      credentials: 'include',
    });
  },

  async bulkImport(questions: CreateQuestionDto[]): Promise<BulkImportResult> {
    const response = await fetch(`${API_BASE}/questions/bulk-import`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify(questions),
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.error || 'Bulk import failed');
    }

    return response.json();
  },
};
