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

    class EvalService
    {
    }
}
