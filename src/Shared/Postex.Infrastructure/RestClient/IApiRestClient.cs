using Postex.Application.Domain;
using RestSharp;

namespace Postex.Infrastructure.RestClient
{
    internal interface IApiRestClient
    {
        RestClientType ClientType { get; }
        //RestClientConfig RestClientConfig { get; set; }

        void AddDefaultHeader(string username, string password);
        RestRequest CreateRequest(string service);
        RestRequest CreateRequest(string service, object jsonBody);
        RestResponse<T> Delete<T>(RestRequest request);
        Task<RestResponse<T>> DeleteAsync<T>(RestRequest request);
        RestResponse<T> Get<T>(RestRequest request);
        Task<RestResponse<T>> GetAsync<T>(RestRequest request);
        RestResponse<T> Post<T>(RestRequest request);
        Task<RestResponse<T>> PostAsync<T>(RestRequest request);
        RestResponse<T> Put<T>(RestRequest request);
        Task<RestResponse<T>> PutAsync<T>(RestRequest request);
    }
}