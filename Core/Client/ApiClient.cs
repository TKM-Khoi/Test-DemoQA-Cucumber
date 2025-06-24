using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Serializers.NewtonsoftJson;

namespace Core.Client
{
    public class ApiClient
    {
        private readonly RestClient _client;
        private string _baseUrl = string.Empty;
        public ApiClient(RestClient client)
        {
            _client = client;
        }
        public ApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            var opts = new RestClientOptions(baseUrl);
            _client = new RestClient(opts, configureSerialization: cs=>cs.UseNewtonsoftJson());
        }
        private ApiClient(RestClientOptions opts)
        {
            _client = new RestClient(opts, configureSerialization: cs=>cs.UseNewtonsoftJson());
        }
        public ApiClient SetBasicAuthentication(string username, string password)
        {
            var options = new RestClientOptions(_baseUrl);
            options.Authenticator = new HttpBasicAuthenticator(username, password);
            return new ApiClient(options);
        }
        public ApiClient SetRequestTokenAuthentication(string comsumerKey, string consumerSecret)
        {
            var options = new RestClientOptions(_baseUrl);
            options.Authenticator = OAuth1Authenticator.ForRequestToken(comsumerKey, consumerSecret);
            return new ApiClient(options);
        }
        public ApiClient SetAccessTokenAuthentication(string comsumerKey, string consumerSecret, string oauthToken, string oauthSecret)
        {
            var options = new RestClientOptions(_baseUrl);
            options.Authenticator = OAuth1Authenticator.ForAccessToken(comsumerKey, consumerSecret, oauthToken, oauthSecret);
            return new ApiClient(options);
        }
        public ApiClient SetRequestHeaderAuthentication(string token, string authType = "Bearer")
        {
            var options = new RestClientOptions(_baseUrl);
            options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, authType);
            return new ApiClient(options);
        }
        public ApiClient SetJwtAuthentication(string jwt)
        {
            var options = new RestClientOptions(_baseUrl);
            options.Authenticator = new JwtAuthenticator(jwt);
            return new ApiClient(options);
        }
        public ApiClient ClearAuthenticator()
        {
            var opts = new RestClientOptions(_baseUrl);
            return new ApiClient(opts);
        }


        public ApiClient AddDefaultHeaders(Dictionary<string, string> headers)
        {
            _client.AddDefaultHeaders(headers);
            return this;
        }
        public ApiResponse CreateRequest(string source = "")
        {
            return new ApiResponse(_client, new RestRequest(source));
        }
    }
}