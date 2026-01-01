import { Theme, CreateThemeDto } from '../types';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '';
const API_BASE = `${API_BASE_URL}/api`;

export const themeService = {
  async getAll(isActive?: boolean, search?: string): Promise<Theme[]> {
    const params = new URLSearchParams();
    if (isActive !== undefined) params.append('isActive', isActive.toString());
    if (search) params.append('search', search);

    const url = `${API_BASE}/themes${params.toString() ? '?' + params.toString() : ''}`;
    const response = await fetch(url, { credentials: 'include' });
    return response.json();
  },

  async getById(id: string): Promise<Theme> {
    const response = await fetch(`${API_BASE}/themes/${id}`, { credentials: 'include' });
    return response.json();
  },

  async create(theme: CreateThemeDto): Promise<Theme> {
    const response = await fetch(`${API_BASE}/themes`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify(theme),
    });
    return response.json();
  },

  async update(id: string, theme: CreateThemeDto): Promise<Theme> {
    const response = await fetch(`${API_BASE}/themes/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify(theme),
    });
    return response.json();
  },

  async delete(id: string): Promise<void> {
    await fetch(`${API_BASE}/themes/${id}`, {
      method: 'DELETE',
      credentials: 'include',
    });
  },

  async toggleActive(id: string): Promise<void> {
    await fetch(`${API_BASE}/themes/${id}/toggle`, {
      method: 'POST',
      credentials: 'include',
    });
  },
};
