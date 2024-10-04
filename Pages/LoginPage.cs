using Microsoft.Playwright;

namespace IconProjectEvernote.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        private string EmailInput { get; } = "[placeholder='Email address or Username']";
        private string PasswordInput { get; } = "[placeholder='Password']";
        private string LoginButton { get; } = "[data-test-id='login-button']";
        private string LoggedInUniqueElement { get; } = "[data-test-id='logged-in-unique-element']"; 
        public ILocator ErrorMessage => _page.Locator("span.text-secondary-red-400");
        public ILocator LoggedInLocator => _page.Locator(LoggedInUniqueElement);

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

        public async Task ClickLoginButton()
        {
            await _page.ClickAsync(LoginButton);
        }

        public async Task<string> GetErrorMessage()
        {
            return await ErrorMessage.InnerTextAsync();  // Using the Locator for error message
        }

        public async Task<bool> IsLoggedIn()
        {
            return await LoggedInLocator.IsVisibleAsync();
        }
    }
}
