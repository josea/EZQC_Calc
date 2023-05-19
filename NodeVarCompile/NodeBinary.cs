using System.Diagnostics;

namespace NodeVarCompile;

/// <summary>
/// Node that has a binary operator, like +, -, *, /, ^
/// </summary>
internal class NodeBinary : NodeCompilable
{
    private readonly Func<Func<string, double>, double>  _left, _right;
    private readonly char _op;
    private readonly string _refExpression;
    public NodeBinary(Func<Func<string,double>,double> Left, 
        Func<Func<string, double>, double> Right, char Op, string RefExpression)
    {
        _left = Left;
        _right = Right;
        _op = Op;
        _refExpression = RefExpression;
    }

    public Func<Func<string, double>, double> Compile()
    {
        switch (_op)
        {
            case '+':
                return (f) => 
                _left(f) + _right(f);
            case '-':
                return (f) => 
                _left(f) - _right(f);
            case '*':
                return (f) => 
                _left(f) * _right(f);
            case '/':
                return (f) =>
                {
                    var y = _right(f);
                    if (y == 0.0)
                    {
                        throw new DivideByZeroException($"Division by Zero Exception in expression node: {_refExpression}");
                    }
                    return _left(f) / y;
                };
            case '^':
                return (f) => 
                Math.Pow(_left(f), _right(f));
        }
        throw new Exception($"Invalid operator: {_op}");
    }
}
