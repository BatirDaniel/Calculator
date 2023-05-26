using System.Data;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;

namespace Calculator.Models
{
    public class CalculatorViewModel
    {
        public string? Expression { get; set; }

        public double CalculateExpression()
        {
            if (string.IsNullOrEmpty(Expression))
            {
                throw new ArgumentException("The expression is null or empty.");
            }

            if (Expression.Contains("/0"))
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            var stack = new Stack<double>();
            var operators = new Stack<char>();

            int i = 0;
            while (i < Expression.Length)
            {
                char ch = Expression[i];

                if (char.IsDigit(ch))
                {
                    int start = i;
                    while (i < Expression.Length && (char.IsDigit(Expression[i]) || Expression[i] == '.'))
                    {
                        i++;
                    }
                    string numberStr = Expression.Substring(start, i - start);
                    double number = double.Parse(numberStr);

                    stack.Push(number);
                }
                else if (ch == '(')
                {
                    operators.Push(ch);
                    i++;
                }
                else if (ch == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        EvaluateTop(stack, operators);
                    }

                    if (operators.Count == 0 || operators.Peek() != '(')
                    {
                        throw new InvalidOperationException("Invalid expression: Mismatched parentheses.");
                    }

                    operators.Pop();
                    i++;
                }
                else if (IsOperator(ch))
                {
                    while (operators.Count > 0 && Precedence(ch) <= Precedence(operators.Peek()))
                    {
                        EvaluateTop(stack, operators);
                    }
                    operators.Push(ch);
                    i++;
                }
                else if (char.IsWhiteSpace(ch))
                {
                    i++;
                }
                else
                {
                    throw new InvalidOperationException($"Invalid character: {ch}");
                }
            }

            while (operators.Count > 0)
            {
                EvaluateTop(stack, operators);
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid expression: Too many operands.");
            }

            return stack.Pop();
        }

        private void EvaluateTop(Stack<double> stack, Stack<char> operators)
        {
            if (stack.Count < 2)
            {
                throw new InvalidOperationException("Invalid expression: Not enough operands.");
            }

            double operand2 = stack.Pop();
            double operand1 = stack.Pop();
            char op = operators.Pop();

            double result;
            switch (op)
            {
                case '+':
                    result = operand1 + operand2;
                    break;
                case '-':
                    result = operand1 - operand2;
                    break;
                case '*':
                    result = operand1 * operand2;
                    break;
                case '/':
                    result = operand1 / operand2;
                    break;
                default:
                    throw new InvalidOperationException($"Invalid operator: {op}");
            }

            stack.Push(result);
        }

        private bool IsOperator(char token)
        {
            return token == '+' || token == '-' || token == '*' || token == '/';
        }

        private int Precedence(char op)
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
                    throw new InvalidOperationException($"Invalid operator: {op}");
            }
        }

    }
}

