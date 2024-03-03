using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;

namespace StorefrontProject.Views;

public partial class ConfirmDialogView : ReactiveWindow<ViewModels.ConfirmDialogViewModel>
{
    public ConfirmDialogView()
    {
        InitializeComponent();

        this.WhenActivated(lambda => lambda(ViewModel?.YesCommand.Subscribe(result => Close(result))));
        this.WhenActivated(lambda => lambda(ViewModel?.NoCommand.Subscribe(result => Close(result))));
    }
}