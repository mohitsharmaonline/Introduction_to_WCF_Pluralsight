using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


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

    public interface IEvalService
    {
        void SubmitEval(Eval eval);
        List<Eval> GetEvals();
    }

    class EvalService
    {
    }
}
