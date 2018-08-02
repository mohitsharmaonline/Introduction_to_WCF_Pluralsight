// In this class we will begin writing our WCF service code.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

    class HelloWorldService
    {
    }
}
