using Microsoft.Playwright;

namespace EvernoteAutomation.Pages
{
    public class NotesPage
    {
        private readonly IPage _page;

        private readonly string newNoteButtonLocator = "";
        private readonly string noteTitleInputLocator = "";
        private readonly string noteContentInputLocator = "";
        private readonly string saveNoteButtonLocator = "";
        private readonly string logoutButtonLocator = "";
        private readonly string noteContentLocator = "";

        public NotesPage(IPage page)
        {
            _page = page;
        }

        public async Task CreateNote(string title, string content)
        {
            await _page.ClickAsync(newNoteButtonLocator);
            await _page.FillAsync(noteTitleInputLocator, title);
            await _page.FillAsync(noteContentInputLocator, content);
            await _page.ClickAsync(saveNoteButtonLocator);
        }

        public async Task<string> GetNoteContent()
        {
            return await _page.InnerTextAsync(noteContentLocator);
        }

        public async Task Logout()
        {
            await _page.ClickAsync(logoutButtonLocator);
        }
    }
}
