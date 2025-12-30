import { useState, useEffect } from 'react';
import { Theme, CreateThemeDto } from '../types';
import { themeService } from '../services/themeService';
import styles from './ThemesPage.module.css';

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
    icon: '📚',
    isActive: true,
    sortOrder: undefined,
  });

  useEffect(() => {
    loadThemes();
  }, [filterActive, searchText]);

  const loadThemes = async () => {
    try {
      setLoading(true);
      const data = await themeService.getAll(filterActive, searchText);
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
      icon: theme.icon,
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
      icon: '📚',
      isActive: true,
      sortOrder: undefined,
    });
    setEditingTheme(null);
    setShowForm(false);
  };

  return (
    <div className={styles['themes-page']}>
      <h1>Theme Management</h1>

      {error && <div className={styles['error-message']}>{error}</div>}

      <div className={styles['themes-header']}>
        <button onClick={() => setShowForm(!showForm)} className={styles['btn-add-theme']}>
          {showForm ? 'Cancel' : '+ Add Theme'}
        </button>

        <div className={styles['theme-filters']}>
          <select
            value={filterActive === undefined ? 'all' : filterActive ? 'active' : 'inactive'}
            onChange={(e) => {
              const val = e.target.value;
              setFilterActive(val === 'all' ? undefined : val === 'active');
            }}
            className={styles['filter-select']}
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
            className={styles['search-input']}
          />
        </div>
      </div>

      {showForm && (
        <div className={styles['theme-form-container']}>
          <h2>{editingTheme ? 'Edit Theme' : 'Create Theme'}</h2>
          <form onSubmit={handleSubmit} className={styles['theme-form']}>
            <div className={styles['form-row']}>
              <div className={styles['form-group']}>
                <label>Name (French) *</label>
                <input
                  type="text"
                  value={formData.nameFr}
                  onChange={(e) => setFormData({ ...formData, nameFr: e.target.value })}
                  required
                  placeholder="e.g., Géographie"
                />
              </div>

              <div className={styles['form-group']}>
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

            <div className={styles['form-row']}>
              <div className={styles['form-group']}>
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

              <div className={styles['form-group']}>
                <label>Icon *</label>
                <input
                  type="text"
                  value={formData.icon}
                  onChange={(e) => setFormData({ ...formData, icon: e.target.value })}
                  required
                  placeholder="e.g., 🌍"
                  maxLength={10}
                />
                <small>Emoji icon for the theme</small>
              </div>
            </div>

            <div className={styles['form-row']}>
              <div className={styles['form-group']}>
                <label>Sort Order</label>
                <input
                  type="number"
                  value={formData.sortOrder || ''}
                  onChange={(e) => setFormData({ ...formData, sortOrder: e.target.value ? parseInt(e.target.value) : undefined })}
                  placeholder="Optional"
                />
              </div>
            </div>

            <div className={styles['form-group']}>
              <label className={styles['checkbox-label']}>
                <input
                  type="checkbox"
                  checked={formData.isActive}
                  onChange={(e) => setFormData({ ...formData, isActive: e.target.checked })}
                />
                Active
              </label>
            </div>

            <div className={styles['form-actions']}>
              <button type="submit" className={styles['btn-submit']}>
                {editingTheme ? 'Update' : 'Create'} Theme
              </button>
              <button type="button" onClick={resetForm} className={styles['btn-cancel']}>
                Cancel
              </button>
            </div>
          </form>
        </div>
      )}

      {loading ? (
        <div className={styles['loading']}>Loading themes...</div>
      ) : (
        <div className={styles['themes-list']}>
          <table className={styles['themes-table']}>
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
                  <td colSpan={7} className={styles['no-data']}>
                    No themes found. Create your first theme!
                  </td>
                </tr>
              ) : (
                themes.map((theme) => (
                  <tr key={theme.id} className={!theme.isActive ? styles['inactive-row'] : ''}>
                    <td>{theme.nameFr}</td>
                    <td>{theme.nameNl}</td>
                    <td><code>{theme.code}</code></td>
                    <td>
                      <span className={styles['question-count']}>
                        {theme.questionCount}
                      </span>
                    </td>
                    <td>{theme.sortOrder || '-'}</td>
                    <td>
                      <span className={`${styles['status-badge']} ${theme.isActive ? styles['active'] : styles['inactive']}`}>
                        {theme.isActive ? 'Active' : 'Inactive'}
                      </span>
                    </td>
                    <td className={styles['actions-cell']}>
                      <button onClick={() => handleEdit(theme)} className={styles['btn-edit']} title="Edit">
                        ✏️
                      </button>
                      <button
                        onClick={() => handleToggleActive(theme)}
                        className={styles['btn-toggle']}
                        title={theme.isActive ? 'Deactivate' : 'Activate'}
                      >
                        {theme.isActive ? '👁️' : '🚫'}
                      </button>
                      <button onClick={() => handleDelete(theme)} className={styles['btn-delete']} title="Delete">
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
