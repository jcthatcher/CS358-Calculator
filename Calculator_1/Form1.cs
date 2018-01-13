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
        CalculatorOperations CalcOps;
        DisplayOperations DisplayOps;
        StringBuilder displayText;

        public Form1()
        {
            CalcOps = new CalculatorOperations();
            DisplayOps = new DisplayOperations(19, 9);
            displayText = new StringBuilder("0");
            ops = '=';
            displayDirty = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EvaluateOperatorKeyPress(char c) //operation key (+,*,-,/,=)  pressed.  
        {
            string[] result;
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
                    result = CalcOps.EvaluateOperator(ops, valid);

                    //Write result to the display.
                    if(result[0] == "-1")
                    {
                        WriteToDisplay(result[1], false);
                    }
                    else
                    {
                        WriteToDisplay(result[1], true);
                    }
                    
                }  
            }
            
            //Record new ops to be performed according to key press.
            ops = c;            
        }

        private void EvaluateCharacterKeyPress(char key)
        {
            //Validate character...
            if (!CalcOps.CharacterAllowed(key))
            {
                MessageBox.Show("Invalid Input. You entered: " + key.ToString());
            }

            //Remove leading zeros...
            if ((displayText.Length > 0) && (displayText[0] == '0'))
            {
                displayText.Remove(0, 1);
            }

            //Add character to stringbuilder...
            displayText.Append(key);

            //Update display with sb value.
            WriteToDisplay(displayText.ToString(), true);
        }

        private void WriteToDisplay(string textToDisplay, bool number)
        {
            string result = DisplayOps.GetFormattedDisplayText(textToDisplay, number);
            txtDisplay.Text = result;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            displayText.Clear();
            ops = '=';
            CalcOps.ResetCalcatorOps();
            ClearDisplay();
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
      
        private void ClearDisplay()
        {
            EvaluateCharacterKeyPress('0');
        }


        //Capture Key Press to allow 10 key entry.  Much preferred.
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CalcOps.CharacterAllowed(e.KeyChar))
            {
                displayDirty = true;
                EvaluateCharacterKeyPress(e.KeyChar);
            }
            else if (CalcOps.OperatorAllowed(e.KeyChar))
            {
                EvaluateOperatorKeyPress(e.KeyChar);
            }

        }

    }
}
