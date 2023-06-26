using Postex.Application.Domain;
using RestSharp;
using System.Collections.Concurrent;

namespace Postex.Infrastructure.RestClient
{
    class ClientFactory
    {
        private static ConcurrentDictionary<RestClientType, ApiRestClient> _clients = new ConcurrentDictionary<RestClientType, ApiRestClient>();

        static ClientFactory()
        {

        }

        public static ApiRestClient CreateClient(RestClientType type)
        {
            if (_clients.ContainsKey(type))
            {
                var cachedCliet = _clients[type];
                if (cachedCliet != null)
                    return cachedCliet;
            }

            var client = CreateNewClient(type);
            _clients.TryAdd(type, client);
            return client;
        }

        private static ApiRestClient CreateNewClient(RestClientType type)
        {
            //var apiConfig = ApiClientConfig.GetConfig(type);

            var clientOption = new RestClientOptions("")
            {
                MaxTimeout = 10,
            };

            var client = new ApiRestClient(type, clientOption)
            {
                //RestClientConfig = apiConfig
            };

            ConfigureClient(client, null);

            return client;
        }

        private static void ConfigureClient(ApiRestClient client, RestClientConfig apiConfig)
        {
            //if (apiConfig.Headers != null)
            //{
            //    foreach (var item in apiConfig.Headers)
            //    {
            //        client.AddDefaultHeader(item.Key, item.Value);
            //    }
            //}
        }
    }
}
