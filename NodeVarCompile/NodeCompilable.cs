namespace NodeVarCompile;

public interface NodeCompilable 
{
    /// <summary>
    /// Nodes must be able to evaluate themselves to a numeric value.
    /// 
    /// In the 'compiled expression with variables' the result of compiling is a function 
    /// that takes a function to return the value of the variables. 
    /// </summary>
    /// <returns></returns>
    public Func<Func<string, double>, double> Compile();
}
