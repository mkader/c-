using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp
{
    [TestClass]
    public class PartialTypes
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    //Partial Types
    //- allow us to split a class or struct, an interface or a method into 2 or more files. 
	//  All these parts are then combines into a single class, when the application is compiled.
	
    //Partial Classes
    //---------------
	/*- Example. Create a exe or web form in VS, it creates 2 file, Form1.cs (developer code), 
	  Form1.designer.cs (system generated code).
	
	- PartialClass1.cs -> class PartialClass   { } 	
	  PartialClass2.cs -> class PartialClass   { } , 
	  compile ERROR, already contain a definition of 'PartialClass'
	
	- PartialClass1.cs -> partial class PartialClass   { } 	
	  PartialClass2.cs -> class PartialClass   { } 
	  compile ERROR, missing partial modifier or definition of type 'PartialClass'	
	  [must use the partial keyword all the files]
	  
	- PartialClass1.cs -> public partial class PartialClass   { } 	
	  PartialClass2.cs -> internal partial class PartialClass   { } 
	  compile ERROR, parital declarations of 'PartialClass' have conflicting different modifiers	
	  [must use the same access modifiers all the files]
	
	- PartialClass1.cs -> public abstract[sealed] partial class PartialClass   { } 	
	  PartialClass2.cs -> partial class PartialClass   { } 
	  Program.cs -> PartialClass pc = new PartialClass(); //throws error, 
	  	[because PartialClass2.cs class considered abstract or sealed or inherit class, see example 2,  3, 4]
	  	
	- PartialClass1.cs -> partial class PartialClass :Customer  { } 	
	  PartialClass2.cs -> partial class PartialClass :Emplyee { } 
	  class Customer {} , class Employee {}
	  [Compile ERROR, does not support multiple inheritance]
	  
	- PartialClass1.cs -> partial class PartialClass :ICustomer  { } 	
	  PartialClass2.cs -> partial class PartialClass :IEmployee { } 
	  interface ICustomer {  void CustomerMethod();} , interface IEmployee {  void EmployeeMethod();}
	  [Compile ERROR, all interface mehtods must declare any one class, see example 5]
	  
	*/
    
    #region "Example 1"
    partial class PartialClass1
    {
        public string GetName() { return FirstName + " " + LastName; }
    }

	//PartialClass2.cs
	partial class PartialClass1
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
	
	//PartialClass1 pc = new PartialClass1();
	//pc.FirstName = "first";
	//pc.LastName = "last";
	//Console.WriteLine(pc.GetName());
    #endregion "Example 1"

    #region "Example 2 parital abstract"
    public abstract partial class PartialClass2
    {
        public string GetName;
    }
    partial class PartialClass2
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class Sample1 : PartialClass2
    {
        public string GetName()
        {
            return FirstName + " " + LastName;
        }
    }
    //Sample1 s = new Sample1(); 
    //s.FirstName = "first"; 
    //s.LastName = "last"; 
    //s.GetName();}
    #endregion "Example 2 parital abstract"

    public class Sample
    {
        public string GetFullName()
        {
            return "Testting";
        }
    }

    #region "Example 3 parital sealed"
    public sealed partial class PartialClass3 :Sample
    {
        public string GetName() { return FirstName + " " + LastName; }
    }

    partial class PartialClass3
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
	/*PartialClass3 pc3 = new PartialClass3();
    s.FirstName = "first"; 
    s.LastName = "last"; 
    s.GetName(); 
    s.GetFullName();}*/
    #endregion "Example 3 parital sealed"

    #region "Example 4 parital inherit class"
    public partial class PartialClass4 :Sample
    {
    }
	partial class PartialClass4
    {

    }
	
    //PartialClass4 pc4 = new PartialClass4();
    //pc4.GetFullName();
    #endregion "Example 4 parital inherit class"

    #region "Example 5 parital interface class"
        partial class PartialClass5 : ICustomer
        {
            public void EMethod() { }
            public void CMethod() { }
        }

        partial class PartialClass5 : IEmployee
        {
      
        }

        interface ICustomer
        {
            void CMethod();
        }
        interface IEmployee
        {
            void EMethod();
        }
        #endregion "Example 5 parital interface class"

    //Partial Methods
    //----------------
    /*- it contains 2 part, one is the definition (declaration or signature) part (any class) 
	  and the other one is the implementation (any class).
	- if method only has definition & no implementation (no compiler error), if method calls, 
	  compiler will ignore the definition and also ignor the call part also.
	  public partial class PartialClass {
	          partial void Display(); //compiler will ignore, no implementation
	  
	          public void Print()
	          {
	              Display(); //call ignore
	          }
    	  }
	- if method only parital implementation & no definition (compiler error), it is non-parital method
	  partial void Display() { }
	  
	- partial method cannot have access modifiers or the virtual, abstract, override, new, sealed or extern modifiers 		
	  public partial void Display(); //error, can't be public modifiers
	- partial method return type must be void, other wise comiple error.
	- arguments must be same both places.
	- partial method method must be declared within a partial class or struct
  	  public  class PartialClass {partial void Display(); }	 //error
	- multiple implementation (2 class same method ) but only one declaration, //compile error.
	*/
	
	//PartialClass1.cs
	public partial class PartialClass
	{
	    partial void Display(int i);
	
	    public void Print()
	    {
	        Console.WriteLine("Print");
	        Display(10);
	    }
    }
         
    //PartialClass2.cs
     partial class PartialClass
	 {
	        partial void Display(int i)
	        {
	            Console.WriteLine("Display " + i);
	        }
    }
    	
    //PartialClass pc = new PartialClass();
	//pc.Print(); //Print Display
	
	//if  "partial void Display(){Console.WriteLine("Display");}" in PartialClass1.cs, it will give output 

}
