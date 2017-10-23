using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CSharpSeven
{
    #region "Classes"
    public class Contact
    {
        public string Name { get; set; }
        public int  ID { get; set; }
        public string Department { get; set; }
    }
    public class HR : Contact {
        
        //public HR(string name, string address);
        public HR()
        {
            this.Name = "Emp 1";
            this.ID = 1;
            this.Department = "HR";
        }

        public void CreateEmployee() { }
    }

    public class Manager : Contact
    {
        public Manager(string name, int id, string department)
        {
            this.Name = name;
            this.ID = id;
            this.Department = department;
        }

        public void ContactTeamManager() { }

        public void ContactProjectManager() { }
    }
    #endregion "Classes"
    class Program
    {
        static void GetName(string first, string last, out string name)
        {
            name = first + " " + last;
        }

        static void PatternMatching(Contact contact)
        {
            switch (contact)
            {
                case HR hr:
                    hr.CreateEmployee();
                    break;
                case Manager teamManager when (teamManager.Department == "IT"):
                    teamManager.ContactTeamManager();
                    break;
                case Manager projectManager:
                    projectManager.ContactProjectManager();
                    break;
                default:
                    Console.WriteLine("Contract employee");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(contact));
            }
        }

        public static void GetID(object empID)
        {
            //older version, error  The name 'iEmpID'  or 'sEmpID' does not exist in the current context

            if (empID is int iEmpID || (empID is string sEmpID &&
            int.TryParse(sEmpID, out iEmpID)))
            {
                Console.WriteLine(iEmpID);
            }
        }

        //Throw Exceptions
        public static string GetName(string id)
        {
            //Old version
            /*if (id == null)
                throw new Exception("Name not found");
            return "F L";*/

            //Throw Exceptions can be use conditional, null coalescing and lambda expressions.
            return "F L" ?? throw new Exception("Name not found");
        }

        public static (string, string, string) EmployeeName(int empID) // tuple return type
        {
            var fn = "Day";
            var mn = "Night";
            var ln = "Soft";

            return (fn, mn, ln); // tuple literal
        }

        static void Main(string[] args)
        {
            //1. Out
            string first = "M";
            string last = "K";
            //have to pre-declare before using the out.
            string name = string.Empty; // OR var name = string.Empty;

            //var name; throws error Implicitly - typed variables must be initialized 
            GetName(first, last, out name);

            //In c# 7.0, you can declare in the variable line
            GetName(first, last, out string name1);
            GetName(first, last, out var name2); ////use var instead of string

            //2. PatternMatching
            Contact hr = new HR();
            PatternMatching(hr);
            Contact tm = new Manager("Emp 2", 2, "IT");
            PatternMatching(tm);
            Contact pm = new Manager("Emp 3", 3, "PM");
            PatternMatching(pm);
            Contact c = new Contact();
            PatternMatching(c);
            //PatternMatching(null);

            //3.Is Expression
            GetID(5);
            GetID("5");

            //4.Tuples
            var alpha = ("A", "B", "c");
            Console.WriteLine(alpha.Item1 + " - " + alpha.Item2 + " - " + alpha.Item3);

            (string first, string last, string middle) names = ("Day", "Night", "Soft");
            //OR (string first, string last, string middle) names = EmployeeName(100);
            Console.WriteLine(names.first + " - " + names.last + " - " + names.middle);
            //Error -The contextual keyword 'var' may only appear within a local variable declaration or in script code
            //var (first, last, middle) names = EmployeeName(100);
            //(var first, var last, var middle) names = LookupName(100);
            //string (first, last, middle) names = ("Day", "Night", "Soft");
            //(first, last, middle) names = ("Day", "Night", "Soft");

            var names1 = (first:"Day", last:"Night", middle:"Soft");
            Console.WriteLine(names1.first + " - " + names1.last + " - " + names1.middle);

            (string first, string last, string middle) names2 = (firstName: "Day", lastName: "Night", middleName: "Soft");
            Console.WriteLine(names2.first + " - " + names2.last + " - " + names2.middle);

            //5. Digit Separators
            long d = 1234567; 
            //OR
            long d1 = 1_234_567; //it would be represented as 1,234,567. 
            //The compiler will ignore the underscores at build time.

            Console.WriteLine(d + " - " + d1); //both same output

            var xv = 0xAB_CD_EF; //11259375
            //OR
            var xv1 = 0xABCDEF; //11259375
            Console.WriteLine(xv + " - " + xv1); //both same output

            var bv1 = 0b1101_0101_0100_0011_0010_0001; //13976353
            var bv2 = 0b110101010100001100100001;
            Console.WriteLine(bv1 + " - " + bv2); //both same output

            //6. Throw Exceptions
            GetName(null);


        }
    }
}
