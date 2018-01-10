using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator_1
{
    public partial class Form1 : Form
    {
        CalculatorOps Calc;
        StringBuilder sb1;

        public Form1()
        {
            sb1 = new StringBuilder("0");
            InitializeComponent();
        }

        private void WriteToDisplay(string textToDisplay)
        {
            txtDisplay.Text = textToDisplay;
        }

        private void ClearDisplay()
        {            
            UpdateDisplayString('0');
        }

        private void UpdateDisplayString(char key)
        {
            if (!Calc.CharacterAllowed(key))
            {
                MessageBox.Show("Invalid Input. You entered {0}.", key.ToString());
            }     
            
            if(sb1.Length>0 && sb1[0] == '0')
            {
                sb1.Remove(0, 1);
            }
            
            //Add number to string.            
            sb1.Append(key);

            WriteToDisplay(sb1.ToString());         
            
        }
        
        private void EvaluateOperatorKeyPress(char c) //operation key (+,*,-,/,=)  pressed.  
        {
            string result;
            double valid;

            //Check to see if there is new input in textbox...
            if (sb1.Length == 0)
            {
                sb1.Append(txtDisplay.Text);
            }            
            
            //Validate input and write to double.
            if (!double.TryParse(sb1.ToString(), out valid))
            {
                MessageBox.Show("You must enter a number");
            }

            result = Calc.EvaluateOperator(c, valid);
            
            //Write the total to the display.
            WriteToDisplay(result);

            //Clear String Builder to accept new input....
            sb1.Clear();          
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            //Apend SB and write to textbox;
            UpdateDisplayString('7');
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            sb1.Clear();
            Calc.ResetCalcatorOps();
            ClearDisplay();
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('8');
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('9');
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('4');
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('5');
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('6');
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('1');
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('2');
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('3');
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('0');
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            int j = 0;
            foreach (char c in sb1.ToString())
            {
                if (c == '.')
                {
                    j += 1;
                }
            }

            if (j > 1)
            {
                MessageBox.Show("Only one decimal allowed");
            }
            else
            {
                UpdateDisplayString('.');
            }
            
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            EvaluateOperatorKeyPress('=');
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            EvaluateOperatorKeyPress('+');
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            EvaluateOperatorKeyPress('-');
        }

        private void btnAsterisk_Click(object sender, EventArgs e)
        {
            EvaluateOperatorKeyPress('*');
        }

        private void btnSlash_Click(object sender, EventArgs e)
        {
            EvaluateOperatorKeyPress('/');
        }

        private void btnClear_MouseHover(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Calc = new CalculatorOps();
            sb1 = new StringBuilder("0");
        }

        private void txtDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //todo: add key press details
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Calc.CharacterAllowed(e.KeyChar))
            {
                UpdateDisplayString(e.KeyChar);
            }
            else if(Calc.OperatorAllowed(e.KeyChar))
            {
                EvaluateOperatorKeyPress(e.KeyChar);
            }
            
        }
    }
}
