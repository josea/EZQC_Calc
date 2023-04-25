using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_CalcVarCompile
{
    internal class NodeValue : Node
    {
        double Value;
        public NodeValue(double value)
        {
            Value = value;
        }

        public override Func<Func<string, double>, double> Compile()
        {
            Debug.WriteLine("Expression Parser");
            return (f) => 
            Value;
        }

    }
}
