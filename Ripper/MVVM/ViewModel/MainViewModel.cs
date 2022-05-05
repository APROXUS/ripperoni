using System;
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
                catch (Exception ex)
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "016", false, ex);
                }
            });

            PropertiesVC = new RelayCommand(o =>
            {
                try
                {
                    CurrentView = PropertiesVM;
                }
                catch (Exception ex)
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "017", false, ex);
                }
            });

            SettingsVC = new RelayCommand(o =>
            {
                try
                {
                    CurrentView = SettingsVM;
                }
                catch (Exception ex)
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "018", false, ex);
                }
            });

            AboutVC = new RelayCommand(o =>
            {
                try
                {
                    CurrentView = AboutVM;
                }
                catch (Exception ex)
                {
                    Utilities.Error("Could not change current view...", "Executable Error", "019", false, ex);
                }
            });
            #endregion
        }
    }
}
