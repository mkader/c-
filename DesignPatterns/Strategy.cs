using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public interface Strategy
    {
        int DoOperation(int i, int j);
    }

    public class OperationAdd :Strategy
    {
        public int DoOperation(int i, int j)
        {
            return i + j;
        }
    }

    public class OperationSub : Strategy
    {
        public int DoOperation(int i, int j)
        {
            return i - j;
        }
    }

    public class Context
    {
        private Strategy strategy;
        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public int ExecuteStrategy(int i, int j)
        {
            return strategy.DoOperation(i, j);
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            Context c = new Context(new OperationAdd());
            Console.WriteLine( c.ExecuteStrategy(1, 2));
            c = new Context(new OperationSub());
            Console.WriteLine(c.ExecuteStrategy(1, 2));
            Console.Read();
        }
    }

}
