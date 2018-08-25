using Client.EvalsServiceReference;
using EvalServiceLibrary;
using System;
using System.Collections.Generic;
using System.ServiceModel;

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

            // Initialize EvalServiceClient instead.
            EvalServiceClient channel = new EvalServiceClient(endpointConfigurationName: "WSHttpBinding_IEvalService");
            // So with use of this proxy class we don't have need to deal with channel factories etc.
            // It will take care of building the channel for us underneath.
            // you can check functionality of this new approach by running servicehost and client, it shall be found
            // working same as before.
            try
            {

                // Run the service by selecting ConsoleHost as Startup project and then Ctrl+F5
                // Now we can use the new constructor on the client side.
                Eval eval = new Eval(submitter: "Mohit", comments: "I'm liking this...");
                // Now we can run our serviceHost and Client to see that everything works as before.
                // Chane submitter to generate faultException
                // Change to normal name.
                // Run Client by selecting it as startup project and ctrl+F5

                channel.SubmitEval(eval);
                channel.SubmitEval(eval);

                // Notice that in the signature here you can see that it returns and Eval array.
                // Now on the service side, we have used List of Evals, but in the actual schema definition
                // that is part of the WSDL, that's just gonna be represented as a sequence of elements. and so 
                // on the client side , the client code generator has to decide how to map that back into .net code,
                // and by default they choose to use arrays insted of Lists. But we can change that later on.
                // Now one problem point was this array of evals instead of List of Evals at our service side
                // implementation. We can fix it by right licking on EvalsServiceReference and selecting 
                // "Configure Service reference" option. refer "Demo_Creation_using_closing_Channels"
                // this will just begin the asynchronous operation and hence won't be returning anything.
                channel.GetEvalsCompleted += Channel_GetEvalsCompleted;
                channel.GetEvalsAsync();
                Console.WriteLine("Waiting...");
                // this readline will prevent console window from getting prematurely closed.
                Console.ReadLine();
                // Now pressing Ctrl+F5 after starting service, we can test it's working.
                
                // run it t ensure that changes worked!

                // When we are done using the channel, we need to close it.
                // we csn do it by calling close() on the channel
                // channel.Close(); we won't find this method by default available to us,
                // because i will only see the operations made available on IEvalService,
                // In order to get the operation Close, we need to cast it to 'IClientChannel' interface.
                // Remember that all channels returned by CreateChannel will automatically implement iClientChannel,
                // as well as service contract type.
                // This is another problem area to cast channel to IClientChannel every time we had to use
                // Close or Abort operations. We talked earlier about special IClintChannel derived
                // interface to solve this problem. refer "Demo_Creation_using_closing_Channels"
                // convert close to abort and you will see that it immidiately shuts down on pressing enter.
                channel.Abort();
                // Before you run the Client code for testing , set project name 'Client' as startup project,
                // and then press Ctrl+F5 again.
                // for output refer "Demo_Creation_using_closing_channels" rtf document.
            }
            catch(FaultException fe)
            {
                Console.WriteLine("FaultException handler: {0}", fe.GetType());
                channel.Abort();
            }
            catch(CommunicationException ce)
            {
                Console.WriteLine("CommunicationException handler: {0}", ce.GetType());
                channel.Abort();
            }
            catch(TimeoutException te)
            {
                Console.WriteLine("TimeoutException handler: {0}", te.GetType());
                channel.Abort();
            }
        }

        private static void Channel_GetEvalsCompleted(object sender, GetEvalsCompletedEventArgs e)
        {
            // I can harvest the result here.
            Console.WriteLine("Number of evals: {0}", e.Result.Count);
        }
    }
}
