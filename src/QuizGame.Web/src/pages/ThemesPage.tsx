import { useState, useEffect } from 'react';
import { Theme, CreateThemeDto } from '../types';
import { themeService } from '../services/themeService';
import './ThemesPage.css';

export default function ThemesPage() {
  const [themes, setThemes] = useState<Theme[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [showForm, setShowForm] = useState(false);
  const [editingTheme, setEditingTheme] = useState<Theme | null>(null);
  const [filterActive, setFilterActive] = useState<boolean | undefined>(undefined);
  const [searchText, setSearchText] = useState('');

  const [formData, setFormData] = useState<CreateThemeDto>({
    nameFr: '',
    nameNl: '',
    code: '',
    isActive: true,
    sortOrder: undefined,
  });

  useEffect(() => {
    loadThemes();
  }, [filterActive, searchText]);

  const loadThemes = async () => {
    try {
      setLoading(true);
      const data = await themeService.getFiltered(filterActive, searchText);
      setThemes(data);
      setError('');
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to load themes');
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingTheme) {
        await themeService.update(editingTheme.id, formData);
      } else {
        await themeService.create(formData);
      }
      resetForm();
      loadThemes();
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to save theme');
    }
  };

  const handleEdit = (theme: Theme) => {
    setEditingTheme(theme);
    setFormData({
      nameFr: theme.nameFr,
      nameNl: theme.nameNl,
      code: theme.code,
      isActive: theme.isActive,
      sortOrder: theme.sortOrder,
    });
    setShowForm(true);
  };

  const handleDelete = async (theme: Theme) => {
    if (theme.questionCount > 0) {
      if (!window.confirm(
        `Warning: Theme "${theme.nameFr}" has ${theme.questionCount} linked question(s). ` +
        `Deleting this theme will set those questions' theme to null. Continue?`
      )) {
        return;
      }
    } else {
      if (!window.confirm(`Delete theme "${theme.nameFr}"?`)) {
        return;
      }
    }

    try {
      await themeService.delete(theme.id);
      loadThemes();
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to delete theme');
    }
  };

  const handleToggleActive = async (theme: Theme) => {
    try {
      await themeService.toggleActive(theme.id);
      loadThemes();
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to toggle theme');
    }
  };

  const resetForm = () => {
    setFormData({
      nameFr: '',
      nameNl: '',
      code: '',
      isActive: true,
      sortOrder: undefined,
    });
    setEditingTheme(null);
    setShowForm(false);
  };

  return (
    <div className="themes-page">
      <h1>Theme Management</h1>

      {error && <div className="error-message">{error}</div>}

      <div className="themes-header">
        <button onClick={() => setShowForm(!showForm)} className="btn-add-theme">
          {showForm ? 'Cancel' : '+ Add Theme'}
        </button>

        <div className="theme-filters">
          <select
            value={filterActive === undefined ? 'all' : filterActive ? 'active' : 'inactive'}
            onChange={(e) => {
              const val = e.target.value;
              setFilterActive(val === 'all' ? undefined : val === 'active');
            }}
            className="filter-select"
          >
            <option value="all">All Themes</option>
            <option value="active">Active Only</option>
            <option value="inactive">Inactive Only</option>
          </select>

          <input
            type="text"
            placeholder="Search themes..."
            value={searchText}
            onChange={(e) => setSearchText(e.target.value)}
            className="search-input"
          />
        </div>
      </div>

      {showForm && (
        <div className="theme-form-container">
          <h2>{editingTheme ? 'Edit Theme' : 'Create Theme'}</h2>
          <form onSubmit={handleSubmit} className="theme-form">
            <div className="form-row">
              <div className="form-group">
                <label>Name (French) *</label>
                <input
                  type="text"
                  value={formData.nameFr}
                  onChange={(e) => setFormData({ ...formData, nameFr: e.target.value })}
                  required
                  placeholder="e.g., Géographie"
                />
              </div>

              <div className="form-group">
                <label>Name (Dutch) *</label>
                <input
                  type="text"
                  value={formData.nameNl}
                  onChange={(e) => setFormData({ ...formData, nameNl: e.target.value })}
                  required
                  placeholder="e.g., Geografie"
                />
              </div>
            </div>

            <div className="form-row">
              <div className="form-group">
                <label>Code *</label>
                <input
                  type="text"
                  value={formData.code}
                  onChange={(e) => setFormData({ ...formData, code: e.target.value.toLowerCase() })}
                  required
                  pattern="[a-z0-9_]+"
                  placeholder="e.g., geography"
                  title="Lowercase letters, numbers, and underscores only"
                />
                <small>Lowercase letters, numbers, and underscores only</small>
              </div>

              <div className="form-group">
                <label>Sort Order</label>
                <input
                  type="number"
                  value={formData.sortOrder || ''}
                  onChange={(e) => setFormData({ ...formData, sortOrder: e.target.value ? parseInt(e.target.value) : undefined })}
                  placeholder="Optional"
                />
              </div>
            </div>

            <div className="form-group">
              <label className="checkbox-label">
                <input
                  type="checkbox"
                  checked={formData.isActive}
                  onChange={(e) => setFormData({ ...formData, isActive: e.target.checked })}
                />
                Active
              </label>
            </div>

            <div className="form-actions">
              <button type="submit" className="btn-submit">
                {editingTheme ? 'Update' : 'Create'} Theme
              </button>
              <button type="button" onClick={resetForm} className="btn-cancel">
                Cancel
              </button>
            </div>
          </form>
        </div>
      )}

      {loading ? (
        <div className="loading">Loading themes...</div>
      ) : (
        <div className="themes-list">
          <table className="themes-table">
            <thead>
              <tr>
                <th>Name (FR)</th>
                <th>Name (NL)</th>
                <th>Code</th>
                <th>Questions</th>
                <th>Sort Order</th>
                <th>Status</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {themes.length === 0 ? (
                <tr>
                  <td colSpan={7} className="no-data">
                    No themes found. Create your first theme!
                  </td>
                </tr>
              ) : (
                themes.map((theme) => (
                  <tr key={theme.id} className={!theme.isActive ? 'inactive-row' : ''}>
                    <td>{theme.nameFr}</td>
                    <td>{theme.nameNl}</td>
                    <td><code>{theme.code}</code></td>
                    <td>
                      <span className="question-count">
                        {theme.questionCount}
                      </span>
                    </td>
                    <td>{theme.sortOrder || '-'}</td>
                    <td>
                      <span className={`status-badge ${theme.isActive ? 'active' : 'inactive'}`}>
                        {theme.isActive ? 'Active' : 'Inactive'}
                      </span>
                    </td>
                    <td className="actions-cell">
                      <button onClick={() => handleEdit(theme)} className="btn-edit" title="Edit">
                        ✏️
                      </button>
                      <button
                        onClick={() => handleToggleActive(theme)}
                        className="btn-toggle"
                        title={theme.isActive ? 'Deactivate' : 'Activate'}
                      >
                        {theme.isActive ? '👁️' : '🚫'}
                      </button>
                      <button onClick={() => handleDelete(theme)} className="btn-delete" title="Delete">
                        🗑️
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
