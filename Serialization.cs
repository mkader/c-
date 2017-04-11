using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace CSharp
{
    [TestClass]
    public class Serialization
    {
        [TestMethod]
        public void Main()
        {
            //Binary();
            //Soap();
            //Memory();
            //DCFile();
            XML();
        }

        //Run Time Seralization - Binary
        public void Binary()
        {
            MyObject m = new MyObject();
            m.i = 1123;
            m.d = 2;
            m.name = "Test qeqe";

            IFormatter f = new BinaryFormatter();
            Stream s = new FileStream(@"E:\interview\dot_net\net\c#\test.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            f.Serialize(s, m);
            s.Close();

            s = new FileStream(@"E:\interview\dot_net\net\c#\test.bin", FileMode.Open, FileAccess.Read, FileShare.None);
            MyObject m1 = (MyObject)f.Deserialize(s);
            s.Close();
        }

        /*<SOAP-ENV:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:SOAP-ENC="http://schemas.xmlsoap.org/soap/encoding/" xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/" xmlns:clr="http://schemas.microsoft.com/soap/encoding/clr/1.0" SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">
        <SOAP-ENV:Body>
        <a1:MyObject id = "ref-1" xmlns:a1="http://schemas.microsoft.com/clr/nsassem/CSharp/CSharp%2C%20Version%3D1.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3Dnull">
        <i>1123</i>
        <name id = "ref-3" > Test qeqe</name>
        </a1:MyObject>
        </SOAP-ENV:Body>
        </SOAP-ENV:Envelope>*/

        //Run Time Seralization - Soap
        public void Soap()
        {
            MyObject m = new MyObject();
            m.i = 1123;
            m.d = 2;
            m.name = "Test qeqe";

            IFormatter f = new SoapFormatter();
            Stream s = new FileStream(@"E:\interview\dot_net\net\c#\test.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            f.Serialize(s, m);
            s.Close();

            s = new FileStream(@"E:\interview\dot_net\net\c#\test.xml", FileMode.Open, FileAccess.Read, FileShare.None);
            MyObject m1 = (MyObject)f.Deserialize(s);
            s.Close();
        }
        
        //Data Contract - Memory
        public void Memory()
        {
            DataObject m = new DataObject();
            m.id = 12354;
            m.price = 212.23;
            m.name = "Test qeqe";

            MemoryStream ms = new MemoryStream();
            DataContractSerializer ds = new DataContractSerializer(m.GetType());
            ds.WriteObject(ms, m);
            
            ms.Seek(0, SeekOrigin.Begin);
            Debug.WriteLine(XElement.Parse(Encoding.ASCII.GetString(ms.GetBuffer()).Replace("\0", "")));
            /*< DataObject xmlns = "http://schemas.datacontract.org/2004/07/CSharp" xmlns: i = "http://www.w3.org/2001/XMLSchema-instance" >
              < id > 12354 </ id >
              < name > Test qeqe </ name >
            </ DataObject >*/
            ms.Close();
       }

        //Data Contract - file with OnDeserialized
        public void DCFile()
        {
            DataObject1 mo = new DataObject1("fn", "ln");
            Debug.WriteLine(mo.name);

            DataContractSerializer f = new DataContractSerializer(mo.GetType());
            Stream s = new FileStream(@"E:\interview\dot_net\net\c#\test.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            f.WriteObject(s, mo);
            s.Close();
            
            DataContractSerializer r = new DataContractSerializer(typeof(DataObject1));
            s = new FileStream(@"E:\interview\dot_net\net\c#\test.xml", FileMode.Open, FileAccess.Read, FileShare.None);
            DataObject1 m = (DataObject1)r.ReadObject(s);
            //<DataObject1 xmlns="http://schemas.datacontract.org/2004/07/CSharp" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
            //    <first>fn</first><last>ln</last>
            //</DataObject1>

            Debug.WriteLine(m.first + " " + m.last + " " + m.name);
            s.Close();
        }

        //XML Seralization
        public void XML()
        {
            XMLObject m = new XMLObject();
            m.first = "f";
            m.last = "l";

            XmlSerializer f = new XmlSerializer(m.GetType());
            Stream s = new FileStream(@"E:\interview\dot_net\net\c#\test.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            f.Serialize(s, m);
            s.Close();
            /*
             <?xml version="1.0"?>
            <XMLObject xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" first="f">
                <LastName>l</LastName>
            </XMLObject>
            */
            f = new XmlSerializer(typeof(XMLObject));
            s = new FileStream(@"E:\interview\dot_net\net\c#\test.xml", FileMode.Open, FileAccess.Read, FileShare.None);
            XMLObject m1 = (XMLObject)f.Deserialize(s);

            Console.WriteLine(m1.first + " " + m1.last);
        }
    }

    #region "Version Tolerant Serialization"
    // Version 1 of the Address class.  
    /*[Serializable]
    public class Address
    {
        public string Street;
        public string City;
    }
    
    // Version 2.0 of the Address class.  
    [Serializable]
    public class Address
    {
        public string Street;
        public string City;
        // The older application ignores this data.  
        public string CountryField;
    }*/

    //replace
    [Serializable]
    public class Address
    {
        public string Street;
        public string City;

        //[OptionalField]
        //public string CountryField;

        [OptionalField(VersionAdded = 2)]
        public string CountryField;
    }
    #endregion

    public class XMLObject
    {
        [System.Xml.Serialization.XmlAttribute]
        public string first { get; set; }
        [System.Xml.Serialization.XmlElement(ElementName = "LastName")]  //Controlling XML Serialization Using Attributes
        public string last { get; set; }
    }

    [DataContract]
    class DataObject1
    {
        [DataMember] //tag <first> or  [DataMember(Name="FirstName")] tag <FirstName>
        public string first { get; set; }
        [DataMember]
        public string last { get; set; }
        public string name { get; set; } //not serialized

        // This constructor is not called during deserialization.
        public DataObject1(string first, string last)
        {
            this.first = first;
            this.last = last;
            this.name = first + " " + last;
        }

        // This method is called after the object is completely deserialized. Use it instead of the constructror.
        [OnDeserialized]
        void OnDeserialized(StreamingContext c)
        {
            this.name = this.first + " " + this.last;
        }
    }
    
    [DataContract]
    public class DataObject
    {
        [DataMember]
        public int id { get; set; }
        public double price { get; set; } //not serialized
        [DataMember]
        public string name { get; set; }
    }

    [Serializable] //everthing will be seralized
    public class MyObject
    {
        public int i;
        [NonSerialized]  //not serialized this field
        public double d;
        public string name;
    }
}
