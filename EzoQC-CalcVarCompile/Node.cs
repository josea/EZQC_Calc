using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EzoQC_CalcVarCompile
{
    internal abstract class Node
    {
        /// <summary>
        /// Nodes must be able to evaluate themselves to a numeric value.
        /// 
        /// In the 'compiled expression with variables' the result of compiling is a function 
        /// that takes a function to return the value of the variables. 
        /// </summary>
        /// <returns></returns>
        public abstract Func<Func<string, double>, double> Compile();
    }
}
