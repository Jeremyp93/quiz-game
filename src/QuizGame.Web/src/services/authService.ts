const API_BASE = import.meta.env.VITE_API_BASE_URL || '';
const AUTH_URL = `${API_BASE}/api/auth`;

export interface AuthUser {
  authenticated: boolean;
  role?: 'GM' | 'Viewer';
  username?: string;
  reason?: string;
}

interface LoginResponse {
  success: boolean;
  role?: string;
  token?: string;
  expiresIn?: number;
  error?: string;
}

class AuthService {
  private readonly TOKEN_KEY = 'quiz_game_token';

  private getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  private removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  private getAuthHeaders(): HeadersInit {
    const token = this.getToken();
    if (token) {
      return {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      };
    }
    return { 'Content-Type': 'application/json' };
  }

  async login(username: string, password: string): Promise<{ success: boolean; error?: string }> {
    try {
      const response = await fetch(`${AUTH_URL}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const error = await response.json();
        return { success: false, error: error.error || 'Login failed' };
      }

      const data: LoginResponse = await response.json();

      if (data.token) {
        this.setToken(data.token);
      }

      return { success: true };
    } catch (error) {
      return { success: false, error: 'Network error' };
    }
  }

  async logout(): Promise<void> {
    try {
      const token = this.getToken();
      if (token) {
        await fetch(`${AUTH_URL}/logout`, {
          method: 'POST',
          headers: this.getAuthHeaders(),
        });
      }
    } finally {
      this.removeToken();
    }
  }

  async verifyViewerCode(code: string): Promise<{ success: boolean; error?: string }> {
    try {
      const response = await fetch(`${AUTH_URL}/viewer/verify`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ code }),
      });

      if (!response.ok) {
        const error = await response.json();
        return { success: false, error: error.error || 'Verification failed' };
      }

      const data: LoginResponse = await response.json();

      if (data.token) {
        this.setToken(data.token);
      }

      // Store verification flag in localStorage
      localStorage.setItem('viewer_verified', 'true');
      return { success: true };
    } catch (error) {
      return { success: false, error: 'Network error' };
    }
  }

  async getCurrentUser(): Promise<AuthUser> {
    try {
      const response = await fetch(`${AUTH_URL}/me`, {
        headers: this.getAuthHeaders(),
      });

      const data = await response.json();

      // If session invalidated, remove token
      if (!data.authenticated || data.reason === 'session_invalidated') {
        this.removeToken();
      }

      return data;
    } catch (error) {
      this.removeToken();
      return { authenticated: false };
    }
  }

  isViewerVerifiedLocally(): boolean {
    return localStorage.getItem('viewer_verified') === 'true';
  }

  clearViewerVerification(): void {
    localStorage.removeItem('viewer_verified');
  }

  // Expose token for SignalR connection
  getTokenForSignalR(): string | null {
    return this.getToken();
  }
}

export const authService = new AuthService();
