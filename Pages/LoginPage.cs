using Microsoft.Playwright;

namespace IconProjectEvernote.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public ILocator EmailInput => _page.GetByPlaceholder("Email address or Username");
        public ILocator PasswordInput => _page.GetByPlaceholder("Password");
        public ILocator ContinueButton => _page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true });
        public ILocator ErrorMessage => _page.Locator("span.text-secondary-red-400");
        public ILocator LoggedInLocator => _page.Locator("[data-test-id='logged-in-unique-element']");

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
            await EmailInput.PressSequentiallyAsync(email, new() { Delay = 100 });
        }

        public async Task ClickContinueButton()
        {
            await ContinueButton.ClickAsync();
        }

        public async Task EnterPassword(string password)
        {
            await PasswordInput.PressSequentiallyAsync(password, new() { Delay = 100 });
        }

        public async Task<string> GetErrorMessage()
        {
            return await ErrorMessage.InnerTextAsync();
        }

        public async Task<bool> IsLoggedIn()
        {
            return await LoggedInLocator.IsVisibleAsync();
        }
    }
}
