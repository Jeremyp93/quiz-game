const API_BASE = import.meta.env.VITE_API_BASE_URL || '';
const AUTH_URL = `${API_BASE}/api/auth`;

export interface AuthUser {
  authenticated: boolean;
  role?: 'GM' | 'Viewer';
  username?: string;
  reason?: string;
}

class AuthService {
  async login(username: string, password: string): Promise<{ success: boolean; error?: string }> {
    try {
      const response = await fetch(`${AUTH_URL}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const error = await response.json();
        return { success: false, error: error.error || 'Login failed' };
      }

      return { success: true };
    } catch (error) {
      return { success: false, error: 'Network error' };
    }
  }

  async logout(): Promise<void> {
    await fetch(`${AUTH_URL}/logout`, {
      method: 'POST',
      credentials: 'include',
    });
  }

  async verifyViewerCode(code: string): Promise<{ success: boolean; error?: string }> {
    try {
      const response = await fetch(`${AUTH_URL}/viewer/verify`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify({ code }),
      });

      if (!response.ok) {
        const error = await response.json();
        return { success: false, error: error.error || 'Verification failed' };
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
        credentials: 'include',
      });
      const data = await response.json();
      return data;
    } catch (error) {
      return { authenticated: false };
    }
  }

  isViewerVerifiedLocally(): boolean {
    return localStorage.getItem('viewer_verified') === 'true';
  }

  clearViewerVerification(): void {
    localStorage.removeItem('viewer_verified');
  }
}

export const authService = new AuthService();
