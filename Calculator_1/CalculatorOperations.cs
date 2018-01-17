using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_1
{
    class CalculatorOperations
    {
        decimal total;
        char[] allowedChar;
        char[] allowedOps;

        public CalculatorOperations()
        {
            total = 0.0m;
            allowedChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            allowedOps = new char[] { '+', '-', '*', '/', '=', (char)13 };
        }
        
        public bool OperatorAllowed(char operatorToEvaluate)
        {
            return allowedOps.Contains(operatorToEvaluate);
        }
        
        public bool CharacterAllowed(char characterToEvaluate)
        {
            return allowedChar.Contains(characterToEvaluate);
        }

        //overkill here but...
        public decimal AddTwoNumbers(decimal a, decimal b)
        {
            return a + b;
        }

        public decimal SubtractTwoNumbers(decimal a, decimal b)
        {
            return a - b;
        }

        public decimal MultiplyTwoNumbers(decimal a, decimal b)
        {
            return a * b;
        }

        public decimal DivideTwoNumbers(decimal a, decimal b)
        {
            return a / b;
        }

        public void ResetCalcatorOps()
        {
            total = 0.00m;
        }

        public string[] EvaluateOperator(char ops, decimal number)
        {
            string msg, error = " ";
            if(ops == '/' && number == 0)
            {
                msg = "-1";
                error = "DIV BY ZERO";
            }

            //Are there operations to be performed on this number? Is ops <> '=';
            else if (ops != '=' && ops != (char)13)
            {
                msg = "0";
                total = PerformCalculation(number, ops); //Perform calculations.
            }
            else
            {
                msg = "0";
                total = number;                
            }

            string totalToString = total.ToString();

            if (totalToString.Contains('.') && totalToString[totalToString.Length-1] == '0')
            {
                while (totalToString[(totalToString.Length - 1)] == '0')
                {
                    string temp2 = " ";
                    temp2 = totalToString.Remove(totalToString.Length - 1);
                    totalToString = temp2;
                }

                if (totalToString[(totalToString.Length - 1)] == '.')
                {
                    string temp2 = " ";
                    temp2 = totalToString.Remove(totalToString.Length - 1);
                    totalToString = temp2;
                }
            }

            string[] result = { msg, msg=="-1" ? error : totalToString};
            return result;
        }

        private decimal PerformCalculation(decimal number, char ops)
        {
            switch (ops)
            {
                case '+':
                    {
                        return AddTwoNumbers(total, number);
                    }
                case '-':
                    {
                        return SubtractTwoNumbers(total, number);                        
                    }
                case '*':
                    {
                        return MultiplyTwoNumbers(total, number);                        
                    }
                case '/':
                    {
                        return DivideTwoNumbers(total, number);
                    }
                default:
                    {
                        return 0.00m;
                    }
            }
        }
    }
}
