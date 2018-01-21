using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using IncomeEvidenceOSVC.Models;

namespace IncomeEvidenceOSVC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static List<DailyInfo> DailyBalanceCollection = new List<DailyInfo>();
        internal static String FileName { get; set; }
        internal static Boolean ProgressNotSaved { get; set; }
        internal static Boolean IsFileOpened { get; set; }
        internal delegate void LoadDataEventHandler();
        internal delegate void SaveOnExitEventHandler();
        internal static event LoadDataEventHandler LoadData;
        internal static event SaveOnExitEventHandler SaveOnExit;
        private void Application_Exit(Object sender, ExitEventArgs e)
        {
            if(ProgressNotSaved)
            {
                var dialogResult = MessageBox.Show("Chcete uložit změny?", "Upozornění", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveOnExit();
                }
            }
            IncomeEvidenceOSVC.Properties.Settings.Default.Save();
        }

        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
        public static void SetFileName(String fileName)
        {
            FileName = fileName;
            IsFileOpened = true;
            LoadData();
        }
    }
}
