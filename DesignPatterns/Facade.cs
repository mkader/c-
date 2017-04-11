using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class ShapeMaker
    {
        private Factory.IShape circle;
        private Factory.IShape square;

        public ShapeMaker()
        {
            circle = new Factory.Circle();
            square = new Factory.Square();
        }
        public void DrawCircle()
        {
            circle.Draw();
        }

        public void DrawSquare()
        {
            square.Draw();
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            ShapeMaker sm = new ShapeMaker();
            sm.DrawCircle();
            sm.DrawSquare();
            Console.ReadLine();
        }
    }
}
