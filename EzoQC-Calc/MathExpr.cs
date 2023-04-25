using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_Calc
{
    internal static class MathExpr
    {
        
        public static void EvaluateExpr(String expr)
        {
            try { 
                var x = new NodeExpression(expr);               

                Console.WriteLine($"{expr} = {x.Evaluate()}");
            }
            catch (Exception ex)
            {
                    Console.WriteLine($"Exception trying to evaluate: {expr}\n{ex.Message}");
            }
        }

    }
}
