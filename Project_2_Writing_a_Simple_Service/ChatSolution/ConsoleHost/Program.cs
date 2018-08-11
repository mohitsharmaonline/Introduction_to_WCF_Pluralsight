
using EvalServiceLibrary;
using System;
using System.ServiceModel;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Service host initialization.
            ServiceHost host = new ServiceHost(serviceType: typeof(EvalService));

            // adding endpoints to host.
            host.AddServiceEndpoint(implementedContract: typeof(IEvalService),
                binding: new BasicHttpBinding(),
                address: "http://localhost:8080/evals/basic");

            host.AddServiceEndpoint(implementedContract: typeof(IEvalService),
                binding: new WSHttpBinding(),
                address: "http://localhost:8080/evals/ws");

            host.AddServiceEndpoint(implementedContract: typeof(IEvalService),
                binding: new NetTcpBinding(),
                address: "net.tcp://localhost:8081/evals");

            try
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
            catch(Exception ex)
            {

            }

        }
    }
}
