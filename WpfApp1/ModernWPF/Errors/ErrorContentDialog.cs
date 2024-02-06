﻿using Humanizer;

namespace WpfApp1.ModernWPF.Errors;

public class ErrorContentDialog : ModernWpf.Controls.ContentDialog
{
    public ErrorContentDialog(Exception e, IReadOnlyCollection<Enum>? options = null, string? closeText = null)
    {
        Title   = e.GetType().Name;
        Content = e.Message;

        PrimaryButtonText   = options?.ElementAtOrDefault(0)?.Humanize();
        SecondaryButtonText = options?.ElementAtOrDefault(1)?.Humanize();

        CloseButtonText = closeText ?? "Abort";
    }
}