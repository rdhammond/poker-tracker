using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace PokerTracker.WCF
{
    public class EnableCORSBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        private const string VALID_METHODS = "GET,POST,OPTIONS";

        private static readonly Dictionary<string,string> requiredHeaders = new Dictionary<string, string>
        {
            { "Access-Control-Allow-Origin", "*" },
            { "Access-Control-Request-Method", VALID_METHODS },
            { "Access-Control-Allow-Headers", "X-RequestedWith,Content-Type" }
        };

        public override Type BehaviorType
        {
            get { return typeof(EnableCORSBehavior); }
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new HeaderMessageInspector(requiredHeaders));
        }

        public void Validate(ServiceEndpoint endpoint)
        { }

        protected override object CreateBehavior()
        {
            return new EnableCORSBehavior();
        }
    }
}