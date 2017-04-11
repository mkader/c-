using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullObject 
{
    public abstract class AbstractCustomer
    {
        protected string name;
        public abstract bool IsNil();
        public abstract string GetName();
    }

    public class RealCustomer : AbstractCustomer
    {
        public RealCustomer(string name)
        {
            this.name = name;
        }

        public override bool IsNil() {
            return false;
        }
        public override string GetName()
        {
            return name;
        }
    }

    public class NullCustomer : AbstractCustomer
    {
        public override bool IsNil()
        {
            return true;
        }
        public override string GetName()
        {
            return "Not Available in Customer Database";
        }
    }

    public class CustomerFactory
    {
        public static readonly string[] names = new string[]{"A","B","C"};
        
        public static AbstractCustomer GetCustomer(string name)
        {
            foreach(var n in names)
            {
                if (n == name) return new RealCustomer(name);
            }
            return new NullCustomer();
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            AbstractCustomer c1 = CustomerFactory.GetCustomer("A");
            AbstractCustomer c2 = CustomerFactory.GetCustomer("D");
            AbstractCustomer c3 = CustomerFactory.GetCustomer("B");
            AbstractCustomer c4 = CustomerFactory.GetCustomer("e");
            Console.WriteLine(c1.GetName());
            Console.WriteLine(c2.GetName());
            Console.WriteLine(c3.GetName());
            Console.WriteLine(c4.GetName());


            Console.Read();
        }
    }
}
