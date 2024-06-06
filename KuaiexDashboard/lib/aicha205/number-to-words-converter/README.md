# Number to Words Converter

Convert numbers to words using this React component.

## Installation

You can install the `number-to-words-converter` package from npm:

```bash
npm install number-to-words-converter
```
# Usage
## Importing the Component

You can import the NumberToWordsConverter component in your React application as follows:
import React from 'react';
import NumberToWordsConverter from 'number-to-words-converter';

# Rendering the Component
Once you have imported the component, you can render it in your application's UI

# Props
The NumberToWordsConverter component currently does not accept any additional props.

# Functionality
The NumberToWordsConverter component allows users to input a number in the input field and click the "Convert" button to convert the number to its word representation. The converted words will be displayed below the input field.

Please note that the conversion logic used in this component handles numbers in the range of integers (positive and negative) up to 999 vigintillion.

# Example
Here's a simple example of using the NumberToWordsConverter component in your React application:
```bash
import React from 'react';
import NumberToWordsConverter from 'number-to-words-converter';

function App() {
  return (
    <div>
      <h1>Number to Words Converter</h1>
      <NumberToWordsConverter />
    </div>
  );
}
export default App;
```

