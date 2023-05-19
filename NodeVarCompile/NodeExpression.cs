using System.Diagnostics;

namespace NodeVarCompile;

/// <summary>
/// Node that is based on a complex mathematical expression (string).
/// </summary>
public class NodeExpression : NodeCompilable
{
    private readonly string _expr;

    public NodeExpression(string expr)
    {
        _expr = RemoveEnclosingElipsis(expr.Trim(' ').ToLower()); // just trim garbage spaces and normalize to lowercase.            
    }


    /// <summary>
    /// remove the enclosing elipsis, if they are enclosing the whole expression.
    /// recursive to remove multiple elipsis,eg: (((2 + 3 )))
    /// Do NOT remove in cases like (2+3) - (1-5)
    /// </summary>
    private string RemoveEnclosingElipsis(string eExpr)
    {
        if (eExpr.StartsWith('(') && eExpr.EndsWith(')'))
        {
            int countElipsis = 1;
            bool remove = true;
            for (int j = 1; j < eExpr.Length - 1; j++)
            {
                if (eExpr[j] == '(') countElipsis++;
                if (eExpr[j] == ')') countElipsis--;
                if (countElipsis == 0)
                { // this happens when the elipsis close in the middle, like (2 + 4) * (3+1)
                    remove = false; // do not remove in that case.
                    break;
                }
            }
            if (remove)
            {   // if it found enclosing elipsis, remove them and test to see if there is another level, this optional:
                // we might decide that (( 2)) is not a valid expression, in which case recursion is not needed
                eExpr = eExpr.Substring(1, eExpr.Length - 2);
                eExpr = RemoveEnclosingElipsis(eExpr);
            }
        }
        return eExpr;
    }

    /// <summary>
    /// Splits the incoming expressions left to right, by Op.
    /// It returns the left/right expressions, ie: 
    /// {exprleft} Op {exprRight}
    /// eg: (2 + 3) * (5 + 2) => exprLeft = (2 + 3) , exprRight = (5 + 2)
    /// </summary>        
    private bool SplitByOp(string Expr, char Op, out string ExprLeft, out string ExprRight)
    {
        int elipsisCount = 0;
        for (int i = 0; i < Expr.Length; i++)
        {
            if (Expr[i] == '(') elipsisCount++; // counts whether we are inside a ( ) section
            if (Expr[i] == ')') elipsisCount--;
            if (elipsisCount == 0 && Expr[i] == Op && i > 0) // can only split outside ( ), ie: at the top level
            {
                ExprLeft = Expr.Substring(0, i);
                ExprRight = Expr.Substring(i + 1);
                return true;
            }
        }
        ExprLeft = ExprRight = "";
        return false;
    }

    public Func<Func<string, double>, double> Compile()
    {
        double exprValue;
        Func<Func<string, double>, double> compiledFunc;

        // is expression a number? => return it
        if (double.TryParse(_expr, out exprValue))
        {
            return new NodeValue(exprValue).Compile();
        }
        // try to split the expression by operator
        else if ((compiledFunc = TryToSplitByBinaryOp()) != null)
        {            
            return compiledFunc;
        }
        else if (_expr[0] == '-') // if it is a unitary negate operator? 
        {
            return new NodeNegate(new NodeExpression(_expr.Substring(1)).Compile()).Compile();
        }
        // it couldn't split => is it a call to a function? 
        else if ((compiledFunc = TryFunctionCall()) != null)
        {
            return compiledFunc;
        }
        else if ((compiledFunc = TryVariable()) != null)
        {
            return compiledFunc;
        }
        throw new Exception($"Unparseable: {_expr}");
    }

    private Func<Func<string, double>, double> TryVariable()
    {
        if (_expr == "x" || _expr == "y")
        { // concept variable handling
            return (f) => f(_expr);  // it just calls the function passed by the end user. 
        }
        return null!;
    }

    private Func<Func<string, double>, double> TryFunctionCall()
    {
        // depending on how many functions we support, this could be a regex to detect anything like
        // funct_name ( x ) 
        if (_expr.StartsWith("sqrt"))
        {
            var nf = new NodeFunction("sqrt",
                new NodeExpression(_expr.Substring(4)).Compile());
            return nf.Compile();
        }
        return null!;
    }

    /// <summary>
    /// Tries to split the expression by binary operator
    /// </summary>
    /// <returns>Function if succeded, null if it failed.</returns>
    private Func<Func<string, double>, double> TryToSplitByBinaryOp()
    {
        string exprLeft = "", exprRight = "";
        char[] ops = { '+', '-', '*', '/', '^' }; // highest precendence operators go last
        bool found = false;
        char oper = ' ';
        foreach (var op in ops)
        {
            found = SplitByOp(_expr, op, out exprLeft, out exprRight);
            if (found)
            {
                oper = op;
                break;
            }
        }
        if (found)
        { // was able to split by operator => create NodeBinary with both sides and evaluate.

            // special case to handle things like -(2+3)
            //if (oper == '-' && exprLeft == "") exprLeft = "0";

            var ne1 = new NodeExpression(exprLeft).Compile();
            var ne2 = new NodeExpression(exprRight).Compile();
            var nb = new NodeBinary(ne1, ne2, oper, _expr);
            return nb.Compile();
        }
        return null!;
    }
}
