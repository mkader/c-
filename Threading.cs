using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp
{
    public class Account
    {
        public int Id { get; set; }
        public double Amount { get; set; }

        public void WithDraw(double amt)
        {
            this.Amount -= amt;
        }

        public void Deposit(double amt)
        {
            this.Amount += amt;
        }
    }

    public class AccountManager
    {
        public Account From { get; set; }
        public Account To { get; set; }
        public double Amt { get; set; }

        public void Transfer_Deadlock()
        {
            Debug.WriteLine(Thread.CurrentThread.Name + " trying to acquire lock on " + From.Id.ToString());
            lock (From)
            {
                Debug.WriteLine(Thread.CurrentThread.Name + " acquired lock on " + From.Id.ToString());
                Debug.WriteLine(Thread.CurrentThread.Name + " suspended for 1 second");
                Thread.Sleep(1000);
                Debug.WriteLine(Thread.CurrentThread.Name + " back in action and trying to acquire lock on " + To.Id.ToString());

                lock (To)
                {
                    Debug.WriteLine("Deadlock = Not Reached Here");
                    From.WithDraw(Amt);
                    To.Deposit(Amt);
                }
            }
        }

        //1)Acquiring locks in a specified defined order , the above Transnsfer function is re arranged
        public void Transfer_Deadlock_Resolved()
        {
            Object _a;
            Object _b;
            if (From.Id < To.Id)
            {
                _a = From;
                _b = To;
            }
            else
            {
                _b = From;
                _a = To;
            }
            Debug.WriteLine(Thread.CurrentThread.Name + " trying to acquire lock on " + ((Account)_a).Id.ToString());
            lock (_a)
            {
                Debug.WriteLine(Thread.CurrentThread.Name + " acquired lock on " + ((Account)_a).Id.ToString());
                Debug.WriteLine(Thread.CurrentThread.Name + " suspended for 1 second");
                Thread.Sleep(1000);
                Debug.WriteLine(Thread.CurrentThread.Name + " back in action and trying to acquire lock on " + ((Account)_b).Id.ToString());

                lock (_b)
                {
                    Debug.WriteLine(Thread.CurrentThread.Name + " acquired lock on " + ((Account)_b).Id.ToString());
                    From.WithDraw(Amt);
                    To.Deposit(Amt);
                    Debug.WriteLine(Thread.CurrentThread.Name + " Transfered " + Amt.ToString() + " from " + From.Id.ToString() + " to " + To.Id.ToString());
                }
            }
        }
    }

    [TestClass]
    public class Threading
    {
        int i = 0, j = 0;
        Random r = new Random();
        int total = 0;
        [TestMethod]
        public void Main()
        {
            CrossThreadPreventing();
            //FourProcessorUsing();
            //FourProcessorNotUsing();
            //MultiThread_Performance_Single_VS_Multi_CoreProcessor();
            //MultiThread_DeadLock();
            //MultiThread_InterLocked();
            //MultiThread_ThreadSafe();
            //MultiThread_ThreadNotSafe();
            //Foregroud();
            //Background();
        }

        public void CrossThreadPreventing()
        {
            //2Forms
            //1Form - Contains txtSend
            //2From - Contains txtReceive and btnClose

            //Application Starts - Application.Run(new Form1());

            //public partial class Form1 : Form {
                
                //this code will prevent cross thread                 
                /*private void SendTexttoForm2(string s)
                {
                    if (frm2.txtReceive.InvokeRequired)
                    {
                        frm2.txtReceive.Invoke((MethodInvoker)delegate ()
                        {
                            SendTexttoForm2(s);
                        });
                    }
                    else frm2.txtReceive.Text = s;
                }

                private void CloseForm2()
                {
                    if (frm2.txtReceive.InvokeRequired)
                    {
                        frm2.txtReceive.Invoke((MethodInvoker)delegate ()
                        {
                            CloseForm2();
                        });
                    }
                    else frm2.Close();
                }*/

                /*
                 Form2 frm2 = new Form2();
                public Form1() {
                    InitializeComponent();
                    Thread t = new Thread(new ThreadStart(ShowForm2));
                    t.Start();
                }

                private void ShowForm2() { Application.Run(frm2); }
                private void btnClose_Click(object sender, EventArgs e) { CloseForm2(); }

                private void txtSend_TextChanged(object sender, EventArgs e) { SendTexttoForm2(txtSend.Text); }*/
            //}
        }

        
        public void FourProcessorUsing()
        {
            Parallel.For(0, 1000000, x => RunMillionIterations());
        }

        public void FourProcessorNotUsing()
        {
            Thread t = new Thread(RunMillionIterations);
            t.Start();
            //Debug.Read();
        }

        static void RunMillionIterations()
        {
            string x = "";
            for (int i = 0; i < 1000000; i++)
            {
                x = x + 's';
            }
        }


        public void MultiThread_Performance_Single_VS_Multi_CoreProcessor()
        {
            Debug.WriteLine("Start");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SumOdd();
            SumEven();
            SumOdd();
            SumEven();
            sw.Stop();
            Debug.WriteLine("Without MT " + sw.ElapsedTicks);

            sw = new Stopwatch();
            sw.Start();
            Thread t1 = new Thread(SumOdd);
            t1.Start();
            Thread t2 = new Thread(SumEven);
            t2.Start();
            Thread t3 = new Thread(SumOdd);
            t3.Start();
            Thread t4 = new Thread(SumEven);
            t4.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            Debug.WriteLine("With MT " + sw.ElapsedTicks);
            Debug.WriteLine("End");

            /*
            Start
            Sum Odd = 1642668640
            Sum Even = 820084320
            Sum Odd = 1642668640
            Sum Even = 820084320
            Without MT 367714

            Sum Odd = 1642668640
            Sum Even = 820084320
            Sum Even = 820084320
            Sum Odd = 1642668640
            With MT 268197
            End
            */
        }

        static void SumOdd()
        {
            int s = 0;
            for (int i = 0; i < 5000000; i++)
            {
                if (i % 1 == 0) s += i;
            }
            Debug.WriteLine("Sum Odd = " +s);
        }

        static void SumEven()
        {
            int s = 0;
            for (int i = 0; i < 5000000; i++)
            {
                if (i % 2 == 0) s += i;
            }
            Debug.WriteLine("Sum Even = " +s);
        }
        public void MultiThread_DeadLock()
        {
            Debug.WriteLine("Start");
            Account a = new Account() { Id = 101, Amount = 5000 };

            Account b = new Account() { Id = 201, Amount = 8000 };

            AccountManager ab = new AccountManager() { From = a, To = b, Amt =2000 };
            //Thread t1 = new Thread(ab.Transfer_Deadlock);
            Thread t1 = new Thread(ab.Transfer_Deadlock_Resolved); 
            t1.Name = "T1";
            t1.Start();
            
            AccountManager ba = new AccountManager() { From = b, To = a, Amt = 3000 };
            //Thread t2 = new Thread(ba.Transfer_Deadlock);
            Thread t2 = new Thread(ba.Transfer_Deadlock_Resolved);
            t2.Name = "T2";
            t2.Start();
            

            t1.Join();
            t2.Join();

            Debug.WriteLine("End");

        }

        public void MultiThread_InterLocked()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Option 1
            //it prints always 300000, Elasped Ticks 6130, 5050, 5000
            /*Print();
            Print();
            Print();*/

            //Option 2
            //it prints different number every time like Total 222018 --> 130370, 127501 --> 126786
            //so it is incosistetn for every times

            //Protecting shared resources from concurrent access in multithreading
            //Option 3 - Total 300000-- > 104333, 131732, it's fast compare to lock 
            //Option 4 - Total 300000-- > 181784, 184131, it's slow performance - lock 
            Thread t1 = new Thread(Print);
            t1.Start();
            Thread t2 = new Thread(Print);
            t2.Start();
            Thread t3 = new Thread(Print);
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();

            sw.Stop();
            Debug.WriteLine("Total "+ total + " --> " + sw.ElapsedTicks);
            
        }

        public void Print()
        {
            //Option 1 & Option 2
            //for (int i = 0; i < 100000; i++) total++;

            //Option 3 Protecting shared resources from concurrent access in multithreading
            //for (int i = 0; i < 100000; i++) Interlocked.Increment(ref total);

            //Option 4 Protecting shared resources from concurrent access in multithreading
            for (int i = 0; i < 100000; i++)
            {
                lock (this)
                {
                    total++;
                }
            }
        }

        public void MultiThread_ThreadSafe()
        {
            Thread t = new Thread(Divide_ThreadSafe);
            t.Start(); //Child Thread
            Divide_ThreadSafe(); //Main Thread
        }
        public void Divide_ThreadSafe()
        {
            for (int x = 1; x < 10000000; x++)
            {
                /*//Option 1
                lock (this)
                {
                    Divide();
                }
                //OR - Option 2
                Monitor.Enter(this);
                Divide();
                Monitor.Exit(this);*/

                //OR - Option 3
                try
                {
                    Monitor.Enter(this);
                    try
                    {
                        Divide();
                    }
                    finally
                    {
                        Monitor.Exit(this);
                    }
                }
                catch(SynchronizationLockException ex)
                {

                }
            }
        }

        public void MultiThread_ThreadNotSafe()
        {
            Thread t = new Thread(Divide_ThreadNotSafe);
            t.Start(); //Child Thread
            Divide_ThreadNotSafe(); //Main Thread
        }

        /*
        "Attempted to divide by zero" error message will be throw.
	    Divide_ThreadNotSafe is not thread safe, why the error is comming?
	    Both of thread are calling this divide function in a concurrent manner, it is possible main thread is dividing and 
	    child thread is setting the j value is zero. so you get the exception.
	
	    Solution is, inside the for loop code, at least one time only one thread should execute. 
	    To avoid this situation, using proper thread synchronization, use Lock Lock(this) {} or Monitor.
         */
        public void Divide_ThreadNotSafe()
        {
            for (int x = 1; x < 100000; x++)
            {
                Divide();
            }
        }

        public void Divide()
        {
            i = r.Next(1, 10);
            j = r.Next(1, 10);
            double d = i / j;
            i = 0;
            j = 0;
        }

        public void Background()
        {
            Debug.WriteLine("Start");
            Thread t = new Thread(Print1);
            t.IsBackground = true;
            t.Start();
            Debug.WriteLine("End");
            /*Output
             Start
             End
             Print 1 0 [it will run background]
             */
        }

        //foreground thread example
        public void Foregroud()
        {
            //executing sequentially
            //Print1(); 
            //Print2(); //Print 1 0 ...Print 2 0... Print 2 9

            Debug.WriteLine("Start");
            //creating thread
            Thread t1 = new Thread(Print1);
            Thread t2 = new Thread(Print2);
            //invoke the thread
            t1.Start();
            t2.Start();
            //application quits here
            Debug.WriteLine("End");
            /*output 
             Start 
             Print 2 0
             Print 2 1
             Print 2 0
             End 
             Print 2 2 
             Print 1 1..*/
        }

        public void Print1()
        {
            for (int i = 0; i < 10; i++) Debug.WriteLine("Print 1 " + i);
            Thread.Sleep(5000);
        }

        public void Print2()
        {
            for (int i = 0; i < 10; i++) Debug.WriteLine("Print 2 " + i);
            Thread.Sleep(1000);
        }
    }
}
