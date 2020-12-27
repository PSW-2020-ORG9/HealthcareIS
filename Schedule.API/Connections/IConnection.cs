namespace Schedule.API.Connections
{
    public interface IConnection
    {
        /// <summary>
        /// Sends a GET request to an endpoint.
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized from response.</typeparam>
        /// <returns>JSON Content of the response deserialized to an object.</returns>
        T Get<T>();
        /// <summary>
        /// Sends a POST request to an endpoint.
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized from response.</typeparam>
        /// <param name="obj">Object to be serialized as JSON</param>
        /// <returns>JSON Content of the response deserialized to an object.</returns>
        T Post<T>(object obj);

        /// <summary>
        /// Sends a GET request to an endpoint, with the given path parameter.
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized from response.</typeparam>
        /// <param name="pathParam">Path parameter to be appended to the url of this connection. </param>
        /// <returns>JSON Content of the response deserialized to an object.</returns>
        public T Get<T>(string pathParam);

    }
}
