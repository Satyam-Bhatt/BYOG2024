using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class CustomExpressionEvaluator
{
    // Dictionary of supported mathematical functions
    private static readonly Dictionary<string, Func<double, double>> Functions = new Dictionary<string, Func<double, double>>
    {
        {"sin", Math.Sin},
        {"cos", Math.Cos},
        {"tan", Math.Tan},
        {"sqrt", Math.Sqrt},
        {"abs", Math.Abs}
    };

    // Main method to evaluate an expression
    public float EvaluateExpression(string expression, float x)
    {
        try
        {
            // Replace 'x' in the expression with its numeric value
            expression = expression.Replace("x", x.ToString());
            // HIGHLIGHT START
            // Replace 'Pi' or 'pi' with its numeric value
            expression = expression.Replace("Pi", Math.PI.ToString());
            // HIGHLIGHT END
            // Convert the expression into tokens
            var tokens = Tokenize(expression);
            // Convert infix notation to postfix notation
            var postfix = ShuntingYard(tokens);
            // Evaluate the postfix expression and return the result as a float
            return (float)EvaluatePostfix(postfix);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error evaluating expression: {e.Message}");
            return float.NaN; // Return NaN to indicate an error
        }
    }

    // Convert the expression string into a list of tokens
    private static List<string> Tokenize(string expression)
    {
        // HIGHLIGHT START
        // Add support for multi-character tokens (like Pi)
        return new string(expression
                .Replace("(", " ( ")
                .Replace(")", " ) ")
                .Replace("+", " + ")
                .Replace("-", " - ")
                .Replace("*", " * ")
                .Replace("/", " / ")
                .Replace("^", " ^ ")
                .Replace("%", " % "))
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        // HIGHLIGHT END
    }

    // Implement the Shunting Yard algorithm to convert infix to postfix notation
    private static List<string> ShuntingYard(List<string> tokens)
    {
        var output = new List<string>();
        var operators = new Stack<string>();

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                // If the token is a number, add it to the output
                output.Add(token);
            }
            else if (Functions.ContainsKey(token))
            {
                // If the token is a function, push it onto the operator stack
                operators.Push(token);
            }
            else if (token == "(")
            {
                // If the token is a left parenthesis, push it onto the operator stack
                operators.Push(token);
            }
            else if (token == ")")
            {
                // If the token is a right parenthesis, pop operators to output until we find a left parenthesis
                bool foundLeftParenthesis = false;
                while (operators.Count > 0)
                {
                    string op = operators.Pop();
                    if (op == "(")
                    {
                        foundLeftParenthesis = true;
                        break;
                    }
                    output.Add(op);
                }
                if (!foundLeftParenthesis)
                {
                    throw new ArgumentException("Mismatched parentheses");
                }
                if (operators.Count > 0 && Functions.ContainsKey(operators.Peek()))
                {
                    // If there's a function at the top of the operator stack, add it to the output
                    output.Add(operators.Pop());
                }
            }
            else if (IsOperator(token))
            {
                // Pop operators with higher or equal precedence to the output
                while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token))
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
            else
            {
                throw new ArgumentException($"Unknown token: {token}");
            }
        }

        // Pop any remaining operators to the output
        while (operators.Count > 0)
        {
            if (operators.Peek() == "(")
            {
                throw new ArgumentException("Mismatched parentheses");
            }
            output.Add(operators.Pop());
        }

        return output;
    }

    // Evaluate the postfix expression
    private static double EvaluatePostfix(List<string> postfix)
    {
        var stack = new Stack<double>();

        foreach (var token in postfix)
        {
            if (double.TryParse(token, out var number))
            {
                // If the token is a number, push it onto the stack
                stack.Push(number);
            }
            else if (Functions.ContainsKey(token))
            {
                // If the token is a function, apply it to the top value on the stack
                if (stack.Count < 1)
                {
                    throw new ArgumentException($"Not enough operands for function: {token}");
                }
                stack.Push(Functions[token](stack.Pop()));
            }
            else if (IsOperator(token))
            {
                // If the token is an operator, apply it to the top two values on the stack
                if (stack.Count < 2)
                {
                    throw new ArgumentException($"Not enough operands for operator: {token}");
                }
                var b = stack.Pop();
                var a = stack.Pop();
                stack.Push(ApplyOperator(token, a, b));
            }
            else
            {
                throw new ArgumentException($"Unknown token in postfix evaluation: {token}");
            }
        }

        // The final value on the stack is the result
        if (stack.Count != 1)
        {
            throw new ArgumentException("Invalid expression: too many operands");
        }

        return stack.Pop();
    }

    // Check if a token is an operator
    private static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^" || token == "%";
    }

    // Determine the precedence of operators
    private static int Precedence(string op)
    {
        return op switch
        {
            "+" or "-" => 1,
            "*" or "/" or "%" => 2,
            "^" => 3,
            _ => 0,
        };
    }

    // Apply the specified operator to two operands
    private static double ApplyOperator(string op, double a, double b)
    {
        return op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => b == 0 ? throw new DivideByZeroException("Division by zero") : a / b,
            "^" => Math.Pow(a, b),
            "%" => b == 0 ? throw new DivideByZeroException("Modulo by zero") : a % b,
            _ => throw new ArgumentException($"Unknown operator: {op}"),
        };
    }
}