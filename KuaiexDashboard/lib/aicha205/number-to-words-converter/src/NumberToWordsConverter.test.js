
import React from 'react';
import { render, fireEvent, screen } from '@testing-library/react';
import NumberToWordsConverter from './NumberToWordsConverter';

describe('NumberToWordsConverter', () => {
  it('should render the component correctly', () => {
    render(<NumberToWordsConverter />);
  });

  it('should handle user input and convert number to words', () => {
    render(<NumberToWordsConverter />);

    const inputField = screen.getByPlaceholderText('Enter a number');
    const convertButton = screen.getByText('Convert');

    fireEvent.change(inputField, { target: { value: '123' } });

    fireEvent.click(convertButton);

    expect(screen.getByText('one hundred and twenty three')).toBeInTheDocument();
});

});
