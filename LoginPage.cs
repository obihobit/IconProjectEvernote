using Microsoft.Playwright;

namespace EvernoteAutomation.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public string EmailInputLocator { get; } = "Email address or Username";
        public string PasswordInputLocator { get; } = "Password";
        public string LoginButtonLocator { get; } = "";
        public string ErrorMessageLocator { get; } = "";
        public string LoggedInUniqueElementLocator { get; } = "";
        public ILocator LoggedInLocator => _page.Locator(this.LoggedInUniqueElementLocator);


        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task NavigateToLoginPage()
        {
            await _page.GotoAsync("https://evernote.com/login");
        }

        public async Task EnterEmail(string email)
        {
            await _page.GetByPlaceholder("Email address or Username").PressSequentiallyAsync(email, new() { Delay = 100 });
        }

        public async Task ClickContinueButton()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        }


        public async Task EnterPassword(string password)
        {
            await _page.GetByPlaceholder("Password").PressSequentiallyAsync(password, new() { Delay = 100 });
        }

        public async Task<string> GetErrorMessage()
        {
            return await _page.InnerTextAsync(ErrorMessageLocator);
        }

        public async Task<bool> IsLoggedIn()
        {
            return await _page.IsVisibleAsync(LoggedInUniqueElementLocator);
        }
    }
}
