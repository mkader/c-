using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory {
    
    //1.Create Interface(Product)
    public interface IShape
    {
        void Draw();
    }

    //2.Create Concreate Class implements Interface(ConcreteProduct )
    public class Circle : IShape 
    {
        public void Draw()
        {
            Console.WriteLine("Drew Cirlce");
            //throw new System.NotImplementedException();
        }
    }

    public class Square : IShape 
    {
        public void Draw()
        {
            Console.WriteLine("Drew Square");
            //throw new System.NotImplementedException();
        }
    }

    //3.Create a Factory to generate object of concrete class (Factory)
    public class Factory //Factory
    {
        public IShape Create(string Shape)
        {
            if (Shape == "Circle") return new Circle();
            if (Shape == "Square") return new Square();
            return null;
        }
    }

    //4.Create UI Demo Win Form
    public class Client
    {
        public static void Demo()
        {
            Factory f = new Factory();

            IShape c = f.Create("Circle");
            c.Draw();

            IShape s = f.Create("Square");
            s.Draw();

            Console.ReadLine();
        }
    }
}

