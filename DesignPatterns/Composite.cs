using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class Employee
    {
        private string name;
        private string dept;
        private int salary;
        private List<Employee> subOrdinates;

        
        public Employee(string name, string dept, int salary)
        {
            this.name = name;
            this.dept = dept;
            this.salary = salary;
            subOrdinates = new List<Employee>();
        } 

        public void Add(Employee e)
        {
            subOrdinates.Add(e);
        }

        public void Remove(Employee e)
        {
            subOrdinates.Remove(e);
        }

        public List<Employee> GetSubOrdinates()
        {
            return subOrdinates;
        }

        public override string ToString()
        {
            return "Name: " + name + " Dept: "+ dept + " Salary: " + salary;
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            Employee ceo = new Employee("A", "Head", 6);
            Employee sales= new Employee("B", "Head Sales", 5);
            Employee marketing = new Employee("C", "Head Marketing", 4);
            ceo.Add(sales);
            ceo.Add(marketing);

            Employee clerk1 = new Employee("D", "Marketing", 3);
            Employee clerk2 = new Employee("E", "Marketing", 2);
            marketing.Add(clerk1);
            marketing.Add(clerk2);

            Employee executive = new Employee("F", "Sales", 1);
            sales.Add(executive);

            Console.WriteLine(ceo);
            foreach(var e in ceo.GetSubOrdinates())
            {
                Console.WriteLine("\t"+e);
                foreach (var s in e.GetSubOrdinates())
                {
                    Console.WriteLine("\t\t" + e);
                }
            }
            Console.ReadLine();
        }
    }
}
