export enum QuestionType {
  Regular = 0,
  List = 1,
  Mcq = 2,
}

export enum McqChoice {
  A = 0,
  B = 1,
  C = 2,
}

export enum Phase {
  Setup = 0,
  FastBuzzer = 1,
  List = 2,
  Sabotage = 3,
  Chrono = 4,
}

export enum Scene {
  Teams = 0,
  Scoreboard = 1,
  Question = 2,
  Answer = 3,
  ListQuestion = 4,
  TeamCreationLoading = 5,
  SabotageThemeAssignment = 6,
  SabotageMcqQuestion = 7,
  SabotageMcqAnswer = 8,
  QuestionTransition = 9,
  ChronoReady = 10,
  ChronoQuestion = 11,
  ChronoCompletion = 12,
  ChronoFailure = 13,
}

export enum TimerState {
  Idle = 0,
  Running = 1,
  Paused = 2,
  Finished = 3,
}

export interface RegularQuestionDetails {
  answerFr: string;
  answerNl: string;
}

export interface McqQuestionDetails {
  choiceAFr: string;
  choiceANl: string;
  choiceBFr: string;
  choiceBNl: string;
  choiceCFr: string;
  choiceCNl: string;
  correctChoice: McqChoice;
}

export interface ListQuestionAnswer {
  id: string;
  answerFr: string;
  answerNl: string;
  altSpellings?: string;
}

export interface Theme {
  id: string;
  nameFr: string;
  nameNl: string;
  code: string;
  icon: string;
  isActive: boolean;
  sortOrder?: number;
  questionCount: number;
}

export interface CreateThemeDto {
  nameFr: string;
  nameNl: string;
  code: string;
  icon: string;
  isActive: boolean;
  sortOrder?: number;
}

export interface Question {
  id: string;
  type: QuestionType;
  difficulty: number;
  isActive: boolean;
  category?: string;
  tags?: string;
  textFr: string;
  textNl: string;
  themeId?: string;
  theme?: Theme;
  regularDetails?: RegularQuestionDetails;
  mcqDetails?: McqQuestionDetails;
  listAnswers?: ListQuestionAnswer[];
}

export interface CreateQuestionDto {
  type: QuestionType;
  difficulty: number;
  isActive: boolean;
  category?: string;
  tags?: string;
  textFr: string;
  textNl: string;
  themeId?: string;
  regularDetails?: RegularQuestionDetails;
  mcqDetails?: McqQuestionDetails;
  listAnswers?: ListQuestionAnswer[];
}

export interface Team {
  name: string;
  players: string[];
  score: number;
}

export interface CurrentQuestion {
  id: string;
  textFr: string;
  textNl: string;
  answerFr: string;
  answerNl: string;
  difficulty: number;
}

export interface ListAnswer {
  answerFr: string;
  answerNl: string;
}

export interface CurrentListQuestion {
  id: string;
  textFr: string;
  textNl: string;
  answers: ListAnswer[];
  difficulty: number;
}

export interface ListTimer {
  durationSeconds: number;
  state: TimerState;
  startedAtUtc?: string;
  pausedAtUtc?: string;
  accumulatedPausedMs: number;
  finishedAtUtc?: string;
  boardsUpVisibleUntilUtc?: string;
}

export interface GameState {
  isGameStarted: boolean;
  players: string[];
  teams: Team[];
  currentPhase: Phase;
  currentScene: Scene;
  lastSceneBeforeScoreboard?: Scene;

  // Phase 1 (Fast Buzzer) state
  currentQuestion?: CurrentQuestion;
  isCurrentQuestionVisibleOnDisplay: boolean;
  lastQuestionId?: string;
  blockedNextQuestionTeamIds: number[];
  blockedTeamIdsForCurrentQuestion: number[];

  // Phase 2 (List) state
  currentListQuestion?: CurrentListQuestion;
  isCurrentListQuestionVisibleOnDisplay: boolean;
  lastListQuestionId?: string;
  listTimer: ListTimer;

  // Phase 3 (Sabotage) state
  sabotage: SabotageState;

  // Phase 4 (Chrono) state
  chrono: ChronoState;
}

export enum ChronoRunStatus {
  Idle = 0,
  Running = 1,
  Paused = 2,
  Finished = 3,
  NotFinished = 4,
  Aborted = 5,
}

export enum ChronoResultStatus {
  NotStarted = 0,
  Finished = 1,
  NotFinished = 2,
  Aborted = 3,
}

export interface ChronoTeamResult {
  status: ChronoResultStatus;
  timeMs?: number;
}

export interface ChronoState {
  activeTeamIndex?: number;
  runStatus: ChronoRunStatus;
  correctCount: number;
  currentQuestion?: CurrentQuestion;
  lastQuestionId?: string;
  timerState: TimerState;
  timerStartedAtUtc?: string;
  timerPausedAtUtc?: string;
  timerAccumulatedPausedMs: number;
  timerFinishedAtUtc?: string;
  bestTimeMs?: number;
  teamResults: Record<number, ChronoTeamResult>;
}

export enum SabotageSubphase {
  ThemeAssignment = 0,
  McqQuestions = 1,
}

export interface SabotageTheme {
  id: string;
  nameFr: string;
  nameNl: string;
  code: string;
  icon: string;
}

export interface TeamThemeAssignment {
  teamIndex: number;
  selfSelectedTheme?: SabotageTheme;
  sabotageTheme?: SabotageTheme;
}

export interface CurrentMcqQuestion {
  id: string;
  textFr: string;
  textNl: string;
  choiceAFr: string;
  choiceANl: string;
  choiceBFr: string;
  choiceBNl: string;
  choiceCFr: string;
  choiceCNl: string;
  correctChoice: McqChoice;
  difficulty: number;
  theme: SabotageTheme;
}

export interface SabotageState {
  currentSubphase: SabotageSubphase;
  selectedThemes: SabotageTheme[];
  teamThemeAssignments: TeamThemeAssignment[];
  currentPickingTeamIndex?: number;
  currentPickNumber: number; // 1 = self-select, 2 = sabotage another team
  isThemeAssignmentComplete: boolean;
  currentPlayingTeamIndex?: number;
  currentThemeIndex?: number;
  currentMcqQuestion?: CurrentMcqQuestion;
  selectedAnswer?: McqChoice;
  isAnswerRevealed: boolean;
  currentQuestionInTheme: number;
}
