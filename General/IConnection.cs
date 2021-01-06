using System;

namespace General
{
    public interface IConnection
    {
        /// <summary>
        /// Sends a GET request to an endpoint.
        /// </summary>
        /// <param name="pathParam">Optional path parameters to be included in the url.</param>
        /// <typeparam name="T">Type of object to be deserialized from response.</typeparam>
        /// <returns>Content of the response deserialized to an object of type <typeparamref name="T"/>.</returns>
        T Get<T>(string pathParam = "") where T : class;
        /// <summary>
        /// Sends a POST request to an endpoint.
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized from response.</typeparam>
        /// <param name="obj">Object to be serialized as JSON</param>
        /// <returns>Content of the response deserialized to an object of type <typeparamref name="T"/>.</returns>
        T Post<T>(object obj) where T : class;
    }
}
