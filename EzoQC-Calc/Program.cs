using EzoQC_Calc;

// See https://aka.ms/new-console-template for more information

Console.WriteLine("*******************");
Console.WriteLine("EzoQC cases");
MathExpr.EvaluateExpr("1+1"); 
MathExpr.EvaluateExpr("1 + 2");
MathExpr.EvaluateExpr("1 + -1");
MathExpr.EvaluateExpr("-1 - -1");
MathExpr.EvaluateExpr("5 - 4");
MathExpr.EvaluateExpr("5*2");
MathExpr.EvaluateExpr("(2+5)*3");
MathExpr.EvaluateExpr("10/2");
MathExpr.EvaluateExpr("2+2*5+5");
MathExpr.EvaluateExpr("2.8*3-1");
MathExpr.EvaluateExpr("2^8");
MathExpr.EvaluateExpr("2^8*5-1");
MathExpr.EvaluateExpr("sqrt(4)");
MathExpr.EvaluateExpr("1/0");


Console.WriteLine("*******************");
Console.WriteLine("My cases");
MathExpr.EvaluateExpr("1+2*5+16/2^2");
MathExpr.EvaluateExpr("(1+2)*(5)/(1+2^2)");
MathExpr.EvaluateExpr("(1+2)*(5+1)");
MathExpr.EvaluateExpr("sqrt(3*3)/2+3");
MathExpr.EvaluateExpr("sqrt(2* 2+5) + 9/9");
MathExpr.EvaluateExpr("1 - -1");
MathExpr.EvaluateExpr("1 - (-1)");
MathExpr.EvaluateExpr("((1 - (-1)))");
MathExpr.EvaluateExpr("((1 - (-1.3)))");
MathExpr.EvaluateExpr("((1 - sqrt(4)))");
MathExpr.EvaluateExpr("2+ sqrt(9) - 1 / 0"); // intentional error
MathExpr.EvaluateExpr("((1 - sqrt(2^sqrt(4))))");
MathExpr.EvaluateExpr("-sqrt(4)");
MathExpr.EvaluateExpr("-(2+3)");
MathExpr.EvaluateExpr("-2+3");
MathExpr.EvaluateExpr("-2+3)"); // intentional error
MathExpr.EvaluateExpr("10/2");
MathExpr.EvaluateExpr("2.8*3-1");
MathExpr.EvaluateExpr("-sin(4)"); // intentional error