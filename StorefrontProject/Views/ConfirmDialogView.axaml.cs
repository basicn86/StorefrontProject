using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace StorefrontProject.Views;

public partial class ConfirmDialogView : ReactiveWindow<ViewModels.ConfirmDialogViewModel>
{
    public ConfirmDialogView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            if (ViewModel?.YesCommand != null)
            {
                ViewModel.YesCommand.Subscribe(result => Close(result));
            }
            if (ViewModel?.NoCommand != null)
            {
                ViewModel.NoCommand.Subscribe(result => Close(result));
            }
        });
    }
}