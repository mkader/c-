using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
    }

    public interface ICriteria
    {
        List<Person> MeetCriteria(List<Person> persons);
    }

    public class Male : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            return persons.Where(p => p.Gender == "Male").ToList();
        }
    }

    public class Female : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            return persons.Where(p => p.Gender == "Female").ToList();
        }
    }

    public class Single : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            return persons.Where(p => p.Status == "Single").ToList();
        }
    }

    public class AndCriteria : ICriteria
    {
        public ICriteria First { get; set; }
        public ICriteria Second { get; set; }
        
        public List<Person> MeetCriteria(List<Person> persons)
        {
            return Second.MeetCriteria(First.MeetCriteria(persons));
        }
    }

    public class OrCriteria : ICriteria
    {
        public ICriteria First { get; set; }
        public ICriteria Second { get; set; }

        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> fl = First.MeetCriteria(persons);
            List<Person> sl = Second.MeetCriteria(persons);
            return fl.Union(sl).ToList();
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            List<Person> persons = new List<Person>();

            persons.Add(new Person() {Name= "Robert", Gender="Male", Status="Single" });
            persons.Add(new Person() {Name = "John", Gender="Male", Status="Married" });
            persons.Add(new Person() { Name = "Mike", Gender = "Male", Status = "Single" });
            persons.Add(new Person() { Name = "Bobby", Gender = "Male", Status = "Single" });
            persons.Add(new Person() { Name = "Laura", Gender = "Female", Status = "Married" });
            persons.Add(new Person(){Name="Diana", Gender="Female", Status="Single" });


            ICriteria male = new Male();
            ICriteria female = new Female();
            ICriteria single = new Single();
            ICriteria singleMale = new AndCriteria() { First = single, Second = male };
            ICriteria singleOrFemale = new OrCriteria() { First = single, Second = female};

            Console.WriteLine("Males: ");
            printPersons(male.MeetCriteria(persons));

            Console.WriteLine("Females: ");
            printPersons(female.MeetCriteria(persons));

            Console.WriteLine("singles: ");
            printPersons(single.MeetCriteria(persons));

            Console.WriteLine("Single Males: ");
            printPersons(singleMale.MeetCriteria(persons));

            Console.WriteLine("Single Or Females: ");
            printPersons(singleOrFemale.MeetCriteria(persons));

            Console.ReadLine();
        }

        public static void printPersons(List<Person> persons)
        {

            foreach(var p in persons)
            {
                Console.WriteLine("Person : [ Name : " + p.Name + ", Gender : " + p.Gender + ", Status : " + p.Status + " ]");
            }
        }
    }
}
