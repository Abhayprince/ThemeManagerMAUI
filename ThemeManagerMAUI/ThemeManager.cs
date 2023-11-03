using Themes = ThemeManagerMAUI.Resources.Themes;

namespace ThemeManagerMAUI;
public static class ThemeManager
{
    private const string ThemeKey = "theme";
    private const string PrevThemeKey = "previous-theme";

    private static readonly IDictionary<string, ResourceDictionary> _themesMap =
        new Dictionary<string, ResourceDictionary>
        {
            [nameof(Themes.Default)] = new Themes.Default(),
            [nameof(Themes.Fire)] = new Themes.Fire(),
            [nameof(Themes.Nature)] = new Themes.Nature(),
            [nameof(Themes.Dark)] = new Themes.Dark()
        };

    static ThemeManager()
    {
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    public static void Initialize()
    {
        var selectedTheme = Preferences.Default.Get<string>(ThemeKey, null);
        if (selectedTheme is null && Application.Current.PlatformAppTheme == AppTheme.Dark)
        {
            selectedTheme = nameof(Themes.Dark);
        }

        SetTheme(selectedTheme ?? nameof(Themes.Default));
    }

    public static event EventHandler SelectedThemeChanged;

    public static string[] ThemeNames => _themesMap.Keys.ToArray();

    private static void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        if (e.RequestedTheme == AppTheme.Dark)
        {
            if (SelectedTheme != nameof(Themes.Dark))
            {
                Preferences.Default.Set(PrevThemeKey, SelectedTheme);
            }
            SetTheme(nameof(Themes.Dark));
        }
        else
        {
            var prevTheme = Preferences.Default.Get<string>(PrevThemeKey, nameof(Themes.Default));
            SetTheme(prevTheme);
        }
    }

    public static string SelectedTheme { get; set; } = nameof(Themes.Default);

    public static void SetTheme(string themeName)
    {
        if (SelectedTheme == themeName)
            return;

        var themeToBeApplied = _themesMap[themeName];

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(themeToBeApplied);

        SelectedTheme = themeName;
        SelectedThemeChanged?.Invoke(null, EventArgs.Empty);

        Preferences.Default.Set<string>(ThemeKey, themeName);
    }
}
