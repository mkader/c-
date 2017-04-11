using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory;

namespace Flyweight
{
    public class Triangle : Factory.IShape
    {
        private string _color;
        public Triangle(string color)
        {
            _color = color;
        }
        public void Draw()
        {
            Console.WriteLine(_color +  " : Triangle : Draw()");
        }
    }
    public class ShapeFactory
    {
        //properties - java is final, c# readonly
        //method - java is final, c# sealed
        private static readonly  Dictionary<string, Factory.IShape> triangle = new Dictionary<string, Factory.IShape>();

        public static Factory.IShape GetTriangle(string color)
        {
            Factory.IShape t;
            triangle.TryGetValue(color, out t);
            if (t == null)
            {
                t = new Triangle(color);
                triangle.Add(color, t);
                Console.WriteLine(color + " : Creating new Object ");
            }
            return t;
        }
    }

    public static class Client
    {
        private static readonly string[] colors = new string[] { "red", "blue", "green", "white", "black" };
        public static void Demo()
        {
            for (int i = 0; i < 20; i++)
            {
                Triangle t = (Triangle)ShapeFactory.GetTriangle(GetColors());
                t.Draw();
            }

            Console.ReadLine();

        }
        public static string GetColors()
        {
            Random r = new Random();
            string s  =colors[r.Next(0, colors.Length)];
            return s;
        }

    }
}
