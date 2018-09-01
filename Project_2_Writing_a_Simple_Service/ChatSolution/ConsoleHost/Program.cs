
using EvalServiceLibrary;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {            
            /*************************************************************************************
             * With this change, it should automatically wire up those endpoints we need.
             * Now we can run host to check. And it works like charm.
             * In browser you can see that list of evals is getting returned as before.
             * jump to rtf document for further info.
             * **********************************************************************************/
            WebServiceHost host = new WebServiceHost(serviceType: typeof(EvalService));
            
            try
            {
                host.Open();
                PrintServiceInfo(host);
                Console.ReadLine();
                host.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);                
                host.Abort();
            }            
        }
                
        static void PrintServiceInfo(ServiceHost host)
        {
            Console.WriteLine("{0} is up and running with these endpoints:", host.Description.ServiceType);
            foreach(ServiceEndpoint se in host.Description.Endpoints)
            {
                Console.WriteLine(se.Address);
            }
        }
    }
}
