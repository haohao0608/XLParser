using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLParser
{
    // The different operators
    public enum Operator
    {
        Plus,
        Min,
        Mult,
        Div,
        Exp,
        Range,
        Intersect,
        Union,
        Concat,
        Percent,
        Eq,
        Neq,
        Lt,
        Gt,
        Lte,
        Gte,
    }

    /// <summary>
    /// Extension methods for Operator enum
    /// </summary>
    public static class Operators
    {
        public static Operator from(string op)
        {
            switch (op)
            {
                case "+":
                    return Operator.Plus;
                case "-":
                    return Operator.Min;
                case "*":
                    return Operator.Mult;
                case "/":
                    return Operator.Div;
                case "^":
                    return Operator.Exp;
                case "==":
                    return Operator.Eq;
                case "<>":
                    return Operator.Neq;
                case "<":
                    return Operator.Lt;
                case ">":
                    return Operator.Gt;
                case ">=":
                    return Operator.Gte;
                case "<=":
                    return Operator.Lte;
                case "&":
                    return Operator.Concat;
                case "%":
                    return Operator.Percent;
                case " ":
                case GrammarNames.TokenIntersect:
                    return Operator.Intersect;
                case GrammarNames.TokenUnionOperator:
                    return Operator.Union;
                case ":":
                    return Operator.Range;
                default:
                    throw new ArgumentException($"Not an operator <<{op}>>", nameof(op));
            }
        }

        public static string Symbol(this Operator op)
        {
            switch (op)
            {
                case Operator.Plus:
                    return "+";
                case Operator.Min:
                    return "-";
                case Operator.Mult:
                    return "*";
                case Operator.Div:
                    return "/";
                case Operator.Exp:
                    return "^";
                case Operator.Range:
                    return ":";
                case Operator.Intersect:
                    return GrammarNames.TokenIntersect;
                case Operator.Union:
                    return GrammarNames.TokenUnionOperator;
                case Operator.Concat:
                    return "&";
                case Operator.Percent:
                    return "%";
                case Operator.Eq:
                    return "==";
                case Operator.Neq:
                    return "<>";
                case Operator.Lt:
                    return "<";
                case Operator.Gt:
                    return ">";
                case Operator.Lte:
                    return "<=";
                case Operator.Gte:
                    return ">=";
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, "Unkown operator");
            }
        }

        // Source: https://support.office.com/en-us/article/Calculation-operators-and-precedence-48be406d-4975-4d31-b2b8-7af9e0e2878a
        // Could also be an enum, but this way you don't need int casts
        public static class Precedences
        {
            // Don't use priority 0, Irony seems to view it as no priority set
            public const int Comparison = 1;
            public const int Concatenation = 2;
            public const int Addition = 3;
            public const int Multiplication = 4;
            public const int Exponentiation = 5;
            public const int UnaryPostFix = 6;
            public const int UnaryPreFix = 7;
            //public const int Reference = 8;
            public const int Union = 9;
            public const int Intersection = 10;
            public const int Range = 11;
        }

        public static int Precedence(this Operator op)
        {
            switch (op)
            {
                case Operator.Eq:
                case Operator.Neq:
                case Operator.Lt:
                case Operator.Gt:
                case Operator.Lte:
                case Operator.Gte:
                    return Precedences.Comparison;
                case Operator.Concat:
                    return Precedences.Concatenation;
                case Operator.Plus:
                case Operator.Min:
                    return Precedences.Addition;
                case Operator.Mult:
                case Operator.Div:
                    return Precedences.Multiplication;
                case Operator.Exp:
                    return Precedences.Exponentiation;
                case Operator.Percent:
                    return Precedences.UnaryPostFix;
                case Operator.Union:
                    return Precedences.Union;
                case Operator.Intersect:
                    return Precedences.Intersection;
                case Operator.Range:
                    return Precedences.Range;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }
    }
}
