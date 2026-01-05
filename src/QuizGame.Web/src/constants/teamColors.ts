export interface TeamColor {
  gradient: string;
  glow: string;
  name: string;
  solid: string;
}

export const TEAM_COLORS: TeamColor[] = [
  { gradient: 'linear-gradient(135deg, #ffd700 0%, #ffed4e 100%)', glow: 'rgba(255, 215, 0, 0.5)', name: 'yellow', solid: 'rgba(255, 215, 99, 1)' },
  { gradient: 'linear-gradient(135deg, #ff0000 0%, #ff4444 100%)', glow: 'rgba(255, 0, 0, 0.5)', name: 'red', solid: 'rgba(229, 79, 68, 1)' },
  { gradient: 'linear-gradient(135deg, #00c853 0%, #00e676 100%)', glow: 'rgba(0, 200, 83, 0.5)', name: 'green', solid: 'rgba(0, 179, 94, 1)' },
  { gradient: 'linear-gradient(135deg, #2979ff 0%, #448aff 100%)', glow: 'rgba(41, 121, 255, 0.5)', name: 'blue', solid: 'rgba(78, 153, 255, 1)' },
  { gradient: 'linear-gradient(135deg, #fa709a 0%, #fee140 100%)', glow: 'rgba(250, 112, 154, 0.5)', name: 'sunset', solid: '#fa709a' },
  { gradient: 'linear-gradient(135deg, #30cfd0 0%, #330867 100%)', glow: 'rgba(48, 207, 208, 0.5)', name: 'teal', solid: '#30cfd0' },
];
