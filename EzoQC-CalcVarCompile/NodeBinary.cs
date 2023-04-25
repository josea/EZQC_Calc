using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_CalcVarCompile
{

    /// <summary>
    /// Node that has a binary operator, like +, -, *, /, ^
    /// </summary>
    internal class NodeBinary : Node
    {
        private Func<Func<string, double>, double> Left, Right;
        private char Op;
        private string RefExpression;
        public NodeBinary(Func<Func<string,double>,double> left, 
            Func<Func<string, double>, double> right, char op, string refExpression)
        {
            Left = left;
            Right = right;
            Op = op;
            RefExpression = refExpression;
        }

        public override Func<Func<string, double>, double> Compile()
        {
            Debug.WriteLine("Expression Parser");
            switch (Op)
            {
                case '+':
                    return (f) => 
                    Left(f) + Right(f);
                case '-':
                    return (f) => 
                    Left(f) - Right(f);
                case '*':
                    return (f) => 
                    Left(f) * Right(f);
                case '/':
                    return (f) =>
                    {
                        var y = Right(f);
                        if (y == 0.0)
                        {
                            throw new DivideByZeroException($"Division by Zero Exception in expression node: {RefExpression}");
                        }
                        return Left(f) / y;
                    };
                case '^':
                    return (f) => 
                    Math.Pow(Left(f), Right(f));
            }
            return (f) => 0;
        }
    }
}
