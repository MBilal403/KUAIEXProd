import { numberToWords } from './numbertowords';

describe('numberToWords', () => {
  it('should convert single-digit numbers', () => {
    expect(numberToWords(0)).toBe('zero');
    expect(numberToWords(5)).toBe('five');
  });

  it('should convert two-digit numbers', () => {
    expect(numberToWords(11)).toBe('eleven');
    expect(numberToWords(37)).toBe('thirty seven');
  });

  it('should convert large numbers', () => {
    expect(numberToWords(123456789)).toBe('one hundred and twenty three million, four hundred and fifty six thousand, seven hundred and eighty nine');
  });

});
