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
                try
                {
                    CurrentView = null;
                }
                catch
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "016", false);
                }
            });

            PropertiesVC = new RelayCommand(o =>
            {
                try
                {
                    CurrentView = PropertiesVM;
                }
                catch
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "017", false);
                }
            });

            SettingsVC = new RelayCommand(o =>
            {
                try
                {
                    CurrentView = SettingsVM;
                }
                catch
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "018", false);
                }
            });

            AboutVC = new RelayCommand(o =>
            {
                try
                {
                    CurrentView = AboutVM;
                }
                catch
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "019", false);
                }
            });
            #endregion
        }
    }
}
