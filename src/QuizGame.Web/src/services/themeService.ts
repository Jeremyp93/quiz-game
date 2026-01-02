import { Theme, CreateThemeDto } from '../types';
import { authService } from './authService';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '';
const API_BASE = `${API_BASE_URL}/api`;

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

export const themeService = {
  async getAll(isActive?: boolean, search?: string): Promise<Theme[]> {
    const params = new URLSearchParams();
    if (isActive !== undefined) params.append('isActive', isActive.toString());
    if (search) params.append('search', search);

    const url = `${API_BASE}/themes${params.toString() ? '?' + params.toString() : ''}`;
    const response = await fetch(url, {
      headers: getAuthHeaders()
    });
    return response.json();
  },

  async getById(id: string): Promise<Theme> {
    const response = await fetch(`${API_BASE}/themes/${id}`, {
      headers: getAuthHeaders()
    });
    return response.json();
  },

  async create(theme: CreateThemeDto): Promise<Theme> {
    const response = await fetch(`${API_BASE}/themes`, {
      method: 'POST',
      headers: getAuthHeaders(),
      body: JSON.stringify(theme),
    });
    return response.json();
  },

  async update(id: string, theme: CreateThemeDto): Promise<Theme> {
    const response = await fetch(`${API_BASE}/themes/${id}`, {
      method: 'PUT',
      headers: getAuthHeaders(),
      body: JSON.stringify(theme),
    });
    return response.json();
  },

  async delete(id: string): Promise<void> {
    await fetch(`${API_BASE}/themes/${id}`, {
      method: 'DELETE',
      headers: getAuthHeaders(),
    });
  },

  async toggleActive(id: string): Promise<void> {
    await fetch(`${API_BASE}/themes/${id}/toggle`, {
      method: 'POST',
      headers: getAuthHeaders(),
    });
  },
};
