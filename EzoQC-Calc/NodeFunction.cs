using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_Calc
{
    /// <summary>
    /// Node that is a call to one parameter function, eg: sqrt, sin, cos, etc.
    /// </summary>
    internal class NodeFunction : Node
    {
        private String Function;
        private Node ParmNode;
        public NodeFunction(string function, Node parameterNode)
        {
            Function = function;
            ParmNode = parameterNode;
        }

        public override double Evaluate()
        {
            switch (Function)
            {
                case "sqrt": return Math.Sqrt(ParmNode.Evaluate());
            }
            throw new Exception($"Function unknown: {Function}");
        }
    }
}
