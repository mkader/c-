using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    public abstract class ShapeDecorator: Factory.IShape {
        protected Factory.IShape decoratedShape;

        public ShapeDecorator(Factory.IShape decoratedShape)
        {
            this.decoratedShape = decoratedShape;
        }
        abstract public void Draw();
    }

    public class RedShapeDecorator : ShapeDecorator
    {
        public RedShapeDecorator(Factory.IShape decoratedShape) : base(decoratedShape) { }
        
        public override void Draw()
        {
            decoratedShape.Draw();
            SetRedBorder(decoratedShape);
        }

        private void SetRedBorder(Factory.IShape decoratedShape)
        {
            Console.WriteLine("Red Border");
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            Factory.IShape c = new Factory.Circle();
            Factory.IShape rc = new RedShapeDecorator(new Factory.Circle());
            
            c.Draw();
            rc.Draw();
            
            Console.ReadLine();
        }
    }
}
