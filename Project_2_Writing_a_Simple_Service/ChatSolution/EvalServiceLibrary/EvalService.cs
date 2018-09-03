using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

namespace EvalServiceLibrary
{
    // This class will represent Evaluation.  
    // In order to use it within my servicontract, i need to annotate it
    // with 'DataContract'.
    [DataContract(Namespace ="http://pluralsight.com/evals")]
    public class Eval
    {
        // Start of Demo: Tool-support for reusing types.
        // Now for this example, we have added a few constructor in the service class.
        // and we would like to reuse these in client side too.
        // Right now, as we have changes the Service only, client won't be having new constructors on his side.
        // So we right now have old version of Eval type in client.
        // refer rtf file Demo_tool_support_for_reusing_types for further information.
        public Eval()  { }
        public Eval(string submitter, string comments)
        {
            this.Submitter = submitter;
            this.Comments = comments;
            this.TimeSent = DateTime.Now;
        }

        // I need to include each member that i use with 'DataMember'
        [DataMember]
        public string Id;
        [DataMember]
        public string Submitter;
        [DataMember]
        public DateTime TimeSent;
        [DataMember]
        public string Comments;        
    }

    /*********************************************************************************
     * First we need to define a URI Mapping for each of these contracts
     * *****************************************************************************/

    // In order to make it an official WCF service contract, i need to annotate it with ServiceContract attribute.
    [ServiceContract]
    public interface IEvalService
    {
        /*****************************************************************************************************
         * These are going to be invoked using a GET request, so i am going to use the WebInvokeAttribute.
         * We have mapped first one to a POST request.
         * **************************************************************************************************/
        [WebInvoke(Method ="POST", UriTemplate ="evals")]
        [OperationContract]
        void SubmitEval(Eval eval);

        /******************************************************************************************************
         * We have mapped input parameter id within our Uri template
         * this template name with extend out base address already defined for service.
         * We have added WebGet to all of these guys to make them invokable using traditional
         * HTTP GET requests. The URI is going to determine which one is going to be called
         * for a particular GET Request.
         * ***************************************************************************************************/
        [WebGet(UriTemplate="eval/{id}")]
        [OperationContract]
        Eval GetEval(string id);

        [WebGet(UriTemplate="evals", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Eval> GetAllEvals();

        [WebGet(UriTemplate = "evals/{submitter}")]
        [OperationContract]
        List<Eval> GetEvalBySubmitter(string submitter);

        /******************************************************************************************************
         * Mapped this one to a DELETE request.
         * ***************************************************************************************************/
        [WebInvoke(Method = "DELETE", UriTemplate = "eval/{id}")]
        [OperationContract]
        void RemoveEval(string id);

        /****************************************************************************************************
         * So basically we have exposed same services as before through the standard HTTP uniform service
         * Contracts. using GET, POST, DELETE in this case.
         * ************************************************************************************************/

        // adding a new operation to service contract to return a syndicationfeedformatter.
        [ServiceKnownType(typeof(Atom10FeedFormatter))]
        [ServiceKnownType(typeof(Rss20FeedFormatter))]
        [WebGet(UriTemplate="evals/feed/{format}")]
        [OperationContract]
        SyndicationFeedFormatter GetFeed(string format);
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
        int evalCount = 0;

        public void SubmitEval(Eval eval)
        {
            eval.Id = (++evalCount).ToString();
            evals.Add(eval);
        }

        public Eval GetEval(string id)
        {
            return evals.First(e => e.Id.Equals(id));
        }

        public List<Eval> GetAllEvals()
        {
            return this.GetEvalBySubmitter(null);
        }        

        public List<Eval> GetEvalBySubmitter(string submitter)
        {
            if(submitter == null || submitter.Equals(""))
            {
                return evals;
            }
            else
            {
                return evals.Where(e => e.Submitter.ToLower().Equals(submitter.ToLower())).ToList<Eval>();
            }
        }
        
        public void RemoveEval(string id)
        {
            evals.RemoveAll(e => e.Id.Equals(id));
        }


        public SyndicationFeedFormatter GetFeed(string format)
        {
            List<Eval> evals = this.GetAllEvals();
            SyndicationFeed feed = new SyndicationFeed()
            {
                Title = new TextSyndicationContent("Pluralsight Evaluations summary"),
                Description = new TextSyndicationContent("Recent student evaluation summary"),
                Items = from eval in evals
                        select new SyndicationItem()
                        {
                            Title = new TextSyndicationContent(eval.Submitter),
                            Content = new TextSyndicationContent(eval.Comments)
                        }
            };

            if(format.Equals("atom"))
            {
                return new Atom10FeedFormatter(feed);
            }
            else
            {
                return new Rss20FeedFormatter(feed);
            }

            // Move to rtf doc Demo_Publishin_Atom_Rss_Feeds for more info.
        }
    }
}
