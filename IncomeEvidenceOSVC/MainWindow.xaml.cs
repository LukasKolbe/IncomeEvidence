using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;
using IncomeEvidenceOSVC.Resources;
using Microsoft.Win32;
using Newtonsoft.Json;
using static System.Environment;

namespace IncomeEvidenceOSVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.LoadData += App_LoadData;
            App.SaveOnExit += App_SaveOnExit;
        }

        private void App_SaveOnExit() => SaveFile_Click(null, null);

        private void App_LoadData()
        {
            SaveMenuButton.IsEnabled = true;
            CloseFileMenuButton.IsEnabled = true;
            if(!(MainFrame.Content is BalanceOverview))
            {
                MainFrame.Navigate(new BalanceOverview());
            }
        }

        private void Window_Loaded(Object sender, RoutedEventArgs e) => MainFrame.Navigate(new BalanceOverview());

        private void NewFile_Click(Object sender, RoutedEventArgs e)
        {
            if(App.ProgressNotSaved)
            {
                var dialogResult = MessageBox.Show("Chcete uložit změny?", "Upozornění", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveFile_Click(null, null);
                }
                App.ProgressNotSaved = false;
            }
            NewFileNameSelector.Show();
        }

        private void OpenFile_Click(Object sender, RoutedEventArgs e)
        {
            if(App.ProgressNotSaved)
            {
                var dialogResult = MessageBox.Show("Chcete uložit změny?", "Upozornění", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveFile_Click(null, null);
                }
                App.ProgressNotSaved = false;
            }
            var saves = GetSaves();
            if(saves != null)
            {
                SavesManager.Show(saves);
            }
        }

        private static List<Tuple<String, String>> GetSaves()
        {
            var saves = new List<Tuple<String, String>>();
            App.CheckSavesDirectoryAccess();
            try
            {
                var files = Directory.GetFiles(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments),"MoneyManagerData", "Saves"));
                foreach(var path in files)
                {
                    saves.Add(Tuple.Create(Path.GetFileNameWithoutExtension(path), Path.GetFileName(path)));
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                return null;
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                return null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }

            return saves;
        }

        private void SaveFile_Click(Object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.SerializeObject(App.DailyBalanceCollection);
            App.CheckSavesDirectoryAccess();
            try
            {
                File.WriteAllText(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Saves", App.FileName), json);
                App.ProgressNotSaved = false;
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
            catch(Exception)
            {
            }
        }

        private void Exit_Click(Object sender, RoutedEventArgs e)
        {
            if(App.ProgressNotSaved)
            {
                var dialogResult = MessageBox.Show("Chcete uložit změny?", "Upozornění", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveFile_Click(null, null);
                    App.ProgressNotSaved = false;
                }
            }
            Close();
        }

        private void Export_Click(Object sender, RoutedEventArgs e)
        {
            if(App.ProgressNotSaved)
            {
                var dialogResult = MessageBox.Show("Chcete uložit změny?", "Upozornění", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveFile_Click(null, null);
                }
                App.ProgressNotSaved = false;
            }
            var folderPicker = new System.Windows.Forms.FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
            var result = folderPicker.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                var destPath = Path.Combine(folderPicker.SelectedPath,"Export_"+DateTime.Now+".zip");
                App.CheckSavesDirectoryAccess();
                var savesToZip = Directory.GetFiles(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Saves"));
                App.CheckDocumentsDirectoryAccess();
                var documentsToZip = Directory.GetFiles(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Documents"));
                try
                {
                    var info = Directory.CreateDirectory(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Temp"));
                    foreach(var file in savesToZip)
                    {
                        File.Copy(file, Path.Combine(info.FullName,Path.GetFileName(file)));
                    }
                    foreach(var document in documentsToZip)
                    {
                        File.Copy(document, Path.Combine(info.FullName, Path.GetFileName(document)));
                    }
                    ZipFile.CreateFromDirectory(info.FullName, destPath);
                    MessageBox.Show("Export proběhl úspěšně", "", MessageBoxButton.OK);
                    Directory.Delete(info.FullName, true);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
        }

        private void Import_Click(Object sender, RoutedEventArgs e)
        {
            var filePicker = new OpenFileDialog
            {
                Filter = "Zip files (*.zip)|*.zip"
            };
            var importFile = filePicker.ShowDialog();
            if(importFile == true)
            {
                App.CheckDocumentsDirectoryAccess();
                App.CheckSavesDirectoryAccess();
                try
                {
                    var info = Directory.CreateDirectory(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Temp"));
                    ZipFile.ExtractToDirectory(filePicker.FileName, info.FullName);
                    var savesPaths = Directory.GetFiles(info.FullName, "*.json");
                    var documentPaths = Directory.GetFiles(info.FullName).Where(p => !p.EndsWith(".json")).ToArray();
                    foreach(var filePath in savesPaths)
                    {
                        var fileName = Path.GetFileName(filePath);
                        File.Copy(Path.Combine(info.FullName, fileName), Path.Combine(GetFolderPath(SpecialFolder.MyDocuments),"MoneyManagerData", "Saves", fileName),true);
                        File.Delete(Path.Combine(info.FullName, fileName));
                    }
                    foreach(var filePath in documentPaths)
                    {
                        var fileName = Path.GetFileName(filePath);
                        File.Copy(Path.Combine(info.FullName, fileName), Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Documents", fileName), true);
                        File.Delete(Path.Combine(info.FullName, fileName));
                    }
                    Directory.Delete(info.FullName, true);
                    MessageBox.Show("Import proběhl úspěšně", "", MessageBoxButton.OK);
                }
                catch(InvalidDataException)
                {
                    MessageBox.Show("Nepodporovaný typ souboru", "Chyba", MessageBoxButton.OK);
                }
                catch(UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
                catch(IOException ex)
                {
                    var result = MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
        }

        private void CloseFile_Click(Object sender, RoutedEventArgs e)
        {
            if(!(MainFrame.Content is BalanceOverview))
            {
                MainFrame.Navigate(new BalanceOverview());
            }
            var page = MainFrame.Content as BalanceOverview;
            page.OverviewGrid.Visibility = Visibility.Collapsed;
            if(App.ProgressNotSaved)
            {
                var dialogResult = MessageBox.Show("Chcete uložit změny?", "Upozornění", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.Yes)
                {
                    SaveFile_Click(null, null);
                }
                App.ProgressNotSaved = false;
            }
            SaveMenuButton.IsEnabled = false;
            App.DailyBalanceCollection = null;
            App.IsFileOpened = false;
            CloseFileMenuButton.IsEnabled = false;
        }
    }
}
