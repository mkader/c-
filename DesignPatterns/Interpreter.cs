using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public interface IExpression
    {
        bool Interpret(string context);   
    }

    public class TerminalExpression : IExpression
    {
        public string Data { get; set; }
        public bool Interpret(string context)
        {
            return context.Contains(Data);
        }
    }

    public class OrExpression : IExpression
    {
        public IExpression Exp1 { get; set; }
        public IExpression Exp2 { get; set; }
        public bool Interpret(string context)
        {
            return Exp1.Interpret(context) || Exp2.Interpret(context);
        }
    }
    public class AndExpression : IExpression
    {
        public IExpression Exp1 { get; set; }
        public IExpression Exp2 { get; set; }
        public bool Interpret(string context)
        {
            return Exp1.Interpret(context) && Exp2.Interpret(context);
        }
    }

    public static class Client
    {
        public static IExpression GetMaleExpression()
        {
            IExpression e1 = new TerminalExpression() { Data = "A" };
            IExpression e2 = new TerminalExpression() { Data = "B" };
            return new OrExpression() { Exp1 = e1, Exp2 = e2 };
        }

        public static IExpression GetMarriedWomanExpression()
        {
            IExpression e1 = new TerminalExpression() { Data = "C" };
            IExpression e2 = new TerminalExpression() { Data = "1" };
            return new AndExpression() { Exp1 = e1, Exp2 = e2 };
        }
        public static void Demo()
        {
            IExpression e1 = GetMaleExpression();
            IExpression e2 = GetMarriedWomanExpression();

            Console.WriteLine("A is male? " + e1.Interpret("A")); //true
            Console.WriteLine("C is a married women "+e2.Interpret("C 1")); //true

            Console.ReadLine();
        }
    }
}
