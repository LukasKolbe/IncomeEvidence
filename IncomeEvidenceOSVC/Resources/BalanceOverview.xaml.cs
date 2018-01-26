using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using IncomeEvidenceOSVC.Models;
using Newtonsoft.Json;
using static System.Environment;

namespace IncomeEvidenceOSVC.Resources
{
    /// <summary>
    /// Interaction logic for BalanceOverview.xaml
    /// </summary>
    public partial class BalanceOverview : Page
    {

        public BalanceOverview()
        {
            InitializeComponent();
            App.LoadData += App_LoadData;
            ToggleFileData();
        }

        private void App_LoadData()
        {
            ToggleFileData();
            try
            {
                var stream = File.Open(Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Saves", App.FileName), FileMode.OpenOrCreate);
                var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                stream.Flush();
                reader.Close();
                App.DailyBalanceCollection = JsonConvert.DeserializeObject<List<DailyInfo>>(json) ?? new List<DailyInfo>();
                App.IsFileOpened = true;
                DailyBalanceList.IsEnabled = true;
                EditRecordButton.IsEnabled = true;
                NewRecordButton.IsEnabled = true;
                RemoveRecordButton.IsEnabled = true;
                RefreshData();
                SummarizeIncomeAndCost();
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
        }

        private void ToggleFileData()
        {
            if(App.IsFileOpened)
            {
                OverviewGrid.Visibility = Visibility.Visible;
            }
            else
            {
                OverviewGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Page_Loaded(Object sender, RoutedEventArgs e)
        {
            RefreshData();
            SummarizeIncomeAndCost();
        }

        private void SummarizeIncomeAndCost()
        {
            Decimal totalIncome = 0, totalCosts = 0;
            foreach(var Record in App.DailyBalanceCollection)
            {
                totalIncome += Record.Income;
                totalCosts += Record.Costs;
            }
            SummaryIncome.Text = totalIncome.ToString("C");
            SummaryCosts.Text = totalCosts.ToString("C");
            if((totalIncome - totalCosts) > 0)
            {
                Gain.Foreground = Brushes.Green;
            }
            else if((totalIncome - totalCosts) < 0)
            {
                Gain.Foreground = Brushes.Red;
            }
            else
            {
                Gain.Foreground = Brushes.Black;
            }
            Gain.Text = (totalIncome - totalCosts).ToString("C");
        }

        private void RefreshData()
        {
            DailyBalanceList.ItemsSource = null;
            App.DailyBalanceCollection = App.DailyBalanceCollection.OrderBy(p => p.Date).ToList();
            DailyBalanceList.ItemsSource = App.DailyBalanceCollection;
        }

        private void AddNewRecord_Click(Object sender, RoutedEventArgs e)
        {
            var frame = App.Current.MainWindow.FindName("MainFrame") as Frame;
            frame.Navigate(new RecordForm());              
        }

        private void RemoveRecord_Click(Object sender, RoutedEventArgs e)
        {
            if(DailyBalanceList.SelectedItem == null)
            {
                MessageBox.Show("Vyber záznam", "Chyba", MessageBoxButton.OK);
                return;
            }
            else
            {
                var item = DailyBalanceList.SelectedItem as DailyInfo;
                RemoveDocuments(item);
                App.DailyBalanceCollection.RemoveAt(App.DailyBalanceCollection.IndexOf(item));
                App.ProgressNotSaved = true;
                RefreshData();
                SummarizeIncomeAndCost();
            }
        }

        private void RemoveDocuments(DailyInfo item)
        {
            foreach(var document in item.Documents)
            {
                try
                {
                    File.Delete(document.Path);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
                }
            }
        }

        private void EditRecord_Click(Object sender, RoutedEventArgs e)
        {
            if(DailyBalanceList.SelectedItem == null)
            {
                MessageBox.Show("Vyber záznam", "Chyba", MessageBoxButton.OK);
                return;
            }
            else
            {
                var item = DailyBalanceList.SelectedItem as DailyInfo;
                var frame = App.Current.MainWindow.FindName("MainFrame") as Frame;
                frame.Navigate(new RecordForm(item));
            }
        }

        private void ListViewItem_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
        {
            var item = ((ListViewItem)sender).DataContext as DailyInfo;
            var frame = App.Current.MainWindow.FindName("MainFrame") as Frame;
            frame.Navigate(new RecordForm(item));
        }
    }
}
