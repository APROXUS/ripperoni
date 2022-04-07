using Ripper.Core;

namespace Ripper.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand DownloadsVC { get; set; }
        public RelayCommand PropertiesVC { get; set; }
        public RelayCommand SettingsVC { get; set; }
        public RelayCommand AboutVC { get; set; }

        public DownloadsViewModel DownloadsVM { get; set; }
        public PropertiesViewModel PropertiesVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            DownloadsVM = new DownloadsViewModel();
            PropertiesVM = new PropertiesViewModel();
            SettingsVM = new SettingsViewModel();
            AboutVM = new AboutViewModel();

            CurrentView = DownloadsVM;

            DownloadsVC = new RelayCommand(o =>
            {
                CurrentView = DownloadsVM;
            });

            PropertiesVC = new RelayCommand(o =>
            {
                CurrentView = PropertiesVM;
            });

            SettingsVC = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            AboutVC = new RelayCommand(o =>
            {
                CurrentView = AboutVM;
            });
        }
    }
}
