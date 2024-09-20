using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class CustomExpressionEvaluator : MonoBehaviour
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
        // Replace 'x' in the expression with its numeric value
        expression = expression.Replace("x", x.ToString());
        // Convert the expression into tokens
        var tokens = Tokenize(expression);
        // Convert infix notation to postfix notation
        var postfix = ShuntingYard(tokens);
        // Evaluate the postfix expression and return the result as a float
        return (float)EvaluatePostfix(postfix);
    }

    // Convert the expression string into a list of tokens
    private static List<string> Tokenize(string expression)
    {
        return new string(expression.Where(c => !char.IsWhiteSpace(c)).ToArray())
            .Replace("(", " ( ")
            .Replace(")", " ) ")
            .Replace("+", " + ")
            .Replace("-", " - ")
            .Replace("*", " * ")
            .Replace("/", " / ")
            .Replace("^", " ^ ")
            .Replace("%", " % ") // Add support for modulo operator
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
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
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }
                operators.Pop(); // Remove the left parenthesis
                if (operators.Count > 0 && Functions.ContainsKey(operators.Peek()))
                {
                    // If there's a function at the top of the operator stack, add it to the output
                    output.Add(operators.Pop());
                }
            }
            else // The token is an operator
            {
                // Pop operators with higher or equal precedence to the output
                while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token))
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
        }

        // Pop any remaining operators to the output
        while (operators.Count > 0)
        {
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
                stack.Push(Functions[token](stack.Pop()));
            }
            else // The token is an operator
            {
                // Pop two values, apply the operator, and push the result back
                var b = stack.Pop();
                var a = stack.Pop();
                stack.Push(ApplyOperator(token, a, b));
            }
        }

        // The final value on the stack is the result
        return stack.Pop();
    }

    // Determine the precedence of operators
    private static int Precedence(string op)
    {
        return op switch
        {
            "+" or "-" => 1,
            "*" or "/" or "%" => 2, // Add modulo with same precedence as multiplication
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
            "/" => a / b,
            "^" => Math.Pow(a, b),
            "%" => a % b, // Add support for modulo operation
            _ => throw new ArgumentException("Unknown operator"),
        };
    }
}