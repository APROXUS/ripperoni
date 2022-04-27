using Ripper.Core;

namespace Ripper.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        #region View Variables...
        public RelayCommand DownloadsVC { get; set; }
        public RelayCommand PropertiesVC { get; set; }
        public RelayCommand SettingsVC { get; set; }
        public RelayCommand AboutVC { get; set; }


        public PropertiesViewModel PropertiesVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }

        private object _currentView;
        #endregion

        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            PropertiesVM = new PropertiesViewModel();
            SettingsVM = new SettingsViewModel();
            AboutVM = new AboutViewModel();

            CurrentView = null;

            #region Relay Commands...
            DownloadsVC = new RelayCommand(o =>
            {
                CurrentView = null;
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
            #endregion
        }
    }
}
