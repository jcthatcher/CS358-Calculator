using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_1
{
    class CalculatorOps
    {
        double total;
        char[] allowedChar;
        char[] allowedOps;
        char ops;

        public CalculatorOps()
        {
            total = 0.0d;
            allowedChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            allowedOps = new char[] { '+', '-', '*', '/', '=' };
            ops = '=';
        }
        
        public bool OperatorAllowed(char operatorToEvaluate)
        {
            return allowedOps.Contains(operatorToEvaluate);
        }
        
        public bool CharacterAllowed(char characterToEvaluate)
        {
            return allowedChar.Contains(characterToEvaluate);
        }

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
            ops = '=';
        }

        public string EvaluateOperator(char c, double number)
        {
            //Are there operations to be performed on this number? Is ops <> '=';
            if (ops != '=')
            {
                total = PerformCalculation(number); //Perform calculations.
            }
            else
            {
                total = number;                
            }

            ops = c;
            return total.ToString();
        }

        private double PerformCalculation(double number)
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
