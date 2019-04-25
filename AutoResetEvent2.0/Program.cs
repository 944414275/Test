using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoResetEvent2._0
{
    class Program
    {
        //初始的时候是没有信号的，这里的意思是指参数false
        const int numIterations = 100; //重复次数设置多少都无所谓，为让大家看清楚设置了100
        static AutoResetEvent myResetEvent = new AutoResetEvent(false);
        static int number;

        static void Main(string[] args)
        {
            //创建并开始一个线程。
            Thread myReaderThread = new Thread(new ThreadStart(MyReadThreadProc));
            myReaderThread.Name = "ReaderThread";
            myReaderThread.Start();//开启线程，MyReadThreadProc方法可以开始运行

            for (int i = 1; i <= numIterations; i++)
            {
                Console.WriteLine("Writer thread writing value: {0}", i);
                number = i;

                //发信号，说明值已经被写进去了。这里的意思是说Set是一个发信号的方法。
                myResetEvent.Set();

                //让每次循环当中有些间隔,没有其他作用,可以注释掉
                Thread.Sleep(1000);
            }

            //终止阅读线程。

            myReaderThread.Abort();
        }

        static void MyReadThreadProc()
        {
            while (true)
            {
                //在数据被作者写入之前不会被读者读取
                //在上次读取之前至少有一次。
                myResetEvent.WaitOne();
                Console.WriteLine("{0} reading value: {1}", Thread.CurrentThread.Name, number);
            }
        }
    }
}
