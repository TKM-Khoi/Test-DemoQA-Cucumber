using RestSharp;

namespace Core.Client
{
    public class ApiResponse
    {
        public RestClient _client;
        public RestRequest Request;

        public ApiResponse(RestClient client, RestRequest request)
        {
            _client = client;
            Request = request;
        }

        public ApiResponse AddHeader(string name, string value)
        {
            Request.AddHeader(name, value);
            return this;
        }
      

        public ApiResponse AddAuthorizationHeader(string value)
        {
            return AddHeader("Authorization", value);
        }
        public ApiResponse AddContentType(string contentType = null)
        {
            return AddHeader("Content-Type", contentType ?? ContentType.Json);
        }
        public ApiResponse AddParameter(string name, string value)
        {
            Request.AddParameter(name, value);
            return this;
        }
        public ApiResponse AddBody(object body, string contentType = null)
        {
            Request.AddBody(body, contentType ?? ContentType.Json);
            return this;
        }
        public ApiResponse AddStringBody(string body, string contentType = null)
        {
            Request.AddStringBody(body, contentType ?? ContentType.Json);
            return this;
        }
        public ApiResponse AddJsonBody<T>(T body, string contentType = null) where T : class
        {
            Request.AddJsonBody(body );
            return this;
        }
        public ApiResponse AddXmlBody<T>(T body, string contentType = null) where T : class
        {
            Request.AddXmlBody<T>(body, contentType ?? ContentType.Json);
            return this;
        }


        public RestResponse ExecuteGet()
        {
            return _client.ExecuteGet(Request);
        }
        public async Task<RestResponse> ExecuteGetAsync()
        {
            return await _client.ExecuteGetAsync(Request);
        }
        public RestResponse<T> ExecuteGet<T>()
        {
            return _client.ExecuteGet<T>(Request);
        }
        public async Task<RestResponse<T>> ExecuteGetAsync<T>()
        {
            return await _client.ExecuteGetAsync<T>(Request);
        }

        public RestResponse ExecutePost()
        {
            return _client.ExecutePost(Request);
        }
        public RestResponse<T> ExecutePost<T>()
        {
            return _client.ExecutePost<T>(Request);
        }
        public async Task<RestResponse> ExecutePostAsync()
        {
            return await _client.ExecutePostAsync(Request);
        }
        public async Task<RestResponse<T>> ExecutePostAsync<T>()
        {
            return await _client.ExecutePostAsync<T>(Request);
        }
        public RestResponse ExecutePut()
        {
            return _client.ExecutePut(Request);
        }
        public async Task<RestResponse> ExecutePutAsync()
        {
            return await _client.ExecutePutAsync(Request);
        }
        public RestResponse ExecutePatch()
        {
            return _client.ExecutePatch(Request);
        }
        public async Task<RestResponse> ExecutePatchAsync()
        {
            return await _client.ExecutePatchAsync(Request);
        }
        public RestResponse ExecuteDelete()
        {
            return _client.ExecuteDelete(Request);
        }
        public async Task<RestResponse> ExecuteDeleteAsync()
        {
            return await _client.ExecuteDeleteAsync(Request);
        }
    }
}