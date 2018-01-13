using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_1
{
    class DisplayOperations
    {
        string displayText;
        string formattedText;
        bool decimalPresent = false;
        bool multiDecimalPresent = false;
        bool tooBig = false, exp = false, needsComma = false;
        int decimalPosition = 0, leftOfDecimalLength = 0, rightOfDecimalLength = 0;
        bool isANumber = true;
        
        int characterLimit = 19; //limits the length of number left / right of decimal in calc...
        int displayLimit = 8; //Sets the display Limit will display exponent if over limit....

        public DisplayOperations()
        {

        }

        public DisplayOperations(string text)
        {
            displayText = text;
        }

        public DisplayOperations(string text, bool isANumber)
        {
            displayText = text;
            this.isANumber = isANumber;
        }

        public DisplayOperations(int maxStringLength, int maxDisplayStringLength)
        {
            characterLimit = maxStringLength;
            displayLimit = maxDisplayStringLength;
        }

        public DisplayOperations(string text, int maxStringLength, int maxDisplayStringLength)
        {
            displayText = text;
            characterLimit = maxStringLength;
            displayLimit = maxDisplayStringLength;
        }
        
        public DisplayOperations(string text, bool isANumber, int maxDisplayStringLength)
        {
            displayText = text;
            this.isANumber = isANumber;
            displayLimit = maxDisplayStringLength;
        }

        public DisplayOperations(string text, bool isANumber, int maxStringLength, int maxDisplayStringLength)
        {
            displayText = text;
            this.isANumber = isANumber;
            characterLimit = maxStringLength;
            displayLimit = maxDisplayStringLength;
        }

        public string GetFormattedDisplayText(string text, bool isANumber, int maxStringLength, int maxDisplayStringLength)
        {
            displayText = text;
            this.isANumber = isANumber;
            characterLimit = maxStringLength;
            displayLimit = maxDisplayStringLength;
            ValidateDisplayText();

            return FormattedText;
        }

        public string GetFormattedDisplayText(string text, int maxStringLength, int maxDisplayStringLength)
        {
            displayText = text;
            characterLimit = maxStringLength;
            displayLimit = maxDisplayStringLength;
            ValidateDisplayText();

            return FormattedText;
        }

        public string GetFormattedDisplayText(string text, bool isANumber)
        {
            displayText = text;
            this.isANumber = isANumber;
            ValidateDisplayText();

            return FormattedText;
        }
        
        public string FormattedText { get => formattedText; set => formattedText = value; }

        //Get Decimal Position, if any
        private void GetDecimalInformation()
        {
            if (displayText.Contains('.'))
            {
                decimalPresent = true;

                if (displayText.IndexOf('.') == displayText.LastIndexOf('.'))
                {                    
                    decimalPosition = displayText.IndexOf('.');
                    leftOfDecimalLength = decimalPosition - 1;
                    rightOfDecimalLength = displayText.Length - decimalPosition - 1;
                }
                else
                {
                    multiDecimalPresent = true;
                    isANumber = false;
                    FormattedText = "too many '.'";
                }
            }            
        }
        
        //Display rules and check for violation....
        private void ValidateDisplayText()
        {
            GetDecimalInformation();

            if (isANumber)
            {

                if (displayText.Length > characterLimit)
                {
                    tooBig = true;
                    FormattedText = "TOO BIG";
                }
                else if (displayText.Length > displayLimit)
                {
                    exp = true;
                    CreateExponent();
                }
                else if (decimalPresent && leftOfDecimalLength > 3)
                {
                    needsComma = true;
                    InsertComma();
                }
                else if (!decimalPresent && displayText.Length > 3)
                {
                    needsComma = true;
                    InsertComma();
                }
                else
                {
                    FormattedText = displayText;
                }
            }
            else
            {
                FormattedText = displayText;
            }
        }

        private void CreateExponent()
        {   
            string eRight = DefineRightOfExp();
            string eLeft = DefineLeftOfExp();

            FormattedText = eLeft + "e" + eRight;
        }

        private string DefineRightOfExp()
        {
            double e;
            string workingText = displayText;

            if (!decimalPresent)
            {
                int fromPosition = workingText.Length;
                int toPosition = 1;
                e = fromPosition - toPosition;
            }
            else if (leftOfDecimalLength > 1 )
            {
                int fromPosition = decimalPosition;
                int toPosition = 1;
                e = fromPosition - toPosition;                
            }
            else
            {
                int firstNonZeroNumber = 0;

                do
                {
                   firstNonZeroNumber += 1;
                } while (workingText[firstNonZeroNumber].Equals("0"));
                
                int toPosition = firstNonZeroNumber;
                int fromPosition = 0;

                e = fromPosition - toPosition;
            }

            return e.ToString();
        }

        private string DefineLeftOfExp()
        {
            //evaluate string.  Use Exponential Notation 5.1234E4.
            double d = Convert.ToDouble(displayText);
            string result = displayText.Remove(displayLimit);            

            if (d >= 1.000)
            {
                double e = Convert.ToDouble(result);
                e = e / Math.Pow(10, displayLimit - 1);
                e = Math.Round(e, displayLimit - 4);
                result = e.ToString();
            }
            else
            {
                int firstNonZeroNumber = 0;
                do
                {
                    firstNonZeroNumber += 1;
                } while (result[firstNonZeroNumber].Equals("0"));

                result.Remove(0, firstNonZeroNumber);
                result.Insert(1, ".");                
            }

            return result;
        } 

        private void InsertComma()
        {
            int commaPosition = 3; //if 3, commas will be inserted every 3rd character.
            int holder;
            StringBuilder s = new StringBuilder(displayText);

            if (!decimalPresent)
            {
                holder = s.Length;
            }
            else
            {
                holder = decimalPosition;
            }
            
            while (holder > commaPosition)
            {            
                holder -= commaPosition; //"12345" length = 5 & string is an array of char so end is 4 (0-4)
                s.Insert(holder, ","); // using comment above 5-3 inserts at array[3] or moves 3 over and inserts comma between 2 & 3...12,345                
            };

            FormattedText = s.ToString();
        }
    }  
}
