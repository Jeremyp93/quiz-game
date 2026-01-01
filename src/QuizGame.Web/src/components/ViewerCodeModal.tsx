import { useState } from 'react';
import { authService } from '../services/authService';

interface ViewerCodeModalProps {
  onVerified: () => void;
}

export default function ViewerCodeModal({ onVerified }: ViewerCodeModalProps) {
  const [code, setCode] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    const result = await authService.verifyViewerCode(code);

    setLoading(false);

    if (result.success) {
      onVerified();
    } else {
      setError(result.error || 'Invalid code');
      setCode('');
    }
  };

  return (
    <div style={{
      position: 'fixed',
      top: 0,
      left: 0,
      right: 0,
      bottom: 0,
      background: 'rgba(0, 0, 0, 0.85)',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      zIndex: 9999,
      backdropFilter: 'blur(4px)'
    }}>
      <div style={{
        background: 'white',
        padding: '2.5rem',
        borderRadius: '16px',
        width: '90%',
        maxWidth: '450px',
        boxShadow: '0 20px 60px rgba(0, 0, 0, 0.3)'
      }}>
        <h2 style={{
          marginBottom: '1rem',
          textAlign: 'center',
          color: '#333',
          fontSize: '1.8rem',
          fontWeight: '600'
        }}>Enter Viewer Code</h2>
        <p style={{
          marginBottom: '2rem',
          textAlign: 'center',
          color: '#666',
          fontSize: '1rem',
          lineHeight: '1.5'
        }}>
          Please enter the 6-digit code shown on the Game Master screen
        </p>

        <form onSubmit={handleSubmit}>
          <input
            type="text"
            value={code}
            onChange={(e) => setCode(e.target.value.replace(/\D/g, '').slice(0, 6))}
            placeholder="000000"
            maxLength={6}
            autoFocus
            required
            style={{
              width: '100%',
              padding: '1.25rem',
              fontSize: '2.5rem',
              textAlign: 'center',
              letterSpacing: '0.75rem',
              border: '3px solid #e0e0e0',
              borderRadius: '12px',
              marginBottom: '1.25rem',
              fontFamily: 'monospace',
              fontWeight: 'bold',
              outline: 'none',
              transition: 'border-color 0.2s'
            }}
            onFocus={(e) => e.target.style.borderColor = '#6f42c1'}
            onBlur={(e) => e.target.style.borderColor = '#e0e0e0'}
          />

          {error && (
            <div style={{
              padding: '0.875rem',
              marginBottom: '1.25rem',
              background: '#fee',
              color: '#c33',
              borderRadius: '8px',
              textAlign: 'center',
              fontSize: '0.95rem',
              border: '1px solid #fcc'
            }}>
              {error}
            </div>
          )}

          <button
            type="submit"
            disabled={loading || code.length !== 6}
            style={{
              width: '100%',
              padding: '1.125rem',
              background: (loading || code.length !== 6) ? '#b8a3d1' : '#6f42c1',
              color: 'white',
              border: 'none',
              borderRadius: '12px',
              fontSize: '1.25rem',
              fontWeight: '600',
              cursor: (loading || code.length !== 6) ? 'not-allowed' : 'pointer',
              transition: 'background 0.2s',
              boxShadow: '0 4px 12px rgba(111, 66, 193, 0.4)'
            }}
            onMouseEnter={(e) => {
              if (!loading && code.length === 6) {
                e.currentTarget.style.background = '#5a2d9c';
              }
            }}
            onMouseLeave={(e) => {
              if (!loading && code.length === 6) {
                e.currentTarget.style.background = '#6f42c1';
              }
            }}
          >
            {loading ? 'Verifying...' : 'Submit'}
          </button>
        </form>
      </div>
    </div>
  );
}
