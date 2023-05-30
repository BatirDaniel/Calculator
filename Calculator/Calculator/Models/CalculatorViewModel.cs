using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.Models
{
    public class CalculatorViewModel
    {

        public string Expression { get; set; }

        public double CalculateExpression()
        {
            Stack<double> operandStack = new Stack<double>();
            Stack<char> operatorStack = new Stack<char>();

            for (int i = 0; i < Expression.Length; i++)
            {
                char ch = Expression[i];

                if (char.IsDigit(ch))
                {
                    string operandStr = "";
                    while (i < Expression.Length && (char.IsDigit(Expression[i]) || Expression[i] == '.'))
                    {
                        operandStr += Expression[i];
                        i++;
                    }

                    double operand = double.Parse(operandStr);
                    operandStack.Push(operand);
                    i--;
                }
                else if (ch == '(')
                {
                    operatorStack.Push(ch);
                }
                else if (ch == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        double result = PerformOperation(operatorStack.Pop(), operandStack.Pop(), operandStack.Pop());
                        operandStack.Push(result);
                    }

                    if (operatorStack.Count > 0 && operatorStack.Peek() == '(')
                    {
                        operatorStack.Pop(); // Remove the '('
                    }
                }
                else if (IsOperator(ch))
                {
                    if (ch == '-' && (i == 0 || Expression[i - 1] == '('))
                    {
                        // Handle unary minus
                        operandStack.Push(0); // Push a placeholder operand
                        operatorStack.Push(ch);
                    }
                    else
                    {
                        while (operatorStack.Count > 0 && OperatorPrecedence(ch) <= OperatorPrecedence(operatorStack.Peek()))
                        {
                            double result = PerformOperation(operatorStack.Pop(), operandStack.Pop(), operandStack.Pop());
                            operandStack.Push(result);
                        }

                        operatorStack.Push(ch);
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                double result = PerformOperation(operatorStack.Pop(), operandStack.Pop(), operandStack.Pop());
                operandStack.Push(result);
            }

            return operandStack.Pop();
        }

        private double PerformOperation(char op, double b, double a)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                        throw new DivideByZeroException("Cannot divide by zero.");
                    return a / b;
                default:
                    throw new ArgumentException("Invalid operator: " + op);
            }
        }

        private bool IsOperator(char ch)
        {
            return ch == '+' || ch == '-' || ch == '*' || ch == '/';
        }

        private int OperatorPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }
    }
}