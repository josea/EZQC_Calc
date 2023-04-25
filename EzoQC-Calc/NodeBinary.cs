using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_Calc
{

    /// <summary>
    /// Node that has a binary operator, like +, -, *, /, ^
    /// </summary>
    internal class NodeBinary : Node
    {
        private Node Left, Right;
        private char Op;
        private string RefExpression;
        public NodeBinary(Node left, Node right, char op, string refExpression)
        {
            Left = left;
            Right = right;
            Op = op;
            RefExpression = refExpression;
        }

        public override double Evaluate()
        {
            switch (Op)
            {
                case '+':
                    return Left.Evaluate() + Right.Evaluate();
                case '-':
                    return Left.Evaluate() - Right.Evaluate();
                case '*':
                    return Left.Evaluate() * Right.Evaluate();
                case '/':
                    var y = Right.Evaluate();
                    if (y == 0.0)
                    {
                        throw new DivideByZeroException($"Division by Zero Exception in expression node: {RefExpression}");
                    }
                    return Left.Evaluate() / y;
                case '^':
                    return Math.Pow(Left.Evaluate(), Right.Evaluate());
            }
            return 0;
        }
    }
}
