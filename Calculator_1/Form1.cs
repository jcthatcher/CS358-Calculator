using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_1
{
    public partial class Form1 : Form
    {
        double total;
        double workingNumber;
        char[] allowedChar;
        char[] allowedOps;
        char ops;
        StringBuilder sb;

        public Form1()
        {
            total = 0.0d;
            workingNumber = 0.0d;
            allowedChar = new char[]{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            allowedOps = new char[] { '+', '-', '*', '/', '=' };
            InitializeComponent();
        }

        private void WriteToDisplay(string s)
        {
            txtDisplay.Text = s;
        }

        private void UpdateDisplayString(char key)
        {

            if (!allowedChar.Contains(key))
            {
                MessageBox.Show("Invalid Input. You entered {0}.",key.ToString());
            }

            while (sb.Length>0 && sb[0] == '0')
            {
                sb.Remove(0, 1);
            }
            
            WriteToDisplay(sb.Append(key).ToString());    
            
        }

        private void ClearDisplay()
        {
            UpdateDisplayString('0');
        }

        private void PerformCalculation(char c)
        {
            if (!double.TryParse(sb.ToString(), out workingNumber) || c!='.')
            {
                MessageBox.Show("Numbers Only, Check Your Display.");
            }
            
            switch (c)
            {
                case '+':
                    {
                        total += workingNumber;

                        break;
                    }
                case '-':
                    {
                        total -= workingNumber;
                        break;
                    }
                case '*':
                    {
                        total *= workingNumber;
                        break;
                    }
                case '/':
                    {
                        total /= workingNumber;
                        break;
                    }
                    
                default:
                    {
                        break;
                    }
            }
            
            sb.Clear();
            WriteToDisplay(total.ToString());
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            UpdateDisplayString('7');
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            sb.Clear();
            WriteToDisplay("0");
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
            if(txtDisplay.Text.Contains('.'))
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
            PerformCalculation(ops);
            sb.Append(total);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            ops = '+';
            PerformCalculation('+');
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            ops = '-';
            PerformCalculation('-');
        }

        private void btnAsterisk_Click(object sender, EventArgs e)
        {
            ops = '*';
            PerformCalculation('*');
        }

        private void btnSlash_Click(object sender, EventArgs e)
        {
            ops = '/';
            PerformCalculation('/');
        }

        private void btnClear_MouseHover(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip tt1 = new ToolTip();
            tt1.AutoPopDelay = 3000;
            tt1.InitialDelay = 100;
            tt1.ReshowDelay = 5000;

            tt1.SetToolTip(this.btnClear, "Press ONCE to clear screen, TWICE to clear memory");

            sb = new StringBuilder("0");
        }

        private void txtDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //todo: add key press details
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowedChar.Contains(e.KeyChar))
            {
                UpdateDisplayString(e.KeyChar);
            }
            else if(allowedOps.Contains(e.KeyChar))
            {
                PerformCalculation(e.KeyChar);
            }
            
        }
    }
}
