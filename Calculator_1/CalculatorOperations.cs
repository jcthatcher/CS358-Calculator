using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_1
{
    class CalculatorOperations
    {
        double total;
        char[] allowedChar;
        char[] allowedOps;

        public CalculatorOperations()
        {
            total = 0.0d;
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
        public double AddTwoNumbers(double a, double b)
        {
            return a + b;
        }

        public double SubtractTwoNumbers(double a, double b)
        {
            return a - b;
        }

        public double MultiplyTwoNumbers(double a, double b)
        {
            return a * b;
        }

        public double DivideTwoNumbers(double a, double b)
        {
            return a / b;
        }

        public void ResetCalcatorOps()
        {
            total = 0.00d;
        }

        public string[] EvaluateOperator(char ops, double number)
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

            string[] result = { msg, msg=="-1" ? error : total.ToString()};
            return result;
        }

        private double PerformCalculation(double number, char ops)
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
                        return 0.00d;
                    }
            }
        }
    }
}
