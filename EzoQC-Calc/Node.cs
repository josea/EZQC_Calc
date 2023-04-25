using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EzoQC_Calc
{
    internal abstract class Node{
        /// <summary>
        /// Nodes must be able to evaluate themselves to a numeric value.
        /// </summary>
        /// <returns></returns>
        public abstract double Evaluate(); 
    }
 
}
