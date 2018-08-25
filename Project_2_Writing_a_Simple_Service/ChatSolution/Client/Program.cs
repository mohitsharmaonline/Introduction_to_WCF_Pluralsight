using Client.EvalsServiceReference;
using EvalServiceLibrary;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

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
            // Just copy the name of endpoint from app.config to change the endpoint to communicate
            // with service.
            //ChannelFactory<IEvalServiceChannel> cf =
            //    new ChannelFactory<IEvalServiceChannel>("WSHttpBinding_IEvalService");
            //IEvalServiceChannel channel = cf.CreateChannel();


            // So with use of this proxy class we don't have need to deal with channel factories etc.
            // It will take care of building the channel for us underneath.
            // you can check functionality of this new approach by running servicehost and client, it shall be found
            // working same as before.


            // using below message to display user when exactly we are waiting to get stuff from mex.
            Console.WriteLine("Retreiving endpoints via MEX....");

            // run the completed applocation by running ServiceHost and then Client app.

            // So essentially we will be asking via mex all endpoints that implements IEvalService.
            ServiceEndpointCollection endpoints = MetadataResolver.Resolve(contract: typeof(EvalsServiceReference.IEvalService),
                address: new EndpointAddress("http://localhost:8080/evals/mex"));

            // after that executes, it'll download that metadata via the nex protocol, actually using a SOAP invocation,
            // and then in the response menssage it will find that metadata, and then basically it'll build that collection of
            // service endpoints for me automatically. Once i have those service endpoints, then i can baiscally programetically 
            // inspect them and decide which one i want to use. In this example we will just use all of them.

            foreach (ServiceEndpoint se in endpoints)
            {
                // Initialize EvalServiceClient instead.
                EvalServiceClient channel = new EvalServiceClient(binding: se.Binding, remoteAddress: se.Address);

                try
                {
                    Eval eval = new Eval(submitter: "Mohit", comments: "I'm liking this...");

                    channel.SubmitEval(eval);
                    channel.SubmitEval(eval);
                                        
                    List<EvalServiceLibrary.Eval> evals = channel.GetEvals();
                    Console.WriteLine("Number of Evals: {0}", evals.Count);
                    
                    
                    channel.Close();                    
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("FaultException handler: {0}", fe.GetType());
                    channel.Abort();
                }
                catch (CommunicationException ce)
                {
                    Console.WriteLine("CommunicationException handler: {0}", ce.GetType());
                    channel.Abort();
                }
                catch (TimeoutException te)
                {
                    Console.WriteLine("TimeoutException handler: {0}", te.GetType());
                    channel.Abort();
                }
            }
        }




    }
}

