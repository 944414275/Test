using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTime
{
    class Program
    {
        static AutoResetEvent autoEvent = null;
        static System.Threading.Timer _timer;


        static void Main(string[] args)
        {
            autoEvent = new AutoResetEvent(true);
            _timer = new System.Threading.Timer(new TimerCallback(test), /*null*/autoEvent, 1500, 60000);
            Console.ReadKey();
        }

        static void test(object /*nul*/_autowait)
        {
            try
            {
                AutoResetEvent autoEvent = (AutoResetEvent)_autowait;
                autoEvent.WaitOne();//切断线程
                try
                {
                    if (DateTime.Now.Minute == 50)
                    {
                        Console.WriteLine("整点定时："+DateTime.Now.ToString());
                    }
                    else { }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                finally { autoEvent.Set(); }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
