import React, { useState } from 'react';
import { numberToWords } from './numbertowords';

const NumberToWordsConverter = () => {
  // State to store user input and the converted words
  const [inputNumber, setInputNumber] = useState('');
  const [convertedWords, setConvertedWords] = useState('');

  // Event handler for the input field
  const handleInputChange = (event) => {
    setInputNumber(event.target.value);
  };

  // Event handler for the "Convert" button
  const handleConvert = () => {
    // Convert the user input to a number
    const number = Number(inputNumber);

    // Check if the input is a valid number
    if (isNaN(number)) {
      setConvertedWords('Invalid input');
    } else {
      // Call the number-to-words conversion function
      const words = numberToWords(number);

      // Update the state with the converted words
      setConvertedWords(words);
    }
  };

  return (
    <div className="container mt-5">
      <h2>Number to Words Converter</h2>
      <div className="input-group mb-3">
        <input
          type="number"
          className="form-control"
          value={inputNumber}
          onChange={handleInputChange}
          placeholder="Enter a number"
        />
        <button className="btn btn-primary" onClick={handleConvert}>
          Convert
        </button>
      </div>
      <div>
        <strong>Converted Words:</strong>
        <p className="border border-secondary p-2">{convertedWords}</p>
      </div>
    </div>
  );
};

export default NumberToWordsConverter;
