/**
 * Checks if French and Dutch text are identical
 */
export function isDuplicateText(textFr: string, textNl: string): boolean {
  return textFr.trim().toLowerCase() === textNl.trim().toLowerCase();
}
