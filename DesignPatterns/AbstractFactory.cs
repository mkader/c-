using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    //1.Create Shape & Color Interface(Product)
    public interface IColor //Product
    {
        void Fill();
    }

    //2.Create Concreate Class implements Interface(ConcreteProduct )
    public class Blue : IColor //ConcreteProduct
    {
        public void Fill()
        {
            Console.WriteLine("Fill Blue");
        }
    }

    public class Red : IColor //ConcreteProduct
    {
        public void Fill()
        {
            Console.WriteLine("Fill Red");
        }
    }

    //3.Create an Abstract class to get factories for Color and Shape Objects.
    public abstract class AbstractFactory
    {
        public abstract Factory.IShape ShapeCreate(string Shape);

        public abstract IColor ColorCreate(string Color);
    }

    //4.Create a Shop Factory & ColorFactory to generate object of concrete class
    public class ShapeFactory : AbstractFactory
    {
        public override Factory.IShape ShapeCreate(string Shape)
        {
            if (Shape == "Square") return new Factory.Square();
            else if (Shape == "Circle") return new Factory.Circle();
            return null;
        }

        public override IColor ColorCreate(string Color)
        {
            return null;
        }
    }

    public class ColorFactory : AbstractFactory
    {
        public override Factory.IShape ShapeCreate(string Shape)
        {
            return null;
        }

        public override IColor ColorCreate(string Color)
        {
            if (Color == "Blue") return new Blue();
            else if (Color == "Red") return new Red();
            return null;
        }
    }

    //5. Create a Factory generator/producer class to get factories by passing an information such as Shape or Color
    public class FactoryProducer
    {
        public static AbstractFactory Generator(string Choice)
        {
            if (Choice == "Shape") return new ShapeFactory();
            else if (Choice == "Color") return new ColorFactory();
            return null;
        }
    }

    //6.Create UI Demo Win Form
    public class Client
    {
        public static void Demo()
        {
            AbstractFactory shapeGen = FactoryProducer.Generator("Shape");
            Factory.IShape c = shapeGen.ShapeCreate("Circle");
            c.Draw();
            Factory.IShape s = shapeGen.ShapeCreate("Square");
            s.Draw();

            AbstractFactory colorGen = FactoryProducer.Generator("Color");
            IColor r = colorGen.ColorCreate("Red");
            r.Fill();
            IColor b = colorGen.ColorCreate("Blue");
            b.Fill();

            Console.ReadLine();
        }
    }
}
