using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp
{
    [TestClass]
    public class Properties_Private_Setters
    {
        [TestMethod]
        public void Main()
        {
            Emplpoyee e = new Emplpoyee();
            //e.LastName = "qwe"; //Error
        }
    }

    // tool to create fast properties from VS type prop[full, g, a]
    
    //Enhanced Properties
    class Emplpoyee
    {
        //private setters
        public string LastName { private set; get; }
        public string Name { set; get; }
        //Validation
        public string FirstName {
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("Name cannot be Empty");
                Name = value;
            }
            get
            {
                return string.IsNullOrWhiteSpace(Name) ? "No Name" : Name;
            }
        }
    }

    //Properties
    class Zoo_Properties
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    //Old Get,Set 
    class Zoo_OldSetGet
    {
        private string _name;
        public void setName(string name)
        {
            _name = name;
        }

        public string getName()
        {
            return _name;
        }
    }

    
}
