using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento
{
    public class Memento
    {
        public string State { get; set; }
    }

    public class Originator
    {
        public string State { get; set; }
        public Memento Memento { get; set; }
    }

    public class CareTaker
    {
        private List<Memento> mementoList = new List<Memento>();
        
        public void Add(Memento state)
        {
            mementoList.Add(state);
        }

        public Memento Get(int index)
        {
            return mementoList[index];
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            Originator o = new Originator() { State="1"};
            CareTaker ct = new CareTaker();
            o.State = "2";
            ct.Add(o.Memento);

            o.State = "3";
            ct.Add(o.Memento);

            o.State = "4";
            Console.WriteLine("Current State " + o.State);

            o.Memento=ct.Get(0);
            Console.WriteLine("First State " + o.State);

            o.Memento=ct.Get(1);
            Console.WriteLine("Second State " + o.State);

            Console.Read();

        }
    }
}
