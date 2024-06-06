export function numberToWords(num) {
    const units = ['', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
    const teens = ['eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
    const tens = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
    const scales = ['', 'thousand', 'million', 'billion', 'trillion', 'quadrillion', 'quintillion', 'sextillion', 'septillion', 'octillion', 'nonillion', 'decillion', 'undecillion', 'duodecillion', 'tredecillion', 'quatttuor-decillion', 'quindecillion', 'sexdecillion', 'septen-decillion', 'octodecillion', 'novemdecillion', 'vigintillion'];
  
    if (num === 0) {
      return 'zero';
    }
  
    // Function to convert a number less than 1000 to words
    function convertLessThanOneThousand(number) {
      if (number === 0) {
        return '';
      }
  
      let words = '';
  
      if (number < 10) {
        words += units[number];
      } else if (number < 20) {
        words += teens[number - 11];
      } else if (number < 100) {
        words += tens[Math.floor(number / 10)];
        if (number % 10 !== 0) {
          words += ' ' + units[number % 10];
        }
      } else {
        words += units[Math.floor(number / 100)] + ' hundred';
        if (number % 100 !== 0) {
          words += ' and ' + convertLessThanOneThousand(number % 100);
        }
      }
  
      return words;
    }
  
    let words = '';
    let currentScale = 0;
  
    while (num > 0) {
      const chunk = num % 1000;
      if (chunk !== 0) {
        let chunkWords = convertLessThanOneThousand(chunk);
        if (currentScale > 0) {
          chunkWords += ' ' + scales[currentScale];
        }
        if (words !== '') {
          words = chunkWords + ', ' + words;
        } else {
          words = chunkWords;
        }
      }
      num = Math.floor(num / 1000);
      currentScale++;
    }
  
    return words;
  }
  
  