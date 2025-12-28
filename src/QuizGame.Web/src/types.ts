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

export interface Question {
  id: string;
  type: QuestionType;
  difficulty: number;
  isActive: boolean;
  category?: string;
  tags?: string;
  textFr: string;
  textNl: string;
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

export interface GameState {
  isGameStarted: boolean;
  players: string[];
  teams: Team[];
  currentPhase: Phase;
  currentScene: Scene;
  lastSceneBeforeScoreboard?: Scene;

  // Phase 1 (Fast Buzzer) state
  currentQuestion?: CurrentQuestion;
  lastQuestionId?: string;
  blockedNextQuestionTeamIds: number[];
  blockedTeamIdsForCurrentQuestion: number[];
}
