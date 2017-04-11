using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{

    namespace LoadExample
    {
        // Loan event argument holds Loan info
        public class LoanEventArgs : EventArgs
        {
            internal Loan Loan { get; set; }
        }

        /// <summary>
        /// The 'Handler' abstract class
        /// </summary>
        abstract class Approver
        {
            // Loan event 
            public EventHandler<LoanEventArgs> Loan;

            // Loan event handler
            public abstract void LoanHandler(object sender, LoanEventArgs e);

            // Constructor
            public Approver()
            {
                Loan += LoanHandler;
            }

            public void ProcessRequest(Loan loan)
            {
                OnLoan(new LoanEventArgs { Loan = loan });
            }

            // Invoke the Loan event
            public virtual void OnLoan(LoanEventArgs e)
            {
                if (Loan != null)
                {
                    Loan(this, e);
                }
            }

            // Sets or gets the next approver
            public Approver Successor { get; set; }
        }

        /// <summary>
        /// The 'ConcreteHandler' class
        /// </summary>
        class Clerk : Approver
        {
            public override void LoanHandler(object sender, LoanEventArgs e)
            {
                if (e.Loan.Amount < 25000.0)
                {
                    Console.WriteLine("{0} approved request# {1}",
                    this.GetType().Name, e.Loan.Number);
                }
                else if (Successor != null)
                {
                    Successor.LoanHandler(this, e);
                }
            }
        }

        /// <summary>
        /// The 'ConcreteHandler' class
        /// </summary>
        class AssistantManager : Approver
        {
            public override void LoanHandler(object sender, LoanEventArgs e)
            {
                if (e.Loan.Amount < 45000.0)
                {
                    Console.WriteLine("{0} approved request# {1}",
                    this.GetType().Name, e.Loan.Number);
                }
                else if (Successor != null)
                {
                    Successor.LoanHandler(this, e);
                }
            }
        }

         class Manager : Approver
        {
            public override void LoanHandler(object sender, LoanEventArgs e)
            {
                if (e.Loan.Amount < 100000.0)
                {
                    Console.WriteLine("{0} approved request# {1}",
                    sender.GetType().Name, e.Loan.Number);
                }
                else if (Successor != null)
                {
                    Successor.LoanHandler(this, e);
                }
                else
                {
                    Console.WriteLine(
                    "Request# {0} requires an executive meeting!",
                    e.Loan.Number);
                }
            }
        }

        class Loan
        {
            public double Amount { get; set; }
            public string Purpose { get; set; }
            public int Number { get; set; }
        }

        public static class Client
        {
            public static void Demo()
            {
                // Setup Chain of Responsibility
                Approver rohit = new Clerk();
                Approver rahul = new AssistantManager();
                Approver manoj = new Manager();

                rohit.Successor = rahul;
                rahul.Successor = manoj;

                // Generate and process loan requests
                var loan = new Loan { Number = 2034, Amount = 24000.00, Purpose = "Laptop Loan" };
                rohit.ProcessRequest(loan);

                loan = new Loan { Number = 2035, Amount = 42000.10, Purpose = "Bike Loan" };
                rohit.ProcessRequest(loan);

                loan = new Loan { Number = 2036, Amount = 156200.00, Purpose = "House Loan" };
                rohit.ProcessRequest(loan);

                // Wait for user
                Console.ReadKey();
            }
        }
    }

    public abstract class AbstractLogger
    {
        public static int INFO = 1;
        public static int DEBUG = 2;
        public static int ERROR = 3;

        protected int level;
        
        //next element in chain or responsibility
        protected AbstractLogger NextLogger;
        public void SetNextLogger(AbstractLogger NextLogger)
        {
            this.NextLogger = NextLogger;
        }

        public void LogMessage(int level, string msg)
        {
            if (this.level <= level) Write(msg);
            if (NextLogger != null) NextLogger.LogMessage(level, msg);
        }

        abstract protected void Write(string msg);
    }

    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(int level)
        {
            this.level = level;
        }
        protected override void Write(string msg)
        {
            Console.WriteLine("Standard Console::Logger: " + msg);
        }
    }

    public class ErrorLogger : AbstractLogger
    {
        public ErrorLogger(int level)
        {
            this.level = level;
        }
        protected override void Write(string msg)
        {
            Console.WriteLine("Error Console::Logger: " + msg);
        }
    }

    public class FileLogger : AbstractLogger
    {
        public FileLogger(int level)
        {
            this.level = level;
        }
        protected override void Write(string msg)
        {
            Console.WriteLine("File::Logger: " + msg);
        }
    }

    public static class Client
    {
        private static AbstractLogger GetChainOfLoggers()
        {
            AbstractLogger error = new ErrorLogger(AbstractLogger.ERROR);
            AbstractLogger debug = new FileLogger(AbstractLogger.DEBUG);
            AbstractLogger info = new ConsoleLogger(AbstractLogger.INFO);
            
            error.SetNextLogger(debug);
            debug.SetNextLogger(info);

            return error;
        }

        public static void Demo()
        {
            AbstractLogger lc = GetChainOfLoggers();
            lc.LogMessage(AbstractLogger.INFO, "This is an information");
            lc.LogMessage(AbstractLogger.DEBUG, "This is an debug information");
            lc.LogMessage(AbstractLogger.ERROR, "This is an error information");

            Console.ReadLine();
        }
    }
}
