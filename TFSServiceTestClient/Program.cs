using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSServiceTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service");

            var client = new TFSServiceClient();
            client.OnStart(args);

            Console.WriteLine("Service started...");
            Console.WriteLine("Press escape to stop.");
            var go = true;
            while (go)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    go = false;
                }
            }
            Console.WriteLine("Stopping Service");
            client.OnStop();
        }
    }
}
