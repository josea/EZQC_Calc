// See https://aka.ms/new-console-template for more information
using EzoQC_CalcVarCompile;


//var expr = "(2 + 2 ) - (sqrt(x^2))";
var expr = "-sqrt((x*y)^2)";
//var expr = "-0";
//var expr = "-x - -x";
var ne = new NodeExpression(expr);

System.Diagnostics.Debug.WriteLine("Parsed started");
var f = ne.Compile();

System.Diagnostics.Debug.WriteLine("Parsed finished");

for (var y = 0; y < 10; y++) 
{
    for (var x = 0; x < 10; x++)
    {        
        Console.Write(
            f(
                (id) =>
                {
                    if (id == "x") return x;
                    return y;
                }
             )
         );
        Console.Write("\t");
    }
    Console.WriteLine();
}
