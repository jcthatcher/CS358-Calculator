using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Calculator_1
{
    public partial class Form1 : Form
    {
        char ops;
        bool displayDirty;
        const int characterLimit = 19; //limits the length of number left / right of decimal in calc...
        const int displayLimit = 8; //Sets the display Limit will display exponent if over limit....
        CalculatorOps Calc;
        StringBuilder displayText;

        public Form1()
        {
            ops = '=';
            displayDirty = false;
            displayText = new StringBuilder("0");
            InitializeComponent();
        }
        
        private void UpdateDisplayString(char key)
        {
            //Validate character...
            if (!Calc.CharacterAllowed(key))
            {
                MessageBox.Show("Invalid Input. You entered: " + key.ToString());
            }     
            
            //Remove leading zeros...
            if(displayText.Length>0 && displayText[0] == '0')
            {
                displayText.Remove(0, 1);
            }

            //Add character to stringbuilder...
            displayText.Append(key);

            //Update display with sb value.
            WriteToDisplay(displayText.ToString());         
            
        }
        
        private void EvaluateOperatorKeyPress(char c) //operation key (+,*,-,/,=)  pressed.  
        {
            string result;
            double valid;

            //Check to see if there is new input in textbox...Ensure someone is not just hitting + over and over.
            if (displayText.Length>0 && displayDirty)
            {
                //Validate stringbuilder (display text) and write to double. Prompt...
                if (!double.TryParse(displayText.ToString(), out valid))
                {
                    MessageBox.Show("Invalid Input.You entered " + displayText.ToString());
                }
                else
                {
                    //String was converted to number for operations. Clear String Builder to accept new input....
                    displayText.Clear();
                    //displayText is cleared it is no longer dirty.
                    displayDirty = false;

                    //Perform any calculator operations previously required before preparing for new ops (i.e. new key press)
                    result = Calc.EvaluateOperator(ops, valid);

                    //Write result to the display.
                    WriteToDisplay(result);
                }  
            }
            
            //Record new ops to be performed according to key press.
            ops = c;            
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            displayText.Clear();
            ops = '=';
            Calc.ResetCalcatorOps();
            ClearDisplay();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Calc = new CalculatorOps();
            displayText = new StringBuilder("0");
        }
        
        private void ButtonClickCharacter(object sender, EventArgs e)
        {
            Button btnWorking = (Button)sender;
            displayDirty = true;
            UpdateDisplayString(Convert.ToChar(btnWorking.Text));
        }

        private void ButtonClickOperator(object sender, EventArgs e)
        {
            Button btnWorking = (Button)sender;
            EvaluateOperatorKeyPress(Convert.ToChar(btnWorking.Text));
        }

        private void WriteToDisplay(string textToDisplay)
        {
            string result = textToDisplay;
            bool decimalPresent = false;            
            int decimalPosition = 0, leftOfDecimalLength = 0, rightOfDecimalLength = 0;

            //Get Decimal Position, if any
            if (textToDisplay.Contains('.')){
                decimalPresent = true;
                decimalPosition = textToDisplay.IndexOf('.');
                leftOfDecimalLength = decimalPosition - 1;
                rightOfDecimalLength = textToDisplay.Length - decimalPosition - 1;
            }
           
            //limit to 20 places before or after decimal....
            
            //Develop display rules and check for violation....
            if(leftOfDecimalLength > characterLimit || rightOfDecimalLength > characterLimit)
            {
                result = "num too big";
            }
            //evaluate string.  If length > 9 Use Exponential Notation 5.1234E4
            else if (textToDisplay.Length > displayLimit && decimalPresent)
            {
                string e="error";

                if (decimalPosition > 0 && decimalPosition < 7)
                {
                    e = (decimalPosition - 1).ToString();
                }
                else if(decimalPosition == 0)
                {
                    //where is the first non zero number?
                    int c = 0;
                    do
                    {
                        c += 1;
                    } while (textToDisplay[c] !='0');
                    e = c.ToString();                    
                }

                result = e;
                
            }
            else if (textToDisplay.Length > displayLimit && !decimalPresent)
            {
                string Result;
                const int eMaxDisplayLength = 5;
                int displaySize = displayLimit > eMaxDisplayLength ? eMaxDisplayLength : displayLimit;

                //Get the first 6 characters of string and convert to int
                if (textToDisplay.Length > displaySize + 1)
                {
                    Result = textToDisplay.Remove(displaySize + 1);
                }
                else
                {
                    Result = textToDisplay;
                }
                //assume all positive...
                double g = Convert.ToDouble(Result);
                g = g/Math.Pow(10,displaySize); //(displayLimit > eDisplayLength ? 5 : displayLimit)
                result = g.ToString() + "e" + (textToDisplay.Length-1).ToString();
            }

            

            txtDisplay.Text = result;
        } 

        private void ClearDisplay()
        {
            UpdateDisplayString('0');
        }


        //Capture Key Press to allow 10 key entry.  Much preferred.
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Calc.CharacterAllowed(e.KeyChar))
            {
                displayDirty = true;
                UpdateDisplayString(e.KeyChar);
            }
            else if (Calc.OperatorAllowed(e.KeyChar))
            {
                EvaluateOperatorKeyPress(e.KeyChar);
            }

        }

    }
}
