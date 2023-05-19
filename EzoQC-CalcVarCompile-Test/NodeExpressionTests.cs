using NodeVarCompile;

namespace NodeVarCompileTests;

[TestClass]
public class NodeExpressionTests
{

    [DataTestMethod]
    [DataRow(1,1,2)]
    [DataRow(2,2,4)]
    [DataRow(3,-3,0)]
    [DataRow(3.5, -3, 0.5)]
    [DataRow(-3, -3, -6)]
    [DataRow(-3, 3, 0)]
    public void TestSimpleAddition(double x, double y, double expected)
    {
        var ne = new NodeExpression($"{x} + {y}");

        var f = ne.Compile();
        Assert.AreEqual(expected, f((id) => 0));
    }

    [DataTestMethod]
    [DataRow(1, 1, 0)]
    [DataRow(2, 2, 0)]
    [DataRow(3, -3, 6)]
    [DataRow(3.5, -3, 6.5)]
    [DataRow(-3, -3, 0)]
    [DataRow(-3, 3, -6)]
    public void TestSubstraction(double x, double y, double expected)
    {
        var ne = new NodeExpression($"{x} - {y}");
        var f = ne.Compile();
        Assert.AreEqual(expected, f((id) => 0));
    }

    [TestMethod]
    public void TestInvalidOperator()
    {
        var ne = new NodeExpression("1 ? 0");
        Assert.ThrowsException<Exception>(() =>  ne.Compile());             
    }

    [TestMethod]
    public void TestNestedExpression()
    {
        var ne = new NodeExpression("(2 + 1) * 3 ^2");
        var f = ne.Compile();
        Assert.AreEqual((2 + 1) * 9 , f((id) => 0));
    }

    [TestMethod]
    public void TestSimpleFunction()
    {
        var ne = new NodeExpression("2 *x +5 ");
        var f = ne.Compile();
        for (int x = 0; x < 1000; x++)
        {
            Assert.AreEqual(2 *x + 5, f((id) => id == "x" ? x : 0));
        }
    }
    [TestMethod]
    public void Test2VarsFunction()
    {
        var ne = new NodeExpression("2 *x +sqrt(y) ");
        var f = ne.Compile();
        for (int x = 0; x < 1000; x++)
            for (int y = 0; y < 1000; y++)
            {
                {
                    Assert.AreEqual(2 * x + Math.Sqrt(y), f((id) => id == "x" ? x : (id == "y" ? y : 0)));
                }
            }
    }

    [TestMethod]
    public void TestEllipsis()
    {
        var ne = new NodeExpression("(2 + 3) * (5 -2)");
        var f = ne.Compile();
        Assert.AreEqual<double>(15, f((id) => 0));
    }


    [DataTestMethod]
    [DataRow("1/0")]
    [DataRow("1/ (3-3)")]
    [DataRow("1/ ((3-1)*0)")]
    public void ShouldThrowDivisionByZero(string Expression)
    {
        var ne = new NodeExpression(Expression);
        var f = ne.Compile();            
        Assert.ThrowsException<DivideByZeroException>(()=> f((id) => 0) );
    }

    [TestMethod]
    public void TestDivision()
    {
        var ne = new NodeExpression("4 / 2");
        var f = ne.Compile();
        Assert.AreEqual<double>(2, f((id)=>0));

    }

    [TestMethod]
    public void TestFunctions()
    {
        var ne = new NodeExpression("sqrt(4)");
        var f = ne.Compile();
        Assert.AreEqual<double>(2, f((id) => 0));
    }

    [DataTestMethod]
    [DataRow("-(4)", -4)]
    [DataRow("-4", -4)]
    [DataRow("-sqrt(4)", -2)]
    public void TestNegate(string expression, double expected)
    {
        var ne = new NodeExpression(expression);
        var f = ne.Compile();
        Assert.AreEqual<double>(expected, f((id) => 0));
    }

    [TestMethod]
    public void ShouldThrowExceptionUnparseable()
    {
        var functionName = "unsupportedfunction(4)";
        var ne = new NodeExpression($"{functionName}");        
        var exception = Assert.ThrowsException<Exception>(() => ne.Compile());
        Assert.AreEqual<string>($"Unparseable: {functionName}", exception.Message );
    }

}