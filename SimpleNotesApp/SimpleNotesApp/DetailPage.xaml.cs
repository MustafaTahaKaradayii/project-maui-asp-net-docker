using SimpleNotesApp.Models; // Needed for Note
using SimpleNotesApp.Services; // Needed for ApiService

namespace SimpleNotesApp
{
    public partial class DetailPage : ContentPage
    {
        private Note _currentNote;
        private bool _isNewNote;

        public DetailPage(Note note)
        {
            InitializeComponent();

            if (note == null)
            {
                // Create mode
                _currentNote = new Note();
                _isNewNote = true;
            }
            else
            {
                // Edit mode
                _currentNote = note;
                _isNewNote = false;
            }

            TitleEntry.Text = _currentNote.Title;
            ContentEditor.Text = _currentNote.Content;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _currentNote.Title = TitleEntry.Text ?? string.Empty;
            _currentNote.Content = ContentEditor.Text ?? string.Empty;

            var apiService = new ApiService();

            try
            {
                if (_isNewNote)
                {
                    await apiService.CreateNoteAsync(_currentNote);
                }
                else
                {
                    await apiService.UpdateNoteAsync(_currentNote.Id, _currentNote);
                }

                await Navigation.PopAsync();
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                // If this is a 404, it means the note no longer exists.
                Console.WriteLine("Error saving note: " + ex.Message);
                await DisplayAlert("Error", "This note no longer exists or cannot be updated.", "OK");
            }
        }
    }
}

