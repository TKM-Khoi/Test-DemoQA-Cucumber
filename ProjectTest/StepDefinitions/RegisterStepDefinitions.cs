using FluentAssertions;

using ProjectTest.DataModels;
using ProjectTest.PageObjects;

using Reqnroll;

namespace ProjectTest.StepDefinitions;

[Binding]
public class RegisterStepDefinitions
{
    private readonly RegisterPage _registerPage;
    private readonly ScenarioContext _scenarioContext;
    public RegisterStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _registerPage = new RegisterPage();
    }
    [Given("the user is on Student Registration Form page")]
    public void StepTheUserIsOnStudentRegistrationFormPage()
    {
        _registerPage.NavigateToPage();
    }

    [When("the user input valid data into fields")]
    public void StepTheUserInputValidDataIntoFields(DataTable table)
    {
        RegisterData registerDto = table.CreateInstance<RegisterData>();
        _scenarioContext.Set(registerDto, "register");
        _registerPage.FillRegisterForm(registerDto);
    }

    [When("the user clicks on Submit button")]
    public void StepTheUserClicksOnSubmitButton()
    {
        _registerPage.ClickSubmit();
    }

    [Then("an successfully message {string} is shown")]
    public void StepAnSuccessfullyMessageIsShown(string msg)
    {
        _registerPage.GetRegisterSuccessfullyMessage().Should().Be(msg);
    }

    [Then("all information of student form is shown")]
    public void StepAllInformationOfStudentFormIsShown()
    {
        object actualResult = _registerPage.GetRegiseteredStudent();
        RegisterData expected = _scenarioContext.Get<RegisterData>("register");
        actualResult.Should().BeEquivalentTo(expected.TrasnformToCustomDynamicObject());
    }
}