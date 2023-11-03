namespace ThemeManagerMAUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    protected override void OnStart()
    {
        base.OnStart();
        ThemeManager.Initialize();
    }
}
