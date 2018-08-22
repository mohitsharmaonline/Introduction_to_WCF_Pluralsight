using Client.EvalsServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    // Start of Demo: Using Service References module.Refer rtf file named 'Demo_Using_Service_Reference'
    // Start of 'Creating, using and closing channels': So our client now contains an EvalServicereference.
    // Now we need to start using the API on the client side to constrct a channel, 
    //and start sending messages to this service.
    // Start Service if not already, by pressing Ctrl+F5.
    class Program
    {
        static void Main(string[] args)
        {
            // Construction of channel.
            // Take endpoint name from the config file.
            ChannelFactory<IEvalService> cf =
                new ChannelFactory<IEvalService>("NetNamedPipeBinding_IEvalService");

            IEvalService channel = cf.CreateChannel();
            Eval eval = new Eval();
            eval.Submitter = "Mohit";
            eval.TimeSent = DateTime.Now;
            eval.Comments = "I'm liking this...";

            channel.SubmitEval(eval);
            channel.SubmitEval(eval);

            // Notice that in the signature here you can see that it returns and Eval array.
            // Now on the service side, we have used List of Evals, but in the actual schema definition
            // that is part of the WSDL, that's just gonna be represented as a sequence of elements. and so 
            // on the client side , the client code generator has to decide how to map that back into .net code,
            // and by default they choose to use arrays insted of Lists. But we can change that later on.
            Eval[] evals = channel.GetEvals();
            Console.WriteLine("Number of evals: {0}", evals.Length);

            // When we are done using the channel, we need to close it.
            // we csn do it by calling close() on the channel
            // channel.Close(); we won't find this method by default available to us,
            // because i will only see the operations made available on IEvalService,
            // In order to get the operation Close, we need to cast it to 'IClientChannel' interface.
            // Remember that all channels returned by CreateChannel will automatically implement iClientChannel,
            // as well as service contract type.
            ((IClientChannel)channel).Close();
            // Before you run the Client code for testing , set project name 'Client' as startup project,
            // and then press Ctrl+F5 again.
            // for output refer "Demo_Creation_using_closing_channels" rtf document.
        }
    }
}
