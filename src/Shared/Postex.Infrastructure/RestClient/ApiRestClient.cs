using Postex.Application.Domain;
using RestSharp;

namespace Postex.Infrastructure.RestClient
{
    public class ApiRestClient : IApiRestClient
    {
        //private static readonly ILogger _logger = LoggerFactory.CreateStrategyLogger("InquiryLogger");

        private RestSharp.RestClient _restClient { get; set; }
        public RestClientType ClientType { get; private set; }
        public RestClientConfig RestClientConfig { get; set; }

        public ApiRestClient()
        {
            _restClient = new RestSharp.RestClient();
        }

        public ApiRestClient(string baseUrl)
        {
            _restClient = new RestSharp.RestClient(baseUrl);
        }

        public ApiRestClient(RestClientOptions option)
        {
            _restClient = new RestSharp.RestClient(option);
        }

        public ApiRestClient(RestClientType clientType, RestClientOptions option)
        {
            _restClient = new RestSharp.RestClient(option);
            ClientType = clientType;
        }

        public void AddDefaultHeader(string username, string password)
        {
            _restClient.AddDefaultHeader(username, password);
        }

        private Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request, Method method)
        {
            Task<RestResponse<T>> response = null;

            try
            {
                response = _restClient.ExecuteAsync<T>(request, method);
                //_logger.Trace(999005, request, response);
                return response;
            }
            catch (Exception ex)
            {
                //_logger.Error(999006, ex, request, response);
                throw;
            }
        }

        private RestResponse<T> Execute<T>(RestRequest request, Method method)
        {
            return ExecuteAsync<T>(request, method).Result;
        }

        public RestRequest CreateRequest(string service)
        {
            var timeout = 10;
            //if (RestClientConfig.Services != null && RestClientConfig.Services.Count > 0)
            //{
            //    var serviceConfig = RestClientConfig.Services.Where(x => x.Name.Trim().ToUpper() == service.Trim().ToUpper()).FirstOrDefault();
            //    if (serviceConfig != null)
            //        timeout = serviceConfig.TimeOutInMs;
            //}

            return new RestRequest(service)
            {
                Timeout = timeout,
            };
        }

        public RestRequest CreateRequest(string service, object jsonBody)
        {
            var request = CreateRequest(service);

            request.AddBody(jsonBody);

            return request;
        }

        public RestResponse<T> Post<T>(RestRequest request)
        {
            return Execute<T>(request, Method.Post);
        }

        public RestResponse<T> Get<T>(RestRequest request)
        {
            return Execute<T>(request, Method.Get);
        }

        public RestResponse<T> Put<T>(RestRequest request)
        {
            return Execute<T>(request, Method.Put);
        }

        public RestResponse<T> Delete<T>(RestRequest request)
        {
            return Execute<T>(request, Method.Put);
        }

        public Task<RestResponse<T>> PostAsync<T>(RestRequest request)
        {
            return ExecuteAsync<T>(request, Method.Post);
        }

        public Task<RestResponse<T>> GetAsync<T>(RestRequest request)
        {
            return ExecuteAsync<T>(request, Method.Get);
        }

        public Task<RestResponse<T>> PutAsync<T>(RestRequest request)
        {
            return ExecuteAsync<T>(request, Method.Put);
        }

        public Task<RestResponse<T>> DeleteAsync<T>(RestRequest request)
        {
            return ExecuteAsync<T>(request, Method.Delete);
        }
    }
}
