using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_CalcVarCompile
{
    /// <summary>
    /// Node that is a call to one parameter function, eg: sqrt, sin, cos, etc.
    /// </summary>
    internal class NodeFunction : Node
    {
        private String Function;
        private Func<Func<string, double>, double> ParmNode;
        public NodeFunction(string function, Func<Func<string, double>, double> parameterNode)
        {
            Function = function;
            ParmNode = parameterNode;
        }

        public override Func<Func<string, double>, double> Compile()
        {
            Debug.WriteLine("Expression Parser");
            switch (Function)
            {
                case "sqrt": 
                    return (f) 
                        => Math.Sqrt(ParmNode(f));
            }
            throw new Exception($"Function unknown: {Function}");
        }
    }
}
