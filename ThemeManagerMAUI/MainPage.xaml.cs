namespace ThemeManagerMAUI;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        ThemeManager.SelectedThemeChanged += ThemeManager_SelectedThemeChanged;
    }

    private void ThemeManager_SelectedThemeChanged(object sender, EventArgs e)
    {
        SelectedTheme = ThemeManager.SelectedTheme;
    }

    private string _selectedTheme = ThemeManager.SelectedTheme;
    public string SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if(value != _selectedTheme)
            {
                _selectedTheme = value;
                OnPropertyChanged(nameof(SelectedTheme));
            }
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ThemeManager.SetTheme(nameof(ThemeManagerMAUI.Resources.Themes.Nature));
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var newTheme = await DisplayActionSheet("Choose Theme", "Cancel", null, ThemeManager.ThemeNames);
        if(!string.IsNullOrWhiteSpace(newTheme) && newTheme != "Cancel")
        {
            ThemeManager.SetTheme(newTheme);
        }
    }
}

