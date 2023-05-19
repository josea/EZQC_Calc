using System.Diagnostics;


namespace NodeVarCompile;

internal class NodeValue : NodeCompilable
{
    double Value;
    public NodeValue(double value)
    {
        Value = value;
    }

    public Func<Func<string, double>, double> Compile()
    {
        return (f) => 
        Value;
    }

}
