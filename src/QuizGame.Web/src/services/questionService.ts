import { Question, CreateQuestionDto, QuestionType, BulkImportResult } from '../types';
import { authService } from './authService';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '';
const API_BASE = `${API_BASE_URL}/api`;

// Helper to get auth headers
const getAuthHeaders = (): HeadersInit => {
  const token = authService.getTokenForSignalR();
  const headers: Record<string, string> = {
    'Content-Type': 'application/json'
  };
  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }
  return headers;
};

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
    const response = await fetch(url, {
      headers: getAuthHeaders()
    });
    return response.json();
  },

  async getById(id: string): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions/${id}`, {
      headers: getAuthHeaders()
    });
    return response.json();
  },

  async create(dto: CreateQuestionDto): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions`, {
      method: 'POST',
      headers: getAuthHeaders(),
      body: JSON.stringify(dto),
    });
    return response.json();
  },

  async update(id: string, dto: CreateQuestionDto): Promise<Question> {
    const response = await fetch(`${API_BASE}/questions/${id}`, {
      method: 'PUT',
      headers: getAuthHeaders(),
      body: JSON.stringify(dto),
    });
    return response.json();
  },

  async delete(id: string): Promise<void> {
    await fetch(`${API_BASE}/questions/${id}`, {
      method: 'DELETE',
      headers: getAuthHeaders(),
    });
  },

  async toggleActive(id: string): Promise<void> {
    await fetch(`${API_BASE}/questions/${id}/toggle-active`, {
      method: 'POST',
      headers: getAuthHeaders(),
    });
  },

  async bulkImport(questions: CreateQuestionDto[]): Promise<BulkImportResult> {
    const response = await fetch(`${API_BASE}/questions/bulk-import`, {
      method: 'POST',
      headers: getAuthHeaders(),
      body: JSON.stringify(questions),
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.error || 'Bulk import failed');
    }

    return response.json();
  },
};
