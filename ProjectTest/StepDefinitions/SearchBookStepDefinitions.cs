using System;
using System.Collections.Generic;

using FluentAssertions;

using ProjectTest.PageObjects;

using Reqnroll;

using Service.Models.DTOs;

namespace ProjectTest.StepDefinitions;

[Binding]
public class SearchBookStepDefinitions
{
    private BookStorePage _bookStorePage;
    private readonly ScenarioContext _scenario;

    public SearchBookStepDefinitions(ScenarioContext scenario)
    {
        _scenario = scenario;
        _bookStorePage = new BookStorePage();
    }

    [Given("there are books named {string} and {string}")]
    public void GivenThereAreBooksNamedAnd(string bookName1, string bookName2)
    {
        //There is no way to create a book into the system on the UI
        //And no api to Create a new book, only an api to add an exsiting book to an user's collection
        //And no way to access the database directly

        //If there is a way, this step will implement those steps, either through api, ui or database 

        //For now, we work with the assumption that those books are already in the system
    }

    [Then("all books match with input criteria will be displayed.")]
    public void StepAllBooksMatchWithInputCriteriaWillBeDisplayed()
    {
        string search = _scenario.Get<string>("search");
        IEnumerable<SearchedBookDto> books = _bookStorePage.GetSearchedBooks();
        books.Should().AllSatisfy(book =>
        {
            (book.Image.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            book.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            book.Author.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            book.Publisher.Contains(search, StringComparison.InvariantCultureIgnoreCase)
            ).Should().BeTrue();
        });
    }

    [When("the user inputs book name {string}")]
    public void StepTheUserInputsBookName(string search)
    {
        _scenario.Set(search, "search");
        _bookStorePage.Search(search);

    }

    [Given("the user is on the Book Store page")]
    public void StepTheUserIsOnTheBookStorePage()
    {
        _bookStorePage.NavigateToPage();
    }


}