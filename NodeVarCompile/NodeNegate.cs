namespace NodeVarCompile;

internal class NodeNegate : NodeCompilable
{
    Func<Func<string, double>, double> Child; 
    public NodeNegate(Func<Func<string, double>, double> child)
    {
        Child = child; 
    }

    public Func<Func<string, double>, double> Compile()
    {
        return 
            (f) => Child(f) == 0.0 ? 0 : -Child(f);
        // workaround, -0 is output as "-0" Double.ToString. To test: 
        // double x = 0; 
        // x = -x;
        // var s = x.ToString(); // <= "-0";
        // seems like a weird bug in .NET.
    }
}
