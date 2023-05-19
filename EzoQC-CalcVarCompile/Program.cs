// See https://aka.ms/new-console-template for more information
using NodeVarCompile;


//var expr = "(2 + 2 ) - (sqrt(x^2))";
var expr = "-sqrt((x*y)^2) -y ";
//var expr = "-0";
//var expr = "-x - -x";
var ne = new NodeExpression(expr);

//System.Diagnostics.Debug.WriteLine("Parsed started");
Func<Func<string,double>, double> f = ne.Compile();
//System.Diagnostics.Debug.WriteLine("Parsed finished");

//for ( var z = 0; z< 1000000; z++) {
for (var y = 0; y < 10; y++) 
{
    for (var x = 0; x < 10; x++)
    {
            var val = f(
                    (id) =>
                    {
                        if (id == "x") return x;
                        return y;
                    }
                 );
        Console.Write(val);
        Console.Write("\t");
    }
    Console.WriteLine();
}
//Console.Read();
//}
