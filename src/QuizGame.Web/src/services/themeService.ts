import axios from 'axios';
import { Theme, CreateThemeDto } from '../types';

const API_URL = 'http://localhost:5000/api/themes';

export const themeService = {
  async getAll(): Promise<Theme[]> {
    const response = await axios.get<Theme[]>(API_URL);
    return response.data;
  },

  async getFiltered(isActive?: boolean, search?: string): Promise<Theme[]> {
    const params: any = {};
    if (isActive !== undefined) params.isActive = isActive;
    if (search) params.search = search;

    const response = await axios.get<Theme[]>(API_URL, { params });
    return response.data;
  },

  async getById(id: string): Promise<Theme> {
    const response = await axios.get<Theme>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(theme: CreateThemeDto): Promise<Theme> {
    const response = await axios.post<Theme>(API_URL, theme);
    return response.data;
  },

  async update(id: string, theme: CreateThemeDto): Promise<Theme> {
    const response = await axios.put<Theme>(`${API_URL}/${id}`, theme);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },

  async toggleActive(id: string): Promise<void> {
    await axios.patch(`${API_URL}/${id}/toggle`);
  },
};
