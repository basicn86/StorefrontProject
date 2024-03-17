using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using ReactiveUI;

namespace StorefrontProject.ViewModels
{
    public class ConfirmDialogViewModel : ViewModelBase
    {
        public string Message { get; set; }
        public bool? Result { get; set; }
        public ConfirmDialogViewModel(string message)
        {
            Message = message;

            YesCommand = ReactiveCommand.Create(() =>
            {
                return true;
            });

            NoCommand = ReactiveCommand.Create(() =>
            {
                return false;
            });
        }

        //Reactive command for the Yes button
        public ReactiveCommand<Unit, bool> YesCommand { get; set; }
        //Reactive command for the No button
        public ReactiveCommand<Unit, bool> NoCommand { get; set; }
    }
}
