using Client.EvalServiceReference;
using EvalServiceLibrary;
using System;
using System.Collections.Generic;

namespace Client
{    
    /****************************************************************************
     * Each one method we are invoking here is done using SOAP behind the 
     * scene. So, now what we need to do is to figure out how to map this 
     * over to a more restful design.
     * *************************************************************************/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Evaluation Client Application ***\n");

            EvalServiceClient client = new EvalServiceClient("BasicHttpBinding_IEvalService");

            Console.WriteLine("Please enter a command: ");
            string command = Console.ReadLine();

            while (!command.Equals("exit"))
            {
                switch(command)
                {
                    case "submit":

                        Console.WriteLine("Please enter your name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please enter your comments:");
                        string comments = Console.ReadLine();

                        Eval eval = new Eval();
                        eval.TimeSent = DateTime.Now;
                        eval.Submitter = name;
                        eval.Comments = comments;

                        client.SubmitEval(eval);

                        Console.WriteLine("Evaluation submitted! \n");
                        break;

                    case "get":
                        Console.WriteLine("Please enter the eval id:");
                        string id = Console.ReadLine();

                        Eval fe = client.GetEval(id);
                        Console.WriteLine("{0} -- {1} said: {2} (id {3}) \n", fe.TimeSent, fe.Submitter, fe.Comments, fe.Id);
                        break;

                    case "list":
                        Console.WriteLine("Please enter the submitter name:");
                        name = Console.ReadLine();

                        List<Eval> evals = client.GetEvalBySubmitter(name);

                        evals.ForEach(e => Console.WriteLine("{0} -- {1} said: {2} (id {3})", e.TimeSent, e.Submitter, e.Comments, e.Id));
                        Console.WriteLine();
                        break;

                    case "remove":
                        Console.WriteLine("Please enter the eval id:");
                        id = Console.ReadLine();

                        client.RemoveEval(id);

                        Console.WriteLine("Evaluation {0} removed! \n", id);
                        break;

                    default:
                        Console.WriteLine("Unsupported command.");
                        break;
                }

                Console.WriteLine("Please eneter a command: ");
                command = Console.ReadLine();
            }
        }
    }
}

