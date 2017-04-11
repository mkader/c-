using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace QuestPond
{
    #region "Step 1 - Class Customer & Lead Created - Not Used"
    /*public class Customer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
    }

    public class Lead
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
    }*/
    #endregion "Step 1 - Class Customer & Lead Created"

    #region "Step 2 -  CusomerBase class extends to customer and lead, Validation Added - Not Used"
    /*public class CustomerBase
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        //public  void Validate() { } //it can't override, as per requirement, it should override, so added virutal
        //public virtual void Validate() { } //change to virutal, it define by the child classes
        public virtual void Validate()
        {
            throw new Exception("Not Implemented"); //it will be implemented by the child classes
        }
    }
    public class Customer : CustomerBase
    {
        //By defining a method as virtual,your child classes, can go and override the validate method
        public override void Validate()
        {
            //base.Validate();
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
            if (BillAmount > 0) throw new Exception("Bill is required");
            if (BillDate >= DateTime.Now) throw new Exception("Bill date  is not proper");
        }
    }
    public class Lead : CustomerBase
    {
        public override void Validate()
        {
            //base.Validate();
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
        }
    }*/
    #endregion "Step 2 -  CusomerBase class extends to customer and lead, Validation Added"

    #region "Step 3 -  UI Project Created for testing - Not Used"
    /*public class FrmCustomer 
    {
        string cmbCustomerType = string.Empty;
        private Customer cust = null;
        private Lead lead = null;
        //Add Gold Customer Here 
        
        private void cmbCustomerType_SelectedIndexChanged()
        {
            if (cmbCustomerType == "Customer") cust = new Customer();
            else lead = new Lead();
            //Add Gold Customer Here 
        }

        //Validate Button
        private void btnValidate_Click()
        {
            if (cmbCustomerType == "Customer") cust.Validate();
            else lead.Validate();
            //Add Gold Customer Here 
        }
    }*/
    #endregion "Step 3 -  UI Project Created"

    #region "Step 4 -  minimize UI Project - Not Used"
    /*public class FrmCustomer
    {
        //both of our classes (customer and lead) inherits form the customer base class.
        private CustomerBase cust = null;
        
        private void cmbCustomerType_SelectedIndexChanged(string cmbCustomerType)
        {
            //this is polymorphism - wherein the parent class can point toward his child classes on runtime
            //Polymorphism means change as per situation
            //Polymorphism is the fundamental thing to acheive decoupling (like i am se at office, dad at home).

            if (cmbCustomerType == "Customer") cust = new Customer();
            else cust = new Lead();
        }

        //Validate Button
        private void btnValidate_Click()
        {
          cust.Validate();
        }
    }*/
    #endregion "Step 4 -  minimize UI Project"

    #region "Step 5 - Factory Project Created - Not Used"
    /*public static class Factory
    {
        public static CustomerBase Create(string TypeCust)
        {
            //But when he returns this strong type it will always return the parent based class
            if (TypeCust == "Customer") return new Customer();
            else return new Lead();
        }
    }*/
    #endregion "Step 5 - Factory Project Created - Not Used"

    #region "Step 5 - Change in UI Project - Call Factory.Create - Not Used"
    /*public class FrmCustomer
    {
        private CustomerBase cust = null;

        private void cmbCustomerType_SelectedIndexChanged(string cmbCustomerType)
        {
            cust = Factory.Create(cmbCustomerType);
        }

        private void btnValidate_Click()
        {
            cust.Validate();
        }
    }*/
    #endregion "Step 5 - Change in UI Project"

    #region "Step 6 - Remove if conidtion from Factory Project - Simple Factory pattern & RIP [Design_Pattern] - Not Used"
    //This centralization of object creation called as Simple Factory pattern [Design_Pattern]
    /*public static class Factory
    {
        private static Dictionary<string, CustomerBase> custs = new Dictionary<string, CustomerBase>();
        static Factory()
        {
            //this customer collections is loaded irrespective you wanted or you don't wanted
            custs.Add("Customer", new Customer());
            custs.Add("Lead", new Lead());
        }
        public static CustomerBase Create(string TypeCust)
        {
            //Getting rid of this if conditions by using polymorphism called as RIP(Replace if with Polymorphism) pattern. [Design_Pattern] 
            return custs[TypeCust];
        }
    }*/
    #endregion "Step 6 - Remove if conidtion from Factory Project - Simple Factory pattern & RIP(Replace if with Polymorphism) [Design_Pattern]"

    #region "Step 7 - Customer type should be loaded only on demand - Lazy Or Eager Loading [Design_Pattern] - Not Used"
    /*public static class Factory
    {
        private static Dictionary<string, CustomerBase> custs = null;
        public static CustomerBase Create(string TypeCust)
        {
            //Lazy Loading [Design_Pattern] means when the objects are needed they are loaded or else they are not loaded
            //The opposite if Lazy Loading can be termed as eager loading
            if (custs.Count == 0)
            {
                custs = new Dictionary<string, CustomerBase>();
                custs.Add("Customer", new Customer());
                custs.Add("Lead", new Lead());
            }
            return custs[TypeCust];
        }
    }*/
    #endregion "Step 7 - Customer type should be loaded only on demand - Lazy Or Eager Loading [Design_Pattern]"

    #region "Step 8 - Create Customer Interface Project - Not Used"
    /*public interface ICustomer
    {
        string CustomerName { get; set; }
        string PhoneNumber { get; set; }
        decimal BillAmount { get; set; }
        DateTime BillDate { get; set; }
        string Address { get; set; }
        void Validate();
    }*/
    #endregion "Step 8 - Create Customer Interface Project"

    #region "Step 9 - Cust/lead extends ICustomer - PIC (Polymorphism + Interfaces + Centralizing object creation) Pattern for decoupling [Design_Pattern] - Not Used"
    /*public class Customer : ICustomer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public  void Validate()
        {
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
            if (BillAmount > 0) throw new Exception("Bill is required");
            if (BillDate >= DateTime.Now) throw new Exception("Bill date  is not proper");
        }
    }
    public class Lead : ICustomer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
        public  void Validate()
        {
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
        }
    }*/
    #endregion "Step 9- Customer & lead extends ICustomer"

    #region "Step 9 - Change Factory Class using ICustomer - Not Used"
    /*public static class Factory
    {
        private static Dictionary<string, ICustomer> custs = null;
        public static ICustomer Create(string TypeCust)
        {
            if (custs.Count == 0)
            {
                custs = new Dictionary<string, ICustomer>();
                custs.Add("Customer", new Customer());
                custs.Add("Lead", new Lead());
            }
            return custs[TypeCust];
        }
    }*/

    #region "Step 9a - Automating Lazy loading using Lazy Keyword(need to rewrite?)"
    /*public static class Factory
    {
        public static Lazy<Dictionary<string, ICustomer>> custs = null;

        static Factory()
        {
            custs = new Lazy<Dictionary<string, ICustomer>>(() => LoadCustomerType());
        }

        private static Dictionary<string, ICustomer> LoadCustomerType()
        {
            Dictionary<string, ICustomer> c = new Dictionary<string, ICustomer>();
            c.Add("Customer", new Customer());
            c.Add("Lead", new Lead());
            return c;
        }

        public static ICustomer Create(string CustType)
        {
            return custs.Value[CustType];
        }
    }*/
    #endregion "Step 9a - Automating Lazy loading using Lazy Keyword(need to rewrite?)"
    #endregion "Step 9 - Change Factory Class using ICustomer"

    #region "Step 9 - Change UI Project - User ICustomer Interface - Not Used"
    /* public class FrmCustomer
     {
         private ICustomer cust = null;

         private void cmbCustomerType_SelectedIndexChanged(string cmbCustomerType)
         {
             cust = Factory.Create(cmbCustomerType);
         }

         private void btnValidate_Click()
         {
             cust.Validate();
         }
     }*/
    #endregion "Step 9 - Change UI Project - User ICustomer Interface"

    #region "Step 10 - ICustomer Added Clone method - Implementing cloning (Prototype pattern) [Design_Pattern]"
    /*The above factory pattern class has a defect
        ICustomer cust = Factory<ICustomer>.Create("Customer");
        cust.CustomerName = "Mak";
        ICustomer custnew = Factory<ICustomer>.Create("Customer");
        //custnew.CustomerName  showing "Mak"
    Both are return the same instance, because the factory pattern is pointing to the same instance of the collection.*/
    public interface ICustomer
    {
        string CustomerName { get; set; }
        string PhoneNumber { get; set; }
        decimal BillAmount { get; set; }
        DateTime BillDate { get; set; }
        string Address { get; set; }
        void Validate();
        ICustomer Clone(); // Added an extra method clone
    }
    #endregion "Step 10 - ICustomer Added Clone method - Implementing cloning (Prototype pattern) [Design_Pattern]"

    #region "Step 10 - Customer & lead -  Added Clone method - Not Used"
    /*public class Customer : ICustomer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public void Validate()
        {
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
            if (BillAmount > 0) throw new Exception("Bill is required");
            if (BillDate >= DateTime.Now) throw new Exception("Bill date  is not proper");
        }
        public ICustomer Clone()
        {
            return (ICustomer)this.MemberwiseClone();
        }
    }
    public class Lead : ICustomer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
        public void Validate()
        {
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
        }
        public ICustomer Clone()
        {
            return (ICustomer)this.MemberwiseClone();
        }
    }*/
    #endregion "Step 10 - Customer & lead -  Added Clone method"

    #region "Step 10 - Factory - .Clone method added - Not Used"
    /*public static class Factory
    {
        private static Dictionary<string, ICustomer> custs = null;
        public static ICustomer Create(string TypeCust)
        {
            if (custs.Count == 0)
            {
                custs = new Dictionary<string, ICustomer>();
                custs.Add("Customer", new Customer());
                custs.Add("Lead", new Lead());
            }
            return custs[TypeCust].Clone();
        }
    }*/
    #endregion "Step 10 - Factory - .Clone method added"
   
    #region "Step 11 - Automating Factory using Unity - Not Used"
    /*Using some DI frameworks like unity, ninject, MEF etc, instead of writing the above codes.
    Install Unity (Install-Package Unity -Version 2.1.505.2 , .net 4.0)*/
    //using Microsoft.Practices.Unity;
    /*public static class Factory
    {
        //In unity or any DI framework concept of the containers. 
        //These containers are nothing but collections. 
        //“RegisterType” and “ResolveType” methods helps to add and get objects 
        //from the container collection respectively.
        static IUnityContainer cont = null; //private static Dictionary<string, ICustomer> custs = null;

        static Factory()
        {
            cont = new UnityContainer();
            cont.RegisterType<ICustomer, Lead>("Lead");         //c.Add("Customer", new Customer());
            cont.RegisterType<ICustomer, Customer>("Customer");     //c.Add("Lead", new Lead());
        }

        public static ICustomer Create(string CustType)
        {
            return cont.Resolve<ICustomer>(CustType).Clone();           //custs.Value[CustType];
        }
    }*/
    #endregion "Step 11 - Automating  Factory using Unity"

    #region "Step 12 :- Create Abstract CustomerBase classes - Not Used"
    /*public abstract class CustomerBase : ICustomer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
        public abstract void Validate();
        public ICustomer Clone()
        {
            return (ICustomer)this.MemberwiseClone();
        }
    }*/
    #endregion "Step 12 :- Create Abstract CustomerBase classes"

    #region "Step 12 - Customer & lead -  Implement Abstract CustomerBase - Not Used"
    /*public class Customer : CustomerBase
    {
        public override void Validate()
        {
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
            if (BillAmount > 0) throw new Exception("Bill is required");
            if (BillDate >= DateTime.Now) throw new Exception("Bill date  is not proper");
        }
    }

    public class Lead : CustomerBase
    {
        public override void Validate()
        {
            if (CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (PhoneNumber.Length == 0) throw new Exception("Phone number is required");
        }
    }*/

    /*static Factory()
    {
        // Other codes are removed for readability purpose
        new CustomerBase(); //through error on design time, cannot create an instance of the abstract class or interface 'CustomerBase'
    }*/
    #endregion "Step 12 - Customer & lead -  Implement Abstract CustomerBase"

    #region "Step 13 - Generic Factory - Not Used"
    /*Factory class its binded with the “Customer” type at this moment. if “Supplier” type, need one more “Create” method.
    So if we have lot of business objects like this we would end up with lot of “Create” method.
    public static class Factory
    {
        public static ICustomer Create(int CustomerType)
        {
            return cont.Resolve<icustomer>(CustomerType.ToString());
        }
        public static Supplier Create(int Supplier)
        {
            return cont.Resolve<isupplier>(Supplier.ToString());
        }
    }*/
    //So rather than binding the “Factory” with a single type how about making it a “GENERIC” class.

    /*public static class Factory<AnyType> //public static class Factory<T>
    {  
        static IUnityContainer cont = null;
        public static AnyType Create(string CustType)
        {
            if (cont == null)
            {
                cont = new UnityContainer();
                cont.RegisterType<ICustomer, Lead>("Lead");
                cont.RegisterType<ICustomer, Customer>("Customer");
            }
            return cont.Resolve<AnyType>(CustType);
        }
    }*/
    #endregion "Step 13 - Generic Factory"

    #region "Step 13 - Change UI Project - User Generic Type"
    public class FrmCustomer
     {
         private ICustomer cust = null;

         private void cmbCustomerType_SelectedIndexChanged(string cmbCustomerType)
         {
             cust = Factory< ICustomer>.Create(cmbCustomerType);
         }

         private void btnValidate_Click(string cmbCustomerType)
         {
            cust.CustomerName = "txtCustomerName.Text";
            cust.PhoneNumber = "txtPhoneNumber.Text";
            cust.Address = "txtAddress.Text";
            if (cmbCustomerType == "Customer")
            {
                cust.BillAmount = Convert.ToDecimal("txtBillAmount.Text");
                cust.BillDate = Convert.ToDateTime("txtBillDate.Text");
            }
            cust.Validate();
         }
    }
    #endregion "Step 13 - Change UI Project - User Generic Type"

    #region "Step 14 - Stratergy Interface Created. Strategy Pattern (which helps to select algorithms on runtime) for validation - [Design_Pattern]"
    //Later add new validations, expect the system to be flexible  or I will rather say DYNAMIC to achieve the same.
    //At this moment the entity classes are tied up with the validation algorithm.
    public interface IValidationStratergy<T>
    {
        void Validate(T obj);
    }
    #endregion "Step 14 - Strategy Pattern (which helps to select algorithms on runtime) for validation - [Design_Pattern]"
    
    #region "Step 14 - New Validation Interface Created."
    public class CustomerValidation : IValidationStratergy<ICustomer> {
        public void Validate(ICustomer c) {
            if (c.CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (c.PhoneNumber.Length == 0) throw new Exception("Phone number is required");
            if (c.Address.Length == 0) throw new Exception("Address is required");
            if (c.BillAmount > 0) throw new Exception("Bill is required");
        }
    }

    public class LeadValidation : IValidationStratergy<ICustomer> {
        public void Validate(ICustomer c) {
            if (c.CustomerName.Length == 0) throw new Exception("Customer Name is required");
            if (c.PhoneNumber.Length == 0) throw new Exception("Phone number is required");
            if (c.Address.Length == 0) throw new Exception("Address is required");
        }
    }
    #endregion "Step 14 - New Validation Interface Created."

    #region "Step 14 - Factory - Create any entity and inject any validation in to it"
    public static class Factory<AnyType> //public static class Factory<T>
    {
        static IUnityContainer cont = null;
        public static AnyType Create(string CustType)
        {
            if (cont == null)
            {
                cont = new UnityContainer();
                cont.RegisterType<ICustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));
                cont.RegisterType<ICustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidation()));
            }
            return cont.Resolve<AnyType>(CustType);
        }
    }
    #endregion "Step 14 - Create any entity and inject any validation in to it"

    #region "Step 14 :- CustomerBase - Implement IValidationStratergy"
    public abstract class CustomerBase : ICustomer
    {
        public CustomerBase(IValidationStratergy<ICustomer> _Validate)
        {
            ValidationType = _Validate;
        }
        public IValidationStratergy<ICustomer> ValidationType { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
        public abstract void Validate();
        public ICustomer Clone()
        {
            return (ICustomer)this.MemberwiseClone();
        }
        
    }
    #endregion "Step 14 :- CustomerBase - Implement IValidationStratergy"

    #region "Step 14 - Customer & lead -  Implement IValidationStratergy"
    public class Customer : CustomerBase
    {
        public Customer(IValidationStratergy<ICustomer> _Validate) : base(_Validate){}

        public override void Validate()
        {
            ValidationType.Validate(this);
        }
    }

    public class Lead : CustomerBase
    {
        public Lead(IValidationStratergy<ICustomer> _Validate) : base(_Validate){ }
        public override void Validate()
        {
            ValidationType.Validate(this);
        }
    }
    #endregion "Step 14 - Customer & lead -  Implement IValidationStratergy"
}
