using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public interface IImage
    {
        void Display();
    }

    public class RealImage : IImage
    {
        private string FileName;

        public RealImage(string FileName)
        {
            this.FileName = FileName;
            LoadFromDisk(FileName);
        }

        public void Display()
        {
            Console.WriteLine(FileName + " : Display");
        }
        public void LoadFromDisk(string FileName)
        {
            Console.WriteLine(FileName + " : Load Disk");
        }
    }

    public class ProxyImage : IImage
    {
        private RealImage realImage;
        private string fileName;

        public ProxyImage(string FileName)
        {
            fileName = FileName;
        }
        public void Display()
        {
            if (realImage == null)
            {
                realImage = new RealImage(fileName);
            }
            realImage.Display();
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            IImage i = new ProxyImage("test.jpg");
            //image will  be loaded from disk
            i.Display();
            //image will not be loaded from disk
            i.Display();
            Console.ReadLine();
        }
    }
}
