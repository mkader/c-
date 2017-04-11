using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public abstract class Game
    {
        public abstract void Initialize();
        public abstract void Start();
        public abstract void End();

        //public virtual void Play() - virutal can be override. 
        public void Play() //leave virtual, can't override
        {
            Initialize();
            Start();
            End();
        }
    }

    public class Football :Game
    {
        public override void Initialize() {
            Console.WriteLine("Football Game is Initialized");
        }
        public override void Start()
        {
            Console.WriteLine("Football Game is Starred");
        }
        public override void End()
        {
            Console.WriteLine("Football Game is End");
        }

    }

    public class Baseball : Game
    {
        public override void Initialize()
        {
            Console.WriteLine("Baseball Game is Initialized");
        }
        public override void Start()
        {
            Console.WriteLine("Baseball Game is Starred");
        }
        public override void End()
        {
            Console.WriteLine("Baseball Game is End");
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            Football f = new Football();
            f.Play();

            Baseball b = new Baseball();
            b.Play();
            Console.Read();
        }
    }
}
