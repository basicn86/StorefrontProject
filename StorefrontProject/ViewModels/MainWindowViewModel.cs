using ReactiveUI;

namespace StorefrontProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? mainContent;
        public ViewModelBase? MainContent {
            get => mainContent;
            set => this.RaiseAndSetIfChanged(ref mainContent, value);
        }

        public MainWindowViewModel()
        {
            MainContent = new CatalogViewModel();
        }
    }
}
