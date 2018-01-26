using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using IncomeEvidenceOSVC.Models;
using static System.Environment;

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

        internal static void CheckSavesDirectoryAccess()
        {
            if(!Directory.Exists(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
            if(!Directory.Exists(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Saves")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Saves"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
        }

        internal static void CheckDocumentsDirectoryAccess()
        {
            if(!Directory.Exists(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
            if(!Directory.Exists(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Documents")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Documents"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
        }
    }
}
