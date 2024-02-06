using ModernWpf;
using Stylet;
using StyletIoC;
using Wpf.Ui.Appearance;
using Wpf.Ui.Mvvm.Contracts;
using WpfApp1.ModernWPF;
using Transition = WpfApp1.ModernWPF.Animation.Transition;
using TransitionCollection = WpfApp1.ModernWPF.Animation.Transitions.TransitionCollection;

namespace WpfApp1.ViewModels;

public class SettingsPageViewModel : Screen
{
    private readonly IEventAggregator _events;
    private readonly IContainer _ioc;
    private readonly MainWindowViewModel _main;
    private readonly IThemeService _theme;

    public SettingsPageViewModel(IContainer ioc, MainWindowViewModel main)
    {
        _ioc    = ioc;
        _events = ioc.Get<IEventAggregator>();
        _theme  = ioc.Get<IThemeService>();
        _main   = main;
    }

    public static CaptionedObject<Transition>? Transition { get; set; } = TransitionCollection.Transitions[0];

    public void OnThemeChanged()
    {
        _theme.SetTheme(ThemeManager.Current.ApplicationTheme switch
        {
            ApplicationTheme.Light => ThemeType.Light,
            ApplicationTheme.Dark  => ThemeType.Dark,
            _                      => _theme.GetSystemTheme()
        });
    }
}