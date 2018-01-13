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
        DisplayOperations Disp;
        StringBuilder displayText;

        public Form1()
        {
            ops = '=';
            displayDirty = false;
            displayText = new StringBuilder("0");
            InitializeComponent();
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

        private void EvaluateCharacterKeyPress(char key)
        {
            //Validate character...
            if (!Calc.CharacterAllowed(key))
            {
                MessageBox.Show("Invalid Input. You entered: " + key.ToString());
            }

            //Remove leading zeros...
            if (displayText.Length > 0 && displayText[0] == '0')
            {
                displayText.Remove(0, 1);
            }

            //Add character to stringbuilder...
            displayText.Append(key);

            //Update display with sb value.
            WriteToDisplay(displayText.ToString());

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
            EvaluateCharacterKeyPress(Convert.ToChar(btnWorking.Text));
        }

        private void ButtonClickOperator(object sender, EventArgs e)
        {
            Button btnWorking = (Button)sender;
            EvaluateOperatorKeyPress(Convert.ToChar(btnWorking.Text));
        }

        private void WriteToDisplay(string textToDisplay)
        {
            txtDisplay.Text = textToDisplay;
        } 

        private void ClearDisplay()
        {
            EvaluateCharacterKeyPress('0');
        }


        //Capture Key Press to allow 10 key entry.  Much preferred.
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Calc.CharacterAllowed(e.KeyChar))
            {
                displayDirty = true;
                EvaluateCharacterKeyPress(e.KeyChar);
            }
            else if (Calc.OperatorAllowed(e.KeyChar))
            {
                EvaluateOperatorKeyPress(e.KeyChar);
            }

        }

    }
}
