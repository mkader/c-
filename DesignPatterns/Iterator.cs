using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public interface Iterator
    {
        bool HasNext();
        object Next();
    }

    public interface Container
    {
        Iterator GetIterator();
    }

    public class NameRepository : Container
    {
        public static string[] names = new string[] { "A", "I", "B", "Z", "E" };
        
        public Iterator GetIterator()
        {
            return new NameIterator();
        }

        public class NameIterator : Iterator
        {
            int index;

            public bool HasNext()
            {
                if (index < names.Length) return true;
                return false;
            }

            public object Next()
            {
                if(this.HasNext()) return names[index++];
                return null;
            }
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            NameRepository nr = new NameRepository();
            //NameRepository.NameIterator ni = new NameRepository.NameIterator();
            for (Iterator i = nr.GetIterator();i.HasNext();)
            {
                Console.WriteLine( i.Next());
            }

            Console.ReadLine();
        }
    }
}
