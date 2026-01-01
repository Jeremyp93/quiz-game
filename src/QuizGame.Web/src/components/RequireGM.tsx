import { useEffect, useState } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { authService } from '../services/authService';

export default function RequireGM({ children }: { children: React.ReactNode }) {
  const [loading, setLoading] = useState(true);
  const [isGM, setIsGM] = useState(false);
  const location = useLocation();

  useEffect(() => {
    checkAuth();
  }, []);

  const checkAuth = async () => {
    const user = await authService.getCurrentUser();
    setIsGM(user.authenticated && user.role === 'GM');
    setLoading(false);
  };

  if (loading) {
    return (
      <div style={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '100vh',
        background: '#f5f5f5'
      }}>
        <div style={{
          fontSize: '1.2rem',
          color: '#666'
        }}>Loading...</div>
      </div>
    );
  }

  if (!isGM) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <>{children}</>;
}
