using System.Diagnostics;

namespace NodeVarCompile;

/// <summary>
/// Node that is a call to one parameter function, eg: sqrt, sin, cos, etc.
/// </summary>
internal class NodeFunction : NodeCompilable
{
    private String Function;
    private Func<Func<string, double>, double> ParmNode;
    public NodeFunction(string function, Func<Func<string, double>, double> parameterNode)
    {
        Function = function;
        ParmNode = parameterNode;
    }

    public Func<Func<string, double>, double> Compile()
    {
        switch (Function)
        {
            case "sqrt": 
                return (f) 
                    => Math.Sqrt(ParmNode(f));
        }
        throw new Exception($"Function unknown: {Function}");
    }
}
