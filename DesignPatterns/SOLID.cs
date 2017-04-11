using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SOLID
{
    //S - Single Responsibility Principle
    namespace SingleResponsibilityPrinciple
    {
        #region "Wrong Code  Customer Class doing logging actigivty instead of validation, data access"
        /*public class Customer
        {
            public void Add()
            {
                try {
                    //Database code goes here
                }catch(Exception e) {
                    System.IO.File.WriteAllText(@"c:\error.txt", e.ToString());
                }
            }
        }*/
        #endregion "Wrong Code  Customer Class doing logging actigivty instead of validation, data access"
        #region "Right Code  rewrite, but still need to remove try{}"
        public class FileLogger
        {
            public void Handle(string error)
            {
                System.IO.File.WriteAllText(@"c:\Error.txt", error);
            }
        }
        public class Customer
        {
            private FileLogger fl = new FileLogger();
            public void Add()
            {
                try
                {
                    //Database code goes here
                }
                catch (Exception e)
                {
                    fl.Handle(e.ToString());
                }
            }
        }
        #endregion "Right Code  rewrite, but still need to remove try{}"
    }

    //O - Open closed principle
    namespace OpenClosedPrinciple
    {
        #region "Not Good Code, if you add later another type, customer class need to modify and add anohter if"
        /*public class Customer
        {
            public int Type { get; set; }

            public double GetDiscount(double sales)
            {
                if (Type == 1) return sales* .10;
                else return sales* .15;
            }
        }*/
        #endregion "Not Good Code, if you add later another type, customer class need to modify and add anohter if"

        #region "Rewrite the above code to handle any type in future withour changing customer class"
        public class Customer
        {
            public virtual void Add() { }
            public virtual double GetDiscount(double sales)
            {
                return sales;
            }
        }

        public class SilverCustomer : Customer
        {
            public override double GetDiscount(double sales)
            {
                return base.GetDiscount(sales) - 50;

            }
        }
        public class GoldCustomer : SilverCustomer
        {
            public override double GetDiscount(double sales)
            {
                return base.GetDiscount(sales) - 100;

            }
        }

        [TestClass]
        public class Client1
        {
            [TestMethod]
            public void Demo()
            {
                double d = 0;
                Customer c = new SilverCustomer();
                d = c.GetDiscount(1000);
                c = new GoldCustomer();
                d = c.GetDiscount(1000);
            }
        }
        #endregion "Rewrite the above code to handle any type in future withour changing customer class"
    }

    //L - LSP (Liskov substitution principle)
    namespace LiskovSubstitutionPrinciple
    {
        #region "Not Good Code - Enquiry must discoutn calcuation, not add data in db.so it throws error"
        /*class Enquiry : OpenClosedPrinciple.Customer
        {
            public override double GetDiscount(double TotalSales)
            {
                return base.GetDiscount(TotalSales) - 5;
            }

            public override void Add()
            {
                throw new Exception("Not allowed");
            }

            [TestClass]
            public class Client1
            {
                [TestMethod]
                public void Demo()
                {
                    List<OpenClosedPrinciple.Customer> Customers = new List<OpenClosedPrinciple.Customer>();
                    Customers.Add(new OpenClosedPrinciple.SilverCustomer());
                    Customers.Add(new OpenClosedPrinciple.GoldCustomer());
                    Customers.Add(new Enquiry());

                    foreach (OpenClosedPrinciple.Customer o in Customers)
                    {
                        o.Add();
                    }
                }
            }
        }*/
        #endregion "Not Good Code - Enquiry must discoutn calcuation, not add data in db.so it throws error"

        #region "Good Code - Enquiry must discoutn calcuation, Custoemr discoutn and add"
        interface IDiscount { double GetDiscount(double sales); }
        interface IDatabase { void Add(); }
        class Enquiry : IDiscount
        {
            public virtual double GetDiscount(double sales)
            {
                return sales - 5;
            }
        }

        class Customer : IDiscount, IDatabase
        {
            public virtual double GetDiscount(double sales)
            {
                return sales - 25;
            }

            public virtual void Add()
            {
                try
                {
                    // Database code goes here
                }
                catch (Exception ex)
                {

                }
            }
        }

        [TestClass]
        public class Client1
        {
            [TestMethod]
            public void Demo()
            {
                List<OpenClosedPrinciple.Customer> Customers = new List<OpenClosedPrinciple.Customer>();
                Customers.Add(new OpenClosedPrinciple.SilverCustomer());
                Customers.Add(new OpenClosedPrinciple.GoldCustomer());
                //Customers.Add(new Enquiry()); //can't add figure it out

                foreach (OpenClosedPrinciple.Customer o in Customers)
                {
                    o.Add();
                }
            }
        }
        #endregion "Good Code - Enquiry must discoutn calcuation, Custoemr discoutn and add"
    }

    //I - ISP(Interface Segregation principle)
    namespace InterfaceSegregationPrinciple
    {
        #region "Not Good Code -Add new function in existing interface, it affect other cleints" 
        /*interface IDatabase
        {
            void Add(); // old client are happy with these.
            void Read(); // Added for new clients.
        }*/
        #endregion "Add new function in existing interface, it affect other cleints" 

        #region "Good Code - Create new interface and class" 
        interface IDatabase
        {
            void Add(); // old client are happy with these.
        }
        interface IDatabaseV1
        {
            void Read(); // Added for new clients.
        }
        class Customer : IDatabase
        {
            public void Add()
            {

            }

        }
        class CustomerwithRead : IDatabase, IDatabaseV1
        {
            public void Add()
            {
                Customer o = new Customer();
                o.Add();
            }
            public void Read()
            {       // Implements  logic for read 		}
            }
        }

        [TestClass]
        public class Client1
        {
            [TestMethod]
            public void Demo()
            {
                IDatabase i = new Customer(); // 1000 happy old clients not touched
                i.Add();

                IDatabaseV1 iv1 = new CustomerwithRead(); // new clients
                iv1.Read();
            }
        }
        #endregion "Add new function in existing interface, it affect other cleints" 
    }

    //D - Dependency inversion principle
    namespace DependencyInversionPrinciple
    {
        //SingleResponsibilityPrinciple.FileLogger is not good

         interface ILogger { void Handle(string error); }

         class FileLogger : ILogger
         {
             public void Handle(string error)
             {
                 System.IO.File.WriteAllText(@"c:\Error.txt", error);
             }
         }
         class EventLogger : ILogger
         {
             public virtual void Handle(string error)
             {
                 //log errors to event viewer  
             }
         }
         class EmailLogger : ILogger
         {
             public void Handle(string error)
             {
                 //send errors in email  
             }
         }
         interface IDiscount
         {
             void GetDiscount(); // old client are happy with these.
         }

         interface IDatabase
         {
             void Add(int handle); // old client are happy with these.
         }

        #region "Not Good - violating SRP but this time the aspect is different, ,its about deciding which objects should be created"

        /*class Customer : IDiscount, IDatabase
        {
            private ILogger obj;
            public virtual void GetDiscount()
            { }

            public virtual void Add(int handle)
            {
                try
                {
                    // Database code goes here
                }
                catch (Exception ex)
                {
                    if (handle == 1)
                    {
                        obj = new EventLogger();
                    }
                    else
                    {
                        obj = new EmailLogger();
                    }
                    obj.Handle(ex.Message.ToString());
                }
            }
        }*/
        #endregion "Not Good - violating SRP but this time the aspect is different, ,its about deciding which objects should be created"

        #region "Good "
        
         class Customer : IDiscount, IDatabase
         {
             private ILogger obj;
            public Customer(ILogger l)
            {
                obj = l;
            }
             public virtual void GetDiscount()
             { }

             public virtual void Add(int handle)
             {
                 try
                 {
                     // Database code goes here
                 }
                 catch (Exception ex)
                 {
                     if (handle == 1)
                     {
                         obj = new EventLogger();
                     }
                     else
                     {
                         obj = new EmailLogger();
                     }
                     obj.Handle(ex.Message.ToString());
                 }
             }

            [TestClass]
            public class Client
            {
                [TestMethod]
                public void Demo()
                {
                    IDatabase i = new Customer(new EmailLogger());
                }
            }
            
        }
        #endregion "Good "
    }
}    

