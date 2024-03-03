using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using StorefrontProject.Services;
using StorefrontProject.ViewModels;
using System.Reactive;
using System.Threading.Tasks;

namespace StorefrontProject.Views
{
    public partial class MainWindow : ReactiveWindow<ViewModels.MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(
                action => action(DialogService.ConfirmDialogInteraction.RegisterHandler(ShowConfirmDialogAsync))
            );
        }

        private async Task ShowConfirmDialogAsync(InteractionContext<ConfirmDialogViewModel, bool> interaction)
        {
            var dialog = new ConfirmDialogView();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<bool>(this);
            interaction.SetOutput(result);
        }
    }
}