namespace SimpleNotesApp;
using SimpleNotesApp.Models;        // For Note
using SimpleNotesApp.ViewModels;    // For NotesViewModel

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnGoToDetailClicked(object sender, EventArgs e)
    {
        // Assuming that SelectedNote has been set by the user selecting an item in the CollectionView
        var viewModel = BindingContext as NotesViewModel;
        if (viewModel?.SelectedNote != null)
        {
            await Navigation.PushAsync(new DetailPage(viewModel.SelectedNote));
        }
        else
        {
            await DisplayAlert("No note selected", "Please select a note before going to the detail page.", "OK");
        }
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as NotesViewModel)?.RefreshNotes();
    }


}




