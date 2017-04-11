using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    //command
    public interface IOrder
    {
        void Execute();
    }

    //Receiver
    public class Stock
    {
        private string name = "GLE";
        private int quantity = 10;

        public void Buy()
        {
            Console.WriteLine("Stock Bought " + name + " : " + quantity);
        }

        public void Sell()
        {
            Console.WriteLine("Stock Sold " + name + " : " + quantity);
        }
    }

    public class BuyStock : IOrder
    {
        public Stock Stock { get; set; }
        
        public void Execute()
        {
            Stock.Buy();
        }
    }

    public class SellStock : IOrder
    {
        public Stock Stock { get; set; }

        public void Execute()
        {
            Stock.Sell();
        }
    }

    //Invoker
    public class Broker
    {
        private List<IOrder> orders = new List<IOrder>();

        public void TakeOrder(IOrder s)
        {
            orders.Add(s);
        }

        public void PlaceOrder()
        {
            foreach(var o in orders)
            {
                o.Execute();
            }
            orders.Clear();

        }
            
    }

    public class Client
    {
        public static void Demo()
        {
            Stock s = new Stock();
            IOrder bs = new BuyStock() { Stock= s};
            IOrder ss = new SellStock() { Stock = s };

            Broker b = new Broker();
            b.TakeOrder(bs);
            b.TakeOrder(ss);
            b.PlaceOrder();
            Console.ReadLine();
        }
    }
}
