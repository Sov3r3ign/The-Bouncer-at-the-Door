using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingPatternEx5
{
    class Program
    {
        static List<int> sharedList = new List<int>();
        static volatile bool stop = false;
        static object listLock = new object();
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Thread tEven = new Thread(Even);
            Thread tOdd = new Thread(Odd);
            tEven.Start();
            tOdd.Start();

            for(int i=0; i<10; i++)
            {
                Console.WriteLine($" \nList {sharedList.Count} items.\n");
                Thread.Sleep(1000);
            }
            stop = true;
            tEven.Join();
            tOdd.Join();
            Console.WriteLine("\nFINISHED");
            Console.ReadLine();
        }
        public  static void Even()
        {

            while(!stop)
            {
                
                lock(listLock)
                {
                    int numb = rnd.Next(100);

                    if (numb % 2 ==0)
                    {
                        sharedList.Add(numb);
                        Console.WriteLine("Even number {0} is added", numb);
                    }
                    else
                    {
                        Console.WriteLine($"{numb} Not an even number ");
                    }
                        
                }
                Thread.Sleep(500);
            }
        }
        public static void Odd()
        {
            while (!stop)
            {
                
                lock (listLock)
                {
                    int numb = rnd.Next(100);
                    if (numb % 2 != 0)
                    {
                        sharedList.Add(numb);
                        Console.WriteLine("Odd number {0} is added", numb);
                    }
                    else
                    {
                        Console.WriteLine($"{numb} Not an odd number");
                    }
                        
                }
                Thread.Sleep(500);
            }
        }
    }
}
