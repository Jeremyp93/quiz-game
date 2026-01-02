import * as signalR from '@microsoft/signalr';
import { GameState } from '../types';
import { authService } from './authService';

class GameService {
  private connection: signalR.HubConnection | null = null;
  private stateListeners: ((state: GameState) => void)[] = [];

  async connect() {
    const baseUrl = import.meta.env.VITE_API_BASE_URL || '';
    const hubUrl = baseUrl ? `${baseUrl}/gameHub` : '/gameHub';

    const token = authService.getTokenForSignalR();

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl, {
        accessTokenFactory: () => token || ''
      })
      .withAutomaticReconnect()
      .build();

    this.connection.on('GameStateUpdated', (state: GameState) => {
      this.stateListeners.forEach(listener => listener(state));
    });

    await this.connection.start();
  }

  disconnect() {
    this.connection?.stop();
  }

  onStateUpdated(callback: (state: GameState) => void) {
    this.stateListeners.push(callback);
    return () => {
      this.stateListeners = this.stateListeners.filter(l => l !== callback);
    };
  }

  async startGame() {
    await this.connection?.invoke('StartGame');
  }

  async setPlayers(playerNames: string[]) {
    await this.connection?.invoke('SetPlayers', playerNames);
  }

  async createTeams() {
    await this.connection?.invoke('CreateTeams');
  }

  async renameTeam(teamIndex: number, newName: string) {
    await this.connection?.invoke('RenameTeam', teamIndex, newName);
  }

  async movePlayer(playerName: string, toTeamIndex: number) {
    await this.connection?.invoke('MovePlayer', playerName, toTeamIndex);
  }

  async adjustScore(teamIndex: number, delta: number) {
    await this.connection?.invoke('AdjustScore', teamIndex, delta);
  }

  async showTeamsScene() {
    await this.connection?.invoke('ShowTeamsScene');
  }

  async showScoreboard() {
    await this.connection?.invoke('ShowScoreboard');
  }

  async backToGame() {
    await this.connection?.invoke('BackToGame');
  }

  // Phase 1 (Fast Buzzer)

  async startPhase1() {
    await this.connection?.invoke('StartPhase1');
  }

  async getQuestion() {
    await this.connection?.invoke('GetQuestion');
  }

  async showQuestion() {
    await this.connection?.invoke('ShowQuestion');
  }

  async showAnswer() {
    await this.connection?.invoke('ShowAnswer');
  }

  async applyBlocksForNextQuestion(teamIndices: number[]) {
    await this.connection?.invoke('ApplyBlocksForNextQuestion', teamIndices);
  }

  async endPhase() {
    await this.connection?.invoke('EndPhase');
  }

  // Phase 2 (List)

  async startPhase2() {
    await this.connection?.invoke('StartPhase2');
  }

  async loadListQuestion() {
    await this.connection?.invoke('LoadListQuestion');
  }

  async showListQuestion() {
    await this.connection?.invoke('ShowListQuestion');
  }

  async setListTimerDuration(durationSeconds: number) {
    await this.connection?.invoke('SetListTimerDuration', durationSeconds);
  }

  async startListTimer() {
    await this.connection?.invoke('StartListTimer');
  }

  async pauseListTimer() {
    await this.connection?.invoke('PauseListTimer');
  }

  async resumeListTimer() {
    await this.connection?.invoke('ResumeListTimer');
  }

  async resetListTimer() {
    await this.connection?.invoke('ResetListTimer');
  }

  // Phase 3 (Sabotage)

  async startPhase3() {
    await this.connection?.invoke('StartPhase3');
  }

  async startThemeAssignment() {
    await this.connection?.invoke('StartThemeAssignment');
  }

  async assignThemeToTeam(teamIndex: number, themeId: string) {
    await this.connection?.invoke('AssignThemeToTeam', teamIndex, themeId);
  }

  async undoLastThemeAssignment() {
    await this.connection?.invoke('UndoLastThemeAssignment');
  }

  async startMcqSubphase() {
    await this.connection?.invoke('StartMcqSubphase');
  }

  async loadNextMcqQuestion() {
    await this.connection?.invoke('LoadNextMcqQuestion');
  }

  async showMcqQuestion() {
    await this.connection?.invoke('ShowMcqQuestion');
  }

  async selectMcqAnswer(choice: number) {
    await this.connection?.invoke('SelectMcqAnswer', choice);
  }

  async clearMcqAnswer() {
    await this.connection?.invoke('ClearMcqAnswer');
  }

  async revealMcqAnswer() {
    await this.connection?.invoke('RevealMcqAnswer');
  }

  async advanceToNextMcqQuestion() {
    await this.connection?.invoke('AdvanceToNextMcqQuestion');
  }

  // ============================================================
  // Phase 4 (Chrono) Methods
  // ============================================================

  async startPhase4() {
    await this.connection?.invoke('StartPhase4');
  }

  async selectTeamForChronoRun(teamIndex: number) {
    await this.connection?.invoke('SelectTeamForChronoRun', teamIndex);
  }

  async showNextChronoQuestion() {
    await this.connection?.invoke('ShowNextChronoQuestion');
  }

  async markChronoCorrect() {
    await this.connection?.invoke('MarkChronoCorrect');
  }

  async pauseChronoTimer() {
    await this.connection?.invoke('PauseChronoTimer');
  }

  async resumeChronoTimer() {
    await this.connection?.invoke('ResumeChronoTimer');
  }

  async resetChronoRun() {
    await this.connection?.invoke('ResetChronoRun');
  }

  async abortChronoRun() {
    await this.connection?.invoke('AbortChronoRun');
  }

  async forceFinishChronoRun() {
    await this.connection?.invoke('ForceFinishChronoRun');
  }
}

export const gameService = new GameService();
