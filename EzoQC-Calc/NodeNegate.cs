using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzoQC_Calc
{
    internal class NodeNegate : Node
    {
        Node Child; 
        public NodeNegate(Node child)
        {
            Child = child; 
        }

        public override double Evaluate()
        {
            return -Child.Evaluate();
        }
    }
}
