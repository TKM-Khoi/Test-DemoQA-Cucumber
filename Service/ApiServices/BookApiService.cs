using Core.Client;

using RestSharp;

using Service.Const;
using Service.Models.Response;
using Service.Models.Resquests;

namespace Service.ApiServices;

public class BookApiService
{
    private readonly ApiClient _client;

    public BookApiService(ApiClient client)
    {
        _client = client;
    }
    public RestResponse<GetBookListResponse> GetBookList()
    {

        return _client.CreateRequest(ApiEndpointConst.GET_BOOK_LIST_API)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .ExecuteGet<GetBookListResponse>();
    }
    public async Task<RestResponse<AddBookResponse>> AddBookWithTokenAsync(string isbn, string userId, string token)
    {
        var data = new AddBookRequest(userId, isbn);
        return await _client.CreateRequest(ApiEndpointConst.ADD_BOOK_API)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddAuthorizationHeader(token)
            .AddBody(data)
            .ExecutePostAsync<AddBookResponse>();
    }
    public async Task<RestResponse<AddBookResponse>> AddBookWithUnameAndPasswordAsync(string isbn, string userId, string username, string password)
    {
        var data = new AddBookRequest(userId, isbn);
        return await _client
            .SetBasicAuthentication(username, password)
            .CreateRequest(ApiEndpointConst.ADD_BOOK_API)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddBody(data)
            .ExecutePostAsync<AddBookResponse>();
    }
    public RestResponse<AddBookResponse> AddBookWithToken(string isbn, string userId, string token)
    {
        var data = new AddBookRequest(userId, isbn);
        return _client.CreateRequest(ApiEndpointConst.ADD_BOOK_API)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddAuthorizationHeader(token)
            .AddBody(data)
            .ExecutePost<AddBookResponse>();
    }
    public RestResponse<AddBookResponse> AddBookWithUnameAndPassword(string isbn, string userId, string username, string password)
    {
        var data = new AddBookRequest(userId, isbn);
        return _client
            .SetBasicAuthentication(username, password)
            .CreateRequest(ApiEndpointConst.ADD_BOOK_API)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddBody(data)
            .ExecutePost<AddBookResponse>();
    }
    public async Task<RestResponse> DeleteBookWithTokenAsync(string userId, string isbn, string token)
    {
        return await _client
            .CreateRequest(ApiEndpointConst.DELETE_BOOK_APT)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddAuthorizationHeader(token)
            .AddBody(new
            {
                userId = userId,
                isbn = isbn
            })
            .ExecuteDeleteAsync();
    }
    public async Task<RestResponse> DeleteBookWithUnameAndPassAsync(string userId, string isbn, string username, string password)
    {
        return await _client
            .SetBasicAuthentication(username, password)
            .CreateRequest(ApiEndpointConst.DELETE_BOOK_APT)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddBody(new
            {
                userId = userId,
                isbn = isbn
            })
            .ExecuteDeleteAsync();
    }
    public RestResponse DeleteBookWithToken(string userId, string isbn, string token)
    {
        return _client
            .CreateRequest(ApiEndpointConst.DELETE_BOOK_APT)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddAuthorizationHeader(token)
            .AddBody(new
            {
                userId = userId,
                isbn = isbn
            })
            .ExecuteDelete();
    }
    public RestResponse DeleteBookWithUnameAndPass(string userId, string isbn, string username, string password)
    {
        return _client
            .SetBasicAuthentication(username, password)
            .CreateRequest(ApiEndpointConst.DELETE_BOOK_APT)
            .AddHeader("accept", ContentType.Json)
            .AddHeader("Content-Type", ContentType.Json)
            .AddBody(new
            {
                userId = userId,
                isbn = isbn
            })
            .ExecuteDelete();
    }
}