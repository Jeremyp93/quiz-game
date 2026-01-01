import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import QuestionsPage from './pages/QuestionsPage';
import ThemesPage from './pages/ThemesPage';
import ControlPage from './pages/ControlPage';
import DisplayPage from './pages/DisplayPage';
import LoginPage from './pages/LoginPage';
import RequireGM from './components/RequireGM';
import './App.css';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/questions" element={<RequireGM><QuestionsPage /></RequireGM>} />
        <Route path="/themes" element={<RequireGM><ThemesPage /></RequireGM>} />
        <Route path="/control" element={<RequireGM><ControlPage /></RequireGM>} />
        <Route path="/display" element={<DisplayPage />} />
      </Routes>
    </BrowserRouter>
  );
}

function HomePage() {
  return (
    <div style={{ padding: '2rem' }}>
      <h1>Quiz Game MVP</h1>
      <nav style={{ display: 'flex', gap: '1rem', marginTop: '2rem', flexWrap: 'wrap' }}>
        <Link to="/questions" style={{ padding: '1rem', background: '#007bff', color: 'white', textDecoration: 'none', borderRadius: '4px' }}>
          Admin - Questions
        </Link>
        <Link to="/themes" style={{ padding: '1rem', background: '#667eea', color: 'white', textDecoration: 'none', borderRadius: '4px' }}>
          Admin - Themes
        </Link>
        <Link to="/control" style={{ padding: '1rem', background: '#28a745', color: 'white', textDecoration: 'none', borderRadius: '4px' }}>
          Game Master - Control
        </Link>
        <Link to="/display" style={{ padding: '1rem', background: '#6f42c1', color: 'white', textDecoration: 'none', borderRadius: '4px' }}>
          Projector - Display
        </Link>
      </nav>
    </div>
  );
}

export default App;
