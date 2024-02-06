using System.Windows.Controls;
using WpfApp1.ModernWPF.Animation;
using WpfApp1.ViewModels;

namespace WpfApp1.ModernWPF;

public class AnimatedContentControl : ContentControl
{
    private static Transition Transition => SettingsPageViewModel.Transition!.Object;

    protected override void OnContentChanged(object? oldContent, object? newContent)
    {
        if (oldContent != null)
        {
            var exit = Transition.GetExitAnimation(oldContent, false);
            exit?.Begin();
        }

        if (newContent != null)
        {
            var enter = Transition.GetEnterAnimation(newContent, false);
            enter?.Begin();
        }

        base.OnContentChanged(oldContent, newContent);
    }
}