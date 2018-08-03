// In this class we will begin writing our WCF service code.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

// WCF service can be hosted using IIS server. For testing purposes Visual Studio 2008
// provides a default IIS hosting.

namespace HelloWorldService
{
    // defining a new class, we are going to have a servive called Hello, that will take
    // a name as input.
    // To make this class useful in Service contract, we need to annotate it with dataContract attribute.
    [DataContract]
    public class Name
    {
        // for each field we want to expose, user DataMember attribute.
        [DataMember]
        public string First;
        [DataMember]
        public string Last;
    }

    // Now in order to expose this interface as service contract, i need to apply
    // attribue named 'ServiceContract' to it.
    [ServiceContract]
    public interface IHelloWorld
    {
        [OperationContract]
        string SayHello(Name person);
    }

    // Implementation of the service contract.
    public class HelloWorldService : IHelloWorld
    {
        public string SayHello(Name person)
        {
            return string.Format("Hello {0} {1}", person.First, person.Last);
        }
    }
}
