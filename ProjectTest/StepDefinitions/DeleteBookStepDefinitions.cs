using System.Collections.Generic;
using System.Linq;
using System.Net;

using Core.Client;
using Core.Utils;

using FluentAssertions;

using ProjectTest.DataModels;
using ProjectTest.PageObjects;

using Reqnroll;

using RestSharp;

using Service.ApiServices;
using Service.Const;
using Service.Models.DTOs;
using Service.Models.DTOs;
using Service.Models.Response;

namespace ProjectTest.StepDefinitions;

[Binding]
public class DeleteBookStepDefinitions
{
    private LoginPage _loginPage;
    private ProfilePage _profilePage;
    private readonly ScenarioContext _scenarioContext;
    private ApiClient apiClient;
    private BookApiService _bookService;

    public DeleteBookStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _loginPage = new LoginPage();
        _profilePage = new ProfilePage();
        apiClient = new ApiClient(ConfigurationUtils.GetConfigurationByKey("TestUrl"));
        _bookService = new BookApiService(apiClient);
    }

    [Given("the user logs into the application")]
    public void StepTheUserLogsIntoTheApplication()
    {
        var account = _scenarioContext.Get<AccountDto>(ContextKeyConst.ACCOUNT);

        _scenarioContext.Set(account, ContextKeyConst.ACCOUNT);
        _loginPage.NavigateToPage();
        _loginPage.Login(account.Username, account.Password);
    }

    [Given("there is a book named {string} in user book collection")]
    public void StepThereIsABookNamedInUserCollection(string bookName, DataTable table)
    {
        var owner = table.CreateInstance<AccountDto>();
        _scenarioContext.Set(owner, ContextKeyConst.ACCOUNT);
        RestResponse<GetBookListResponse> restResponse = _bookService.GetBookList();
        ICollection<BookDto> bookList = restResponse.Data.Books;
        BookDto book = bookList.FirstOrDefault(b => b.Title.Contains(bookName));
        var addBookResponse = _bookService.AddBookWithUnameAndPassword(book.Isbn, owner.UserId, owner.Username, owner.Password);
        if (addBookResponse.StatusCode == HttpStatusCode.Created)
        {
            _scenarioContext.Set<DeleteBookLaterDto>(new DeleteBookLaterDto{ isbn = book.Isbn, userId = owner.UserId, userToken = "", username = owner.Username, password = owner.Password },
                ContextKeyConst.DELETE_BOOK_LATER);
        }
    }

    [Given("the user is on the Profile page")]
    public void StepTheUserIsOnTheProfilePage()
    {
        //No action needed, user is already on profile page
    }

    [When("the user search book {string}")]
    public void WhenTheUserSearchBook(string bookName)
    {
        _scenarioContext.Set(bookName, ContextKeyConst.DELETE_BOOK_NAME);
        _profilePage.Search(bookName);
    }

    [When("the user clicks on Delete icon")]
    public void StepTheUserClicksOnDeleteIcon()
    {
        string bookName = _scenarioContext.Get<string>(ContextKeyConst.DELETE_BOOK_NAME);
        _profilePage.ClickDeleteBookBtn(bookName);
    }

    [When("the user clicks on OK button")]
    public void StepTheUserClicksOnOKButton()
    {
        _profilePage.ConfirmDelete();
    }
    [When("the user clicks on OK button of alert {string}")]
    public void WhenTheUserClicksOnOKButtonOfAlert(string s)
    {
        _profilePage.CloseAlert();
    }

    [Then("the book is not shown")]
    public void StepTheBookIsNotShown()
    {
        string deletedBookName = _scenarioContext.Get<string>(ContextKeyConst.DELETE_BOOK_NAME);
        IEnumerable<SearchedBookDto> ownedBooks = _profilePage.GetBooks();
        ownedBooks.Should().AllSatisfy(book =>
        {
            book.Image.ToLower().Should().NotContainAll(deletedBookName);
            book.Title.Should().NotContainAll(deletedBookName);
            book.Author.ToLower().Should().NotContainAll(deletedBookName);
            book.Publisher.ToLower().Should().NotContainAll(deletedBookName);
        });
        
        _profilePage.Search(deletedBookName);
        IEnumerable<SearchedBookDto> searchedOwnedBooks = _profilePage.GetBooks();
        var noBookFoundMsg = _profilePage.GetNoDataWarningMsg();
        noBookFoundMsg.Should().NotBeNullOrWhiteSpace();
        searchedOwnedBooks.Should().BeEmpty();
    }
}
