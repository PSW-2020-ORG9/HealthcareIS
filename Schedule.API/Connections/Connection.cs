using RestSharp;
using RestSharp.Serialization.Json;

namespace Schedule.API.Connections
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

        public T Get<T>()
        {
            return GetAtPath<T>(_endpoint);
        }
        
        public T Get<T>(string pathParam)
        {
            return GetAtPath<T>(_endpoint + pathParam);
        }
        
        public T Post<T>(object obj)
        {
            var request = new RestRequest(_endpoint, DataFormat.Json);
            request.AddJsonBody(obj);
            var response = _client.Post(request);
            return _deserializer.Deserialize<T>(response);
        }

        private T GetAtPath<T>(string path)
        {
            var request = new RestRequest(path, DataFormat.Json);
            var response = _client.Get(request);
            return _deserializer.Deserialize<T>(response);
        }
    }
}
