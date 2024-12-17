using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SimpleNotesApp.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls; // For ICommand if needed.
using System.Linq;
using SimpleNotesApp.Services;


namespace SimpleNotesApp.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged members
        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ObservableCollection for Notes
        private ObservableCollection<Note> _notes= new ObservableCollection<Note>();
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        // Selected note (to edit or pass to detail)
        private Note? _selectedNote;
        public Note? SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        private readonly ApiService _apiService;

        public NotesViewModel()
        {
            _apiService = new ApiService();
            /*
            first try with mock data
            // Initialize mock data
            Notes = new ObservableCollection<Note>
            {
                new Note { Id = 1, Title = "Test Note 1", Content = "Content for note 1" },
                new Note { Id = 2, Title = "Test Note 2", Content = "Content for note 2" }
            };


            // Initialize commands
            AddNoteCommand = new Command(AddNote);
            EditNoteCommand = new Command<Note>(EditNote);
            DeleteNoteCommand = new Command<Note>(DeleteNote);
            */

            // Commands now call async methods that interact with the API
            AddNoteCommand = new Command(async () => await AddNoteAsync());
            DeleteNoteCommand = new Command<Note>(async (note) => await DeleteNoteAsync(note));

            // On initialization, load notes from the API
            LoadNotes();
        }

        private async void LoadNotes()
        {
            try
            {
                var notesFromApi = await _apiService.GetNotesAsync();
                Notes = new ObservableCollection<Note>(notesFromApi);
            }
            catch (Exception ex)
            {
                // Simple error handling:
                // In a real app, you might show a message to the user.
                Console.WriteLine("Error loading notes: " + ex.Message);
            }
        }

        private async Task AddNoteAsync()
        {
            try
            {
                var newNote = new Note { Title = "New Note", Content = "New content" };
                var createdNote = await _apiService.CreateNoteAsync(newNote);
                Notes.Add(createdNote);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding note: " + ex.Message);
            }
        }

        private async Task DeleteNoteAsync(Note noteToDelete)
        {
            if (noteToDelete == null) return;

            try
            {
                await _apiService.DeleteNoteAsync(noteToDelete.Id);
                Notes.Remove(noteToDelete);

                SelectedNote = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting note: " + ex.Message);
            }
        }

        public void RefreshNotes()
        {
            LoadNotes(); // calls the API again to fetch the latest data
        }
    }
}

