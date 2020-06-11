using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockDemo
{
    class Program
    {
        public static void thread1handle()
        {
            PrintStr("子线程1");
        }
        public static void thread2handle()
        {
            //添加测试
            PrintStr("子线程2");
        }
        static void Main(string[] args)
        {

            ThreadStart threadStart1 = new ThreadStart(thread1handle);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();

            ThreadStart threadStart2 = new ThreadStart(thread2handle);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();

            Console.ReadKey();
        }

        private static readonly object obj = new object();
        public static void PrintStr(string inputstr)
        {
            lock (obj)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(inputstr + "输出：" + i.ToString());
                    Thread.Sleep(200);
                }
            }
        }
    }
}
