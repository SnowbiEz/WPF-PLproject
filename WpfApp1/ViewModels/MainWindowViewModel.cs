using ModernWpf;
using Stylet;
using StyletIoC;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using WpfApp1.Views;

namespace WpfApp1.ViewModels;

public class MainWindowViewModel : Conductor<IScreen>
{
    public static MainWindowView MainView = null!;
    public static NavigationStore Navigation = null!;
    private readonly IDialogService _dialog;
    private readonly IEventAggregator _events;
    private readonly IContainer _ioc;
    private readonly ISnackbarService _snackbar;
    private readonly IThemeService _theme;

    public MainWindowViewModel(IStyletIoCBuilder builder)
    {
        builder.Bind<MainWindowViewModel>().ToInstance(this);

        _ioc      = builder.BuildContainer();
        _events   = _ioc.Get<IEventAggregator>();
        _theme    = _ioc.Get<IThemeService>();
        _snackbar = _ioc.Get<ISnackbarService>();
        _dialog   = _ioc.Get<IDialogService>();

        // Setup pages
        LoginViewModel = _ioc.Get<LoginViewModel>();
        SettingsPageViewModel = _ioc.Get<SettingsPageViewModel>();
        RegisterViewModel = _ioc.Get<RegisterViewModel>();
    }

    public string Sex { get; set; } = "None";

    public LoginViewModel LoginViewModel { get; set; }
    public SettingsPageViewModel SettingsPageViewModel { get; set; }
    public RegisterViewModel RegisterViewModel { get; set; }
    public Screen FirstPage => LoginViewModel;

    public void Navigate(INavigation sender, RoutedNavigationEventArgs args)
    {
        if (args.CurrentPage is NavigationItem { Tag: IScreen viewModel })
            ActivateItem(viewModel);
    }

    public void NavigateToItem(IScreen view)
    {
        if (view == SettingsPageViewModel)
        {
            NavigateToSettings();
            return;
        }

        var navigationItem = Navigation.Items.OfType<NavigationItem>().First(x => x.Tag == view);
        Navigation.SelectedPageIndex = Navigation.Items.IndexOf(navigationItem);
        Navigation.Navigate(navigationItem.PageType);
    }

    public void NavigateToSettings()
    {
        ActivateItem(SettingsPageViewModel);
        Navigation.Navigate(typeof(SettingsPageView));
    }

    //public void ToggleTheme()
    //{
    //    ThemeManager.Current.ApplicationTheme = _theme.GetTheme() switch
    //    {
    //        ThemeType.Unknown      => ApplicationTheme.Dark,
    //        ThemeType.Dark         => ApplicationTheme.Light,
    //        ThemeType.Light        => ApplicationTheme.Dark,
    //        ThemeType.HighContrast => ApplicationTheme.Dark,
    //        _                      => ApplicationTheme.Dark
    //    };

    //    SettingsPageViewModel.OnThemeChanged();
    //}

    protected override async void OnViewLoaded()
    {
        MainView   = (MainWindowView)View;
        Navigation = MainView.RootNavigation;
        SettingsPageViewModel.OnThemeChanged();

        NavigateToItem(FirstPage);

        MainView.RootSnackBar.Timeout = (int)TimeSpan.FromSeconds(3).TotalMilliseconds;

        _snackbar.SetSnackbarControl(MainView.RootSnackBar);
        _dialog.SetDialogControl(MainView.RootContentDialog);

        await _snackbar.ShowAsync("Welcome!", "Please login to access features.",
            SymbolRegular.RibbonStar24, ControlAppearance.Primary);
    }
}