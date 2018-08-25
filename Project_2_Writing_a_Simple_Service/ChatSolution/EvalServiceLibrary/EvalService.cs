using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EvalServiceLibrary
{
    // This class will represent Evaluation.  
    // In order to use it within my servicontract, i need to annotate it
    // with 'DataContract'.
    [DataContract]
    public class Eval
    {
        // Start of Demo: Tool-support for reusing types.
        // Now for this example, we have added a few constructor in the service class.
        // and we would like to reuse these in client side too.
        public Eval()  { }
        public Eval(string submitter, string comments)
        {
            this.Submitter = submitter;
            this.Comments = comments;
            this.TimeSent = DateTime.Now;
        }

        // I need to include each member that i use with 'DataMember'
        [DataMember]
        public string Submitter;
        [DataMember]
        public DateTime TimeSent;
        [DataMember]
        public string Comments;
    }

    // In order to make it an official WCF service contract, i need to annotate it with ServiceContract attribute.
    [ServiceContract]
    public interface IEvalService
    {
        // For each method i want to expose , use of OperationContract attribute will be required.
        [OperationContract]
        void SubmitEval(Eval eval);
        [OperationContract]
        List<Eval> GetEvals();
    }

    // At this point i need to decide what instancing mode i will use with my service.
    // In this case i am going to use singleton
    // InstanceContextMode.Single: This means that within a service host there will be a single
    // instance of service running in memory, and it will be used to service all of the incoming
    // messages.
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class EvalService : IEvalService
    {
        // Because there will be only one instance, we can use a list of all Evals here.
        List<Eval> evals = new List<Eval>();

        public List<Eval> GetEvals()
        {
            // dummy code to generate timeout exceptions.
            System.Threading.Thread.Sleep(5000);
            return evals;
        }

        public void SubmitEval(Eval eval)
        {
            // dummy code to generate FaultException.
            if(eval.Submitter.Equals("Throw"))
            {
                throw new FaultException("Error within SubmitEval");
            }
            evals.Add(eval);
        }
    }
}
