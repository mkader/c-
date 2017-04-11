using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    public interface ComputerPart
    {
        void Accept(ComputerPartVisitor cpv);
    }

    public class Keyboard : ComputerPart
    {
        public void Accept(ComputerPartVisitor cpv)
        {
            cpv.Visit(this);
        }
    }

    public class Mouse : ComputerPart
    {
        public void Accept(ComputerPartVisitor cpv)
        {
            cpv.Visit(this);
        }
    }

    public class Monitor : ComputerPart
    {
        public void Accept(ComputerPartVisitor cpv)
        {
            cpv.Visit(this);
        }
    }

    public class Computer : ComputerPart
    {
        ComputerPart[] parts;

        public Computer()
        {
            parts = new ComputerPart[] { new Mouse(), new Keyboard(), new Monitor() };
        }

        public void Accept(ComputerPartVisitor cpv)
        {
            foreach(var p in parts)
            {
                p.Accept(cpv);
            }
            cpv.Visit(this);
        }
    }

    public interface ComputerPartVisitor
    {
        void Visit(Computer c);
        void Visit(Mouse m);
        void Visit(Keyboard k);
        void Visit(Monitor m);
    }

    public class ComputerPartDisplayVisitor : ComputerPartVisitor
    {
        public void Visit(Computer c)
        { 
            Console.WriteLine("Display Computer");
        }
        public void Visit(Mouse m)
        { 
            Console.WriteLine("Display Mouse");
        }
        public void Visit(Keyboard k)
        {
            Console.WriteLine("Display Keyboard");
        }
        public void Visit(Monitor m)
        {
            Console.WriteLine("Display Monitor");
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            ComputerPart c = new Computer();
            c.Accept(new ComputerPartDisplayVisitor());


            Console.Read();
        }
    }
}
