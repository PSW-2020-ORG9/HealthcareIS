using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Connections
{
    public class Connection : IConnection
    {
        private readonly string _endpoint;
        private readonly JsonDeserializer _deserializer;
        private readonly RestClient _client;
        public Connection(string url, string endpoint)
        {
            _client = new RestClient(url);
            _endpoint = endpoint;
            if (!_endpoint.EndsWith("/")) _endpoint += "/";
            _deserializer = new JsonDeserializer();
        }

        public T Get<T>(string pathParam = "")
        {
            var request = new RestRequest(_endpoint + pathParam, DataFormat.Json);
            var response = _client.Get(request);
            return _deserializer.Deserialize<T>(response);
        }
        
        public T Post<T>(object obj)
        {
            var request = new RestRequest(_endpoint, DataFormat.Json);
            request.AddJsonBody(obj);
            var response = _client.Post(request);
            return _deserializer.Deserialize<T>(response);
        }
    }
}
