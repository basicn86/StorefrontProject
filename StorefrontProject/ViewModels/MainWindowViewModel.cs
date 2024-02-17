using ReactiveUI;
using StorefrontProject.Models;

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
            //if the application is running in debug configuration, set the main content to a new instance of the CatalogViewModel with DebugAPIService in the constructor
#if DEBUG
            MainContent = new CatalogViewModel(new DebugAPIService());
#endif

            //if the application is running in release configuration, set the main content to a new instance of the CatalogViewModel with APIService in the constructor
#if !DEBUG
            MainContent = new CatalogViewModel(new APIService());
#endif
        }
    }
}
