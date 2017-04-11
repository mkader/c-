using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    //Bridge - This is an interface which acts as a bridge between the abstraction class and implementer 
    //classes and also makes the functionality of implementer class independent from the abstraction class.
    public interface IDrawAPI
    {
        void DrawCircle(int radius, int x, int y);
    }

    //ImplementationA & ImplementationB - concreate classes
    public class RedCircle : IDrawAPI
    {
        public void DrawCircle(int radius, int x, int y)
        {
            Console.WriteLine("Draw Red Circle");
        }
    }

    public class GreenCircle : IDrawAPI
    {
        public void DrawCircle(int radius, int x, int y)
        {
            Console.WriteLine("Draw Green Circle");
        }
    }

    //Abstraction - This is an abstract class and containing members 
    //that define an abstract business object and its functionality. 
    //It contains a reference to an object of type Bridge. 
    //It can also acts as the base class for other abstractions.
    public abstract class Shape
    {
        protected IDrawAPI drawAPI;
        protected Shape(IDrawAPI drawAPI)
        {
            this.drawAPI = drawAPI;
        }

        public abstract void Draw();
    }

    //Redefined Abstraction - This is a class which inherits from the Abstraction class. 
    //It extends the interface defined by Abstraction class.
    public class Circle :Shape
    {
        private int x;
        private int y;
        private int radius;

        //super(java) is equal to base (c#)
        public Circle(int x, int y, int radius, IDrawAPI drawAPI) :base(drawAPI)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public override void Draw()
        {
            drawAPI.DrawCircle(radius, x, y);
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            Shape red = new Circle(100, 100, 100, new RedCircle());
            Shape green = new Circle(100, 100, 100, new GreenCircle());

            red.Draw();
            green.Draw();

            Console.ReadLine();
        }
    }
}
