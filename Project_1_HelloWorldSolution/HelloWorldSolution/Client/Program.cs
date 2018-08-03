using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.HelloWorldServiceReference;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // initializing the client proxy will initialize necessary channel too.
            HelloWorldClient client = new HelloWorldClient("NetTcpBinding_IHelloWorld");
            Name person = new Name();
            person.First = "Mohit";
            person.Last = "Sharma";

            Console.WriteLine(client.SayHello(person));
            Console.ReadLine();
        }
    }
}
