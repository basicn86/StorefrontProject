using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using StorefrontProject.ViewModels;

namespace StorefrontProject.Services
{
    public static class DialogService
    {
        //interaction to show a confirmation dialog
        public static Interaction<ConfirmDialogViewModel, bool> ConfirmDialogInteraction { get; } = new Interaction<ConfirmDialogViewModel, bool>();
    }
}
