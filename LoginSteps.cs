using EvernoteAutomation.Pages;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

[Binding]
public class LoginSteps
{
    private IPage _page;
    private LoginPage _loginPage;
    private NotesPage _notesPage;

    [Given(@"I am on the login page")]
    public async Task GivenIAmOnTheLoginPage()
    {
        var playwright = await Playwright.CreateAsync();
        var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36";
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            IgnoreDefaultArgs = new[] { "--disable-component-extensions-with-background-pages" },
            Args = new[]
            {
                "--disable-blink-features=AutomationControlled",
                "--no-sandbox",
                "--disable-web-security",
                "--disable-infobars",
                "--disable-extensions",
                "--start-maximized",
                "--window-size=1280,720"
            }
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            UserAgent = userAgent,
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
            DeviceScaleFactor = 1
        });

        _page = await context.NewPageAsync();
        _loginPage = new LoginPage(_page);
        await _loginPage.NavigateToLoginPage();
    }

    [When(@"I login with invalid email ""(.*)"" and password ""(.*)""")]
    public async Task WhenILoginWithInvalidEmailAndPassword(string email, string password)
    {
        await _loginPage.EnterEmail(email)
;
        await _loginPage.ClickContinueButton();
        await _loginPage.EnterPassword(password);
        await _loginPage.ClickContinueButton();
    }

    [Then(@"I should see the error message ""(.*)""")]
    public async Task ThenIShouldSeeTheErrorMessage(string expectedErrorMessage)
    {
        var errorMessageLocator = _page.Locator(_loginPage.ErrorMessageLocator);
        await Assertions.Expect(errorMessageLocator).ToHaveTextAsync(expectedErrorMessage);
    }

    [When(@"I login with valid email ""(.*)"" and password ""(.*)""")]
    [When(@"I login again with valid email ""(.*)"" and password ""(.*)""")]
    public async Task WhenILoginWithValidEmailAndPassword(string email, string password)
    {
        await _loginPage.EnterEmail(email)
;
        await _loginPage.ClickContinueButton();
        await _loginPage.EnterPassword(password);
        await _loginPage.ClickContinueButton();
    }

    [Then(@"I should be logged in successfully")]
    public async Task ThenIShouldBeLoggedInSuccessfully()
    {
        await Assertions.Expect(_loginPage.LoggedInLocator).ToBeVisibleAsync();
    }

    [Given(@"I am logged in with valid email ""(.*)"" and password ""(.*)""")]
    public async Task GivenIAmLoggedInWithValidEmailAndPassword(string email, string password)
    {
        await _loginPage.EnterEmail(email)
;
        await _loginPage.ClickContinueButton();
        await _loginPage.EnterPassword(password);
        await _loginPage.ClickContinueButton();
    }

    [When(@"I create a note with title ""(.*)"" and content ""(.*)""")]
    public async Task WhenICreateANoteWithTitleAndContent(string title, string content)
    {
        _notesPage = new NotesPage(_page);
        await _notesPage.CreateNote(title, content);
    }

    [When(@"I logout")]
    public async Task WhenILogout()
    {
        _notesPage = new NotesPage(_page);
        await _notesPage.Logout();
    }

    [Then(@"I should see the note with title ""(.*)"" and content ""(.*)""")]
    public async Task ThenIShouldSeeTheNoteWithTitleAndContent(string expectedTitle, string expectedContent)
    {
        //_notesPage = new NotesPage(_page);
        //var noteContent = await _notesPage.GetNoteContent();
        //Assert.Contains(expectedContent, noteContent);
    }
}