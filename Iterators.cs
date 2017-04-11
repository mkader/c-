using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace CSharp
{
    [TestClass]
    public class Iterators
    {
        /*
        - An iterator can be used to step through collections such as lists and arrays by using the yield keyword. 
	    - Multiple iterators (must unique name) can be implemented on a class.  
	    - The return type of an iterator must be IEnumerable, IEnumerator, IEnumerable<T>, or IEnumerator<T>.
	    - The declaration can't have any ref or out parameters.
	    - An iterator code by using a foreach statement or a LINQ query.
	    - There are 3 types of Iterator blocks. A method body, An accessor body & An operator body
         */
        [TestMethod]
        public void Main()
        {
            //it will go DisplayOdd(), yield return 1, print 1, then it go DisplayOdd() yield return 2, print 2		
            foreach (int i in DisplayOdd()) Debug.WriteLine(i);

            foreach (int i in DisplayEven(21, 30)) Debug.WriteLine(i);

            DisplayDays d = new DisplayDays();
            foreach (string i in d) Debug.WriteLine(i);

            Zoo z = new Zoo();
            foreach (Animal a in z) Debug.WriteLine(a.name + " " + a.type);
            foreach (Animal a in z.GetAnimalType(2)) Debug.WriteLine(a.name + " " + a.type);

        }

        static IEnumerable<int> DisplayOdd()
        {
            yield return 1;
            yield return 3;
            yield return 5;
        }

        static IEnumerable<int> DisplayEven(int s, int e)
        {
            for (int i = s; i <= e; i++)
            {
                if (i % 2 == 0) yield return i;
            }
        }
    }

    //get a method
    public class DisplayDays  {

        private string[] days = { "M", "T", "W", "TH", "F", "S", "SU" };
        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < days.Length; i++)
            {
                yield return days[i];
            }
        }
    }

    // a get accessor 
    public class Zoo
    {
        List<Animal> animals = new List<Animal>();
        public Zoo()
        {
            animals.Add(new Animal { name = "Cow", type = 1 });
            animals.Add(new Animal { name = "Dog", type = 2 });
            animals.Add(new Animal { name = "Chicken", type = 1 });
            animals.Add(new Animal { name = "Cat", type = 2 });
        }
        public IEnumerator<Animal> GetEnumerator()
        {
            foreach (Animal a in animals) yield return a;
        }

        public IEnumerable<Animal> GetAnimalType(int i)
        {
            foreach (Animal a in animals)
            {
                if (a.type == i) yield return a;
            }
        }
    }

    public class Animal
    {
        public string name { get; set; }
        public int type { get; set; }
    }
}
