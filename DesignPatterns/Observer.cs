using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    public class Subject
    {
        private List<Observer> observers = new List<Observer>();

        private int state;
        public int State
        {
            get { return state; }
            set
            {
                state = value;
                NotifyAllObjerver();
            }
        }


        public void Attach(Observer o)
        {
            observers.Add(o);
        }

        public void NotifyAllObjerver()
        {
            foreach (Observer o in observers)
            {
                o.Update();
            }
        }
    }

    public abstract class Observer
    {
        protected Subject subject;
        public abstract void Update();
    }

    public class BinaryObserver : Observer
    {
        public BinaryObserver(Subject s)
        {
            this.subject = s;
            this.subject.Attach(this);
        }

        public override void Update()
        {
            Console.WriteLine("Binary " + Convert.ToString(subject.State,2));
        }
    }

    public class OctalObserver: Observer
    {
        public OctalObserver(Subject s)
        {
            this.subject = s;
            this.subject.Attach(this);
        }

        public override void Update()
        {
            Console.WriteLine("Octal " + Convert.ToString(subject.State, 8));
        }

    }

    public class HexObserver : Observer
    {
        public HexObserver(Subject s)
        {
            this.subject = s;
            this.subject.Attach(this);
        }

        public override void Update()
        {
            Console.WriteLine("Hex " + Convert.ToString(subject.State, 16));
        }

    }

    public static class Client
    {
        public static void Demo()
        {
            Subject s = new Subject();
            new BinaryObserver(s);
            new OctalObserver(s);
            new HexObserver(s);

            Console.WriteLine("First");
            s.State=15;
            Console.WriteLine("Second");
            s.State = 10;

            Console.Read();

        }
    }
}
