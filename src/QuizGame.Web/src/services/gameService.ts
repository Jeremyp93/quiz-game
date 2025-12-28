import * as signalR from '@microsoft/signalr';
import { GameState } from '../types';

class GameService {
  private connection: signalR.HubConnection | null = null;
  private stateListeners: ((state: GameState) => void)[] = [];

  async connect() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/gameHub')
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
}

export const gameService = new GameService();
