using Stylet;
using StyletIoC;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using WpfApp1.ViewModels;

namespace WpfApp1;

public class Bootstrapper : Bootstrapper<MainWindowViewModel>
{
    private IStyletIoCBuilder _builder;

    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        builder.Bind<IThemeService>().To<ThemeService>().InSingletonScope();
        builder.Bind<ISnackbarService>().To<SnackbarService>().InSingletonScope();
        builder.Bind<IDialogService>().To<DialogService>().InSingletonScope();
        builder.Bind<IStyletIoCBuilder>().ToInstance(builder);

        _builder = builder;
    }
}