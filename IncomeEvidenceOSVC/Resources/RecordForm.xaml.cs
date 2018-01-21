using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IncomeEvidenceOSVC.Models;
using Microsoft.Win32;
using static System.Environment;

namespace IncomeEvidenceOSVC.Resources
{
    public partial class RecordForm : Page
    {

        /// <summary>
        /// The identifier used for operations with unique IDs in collection of DailyInfo
        /// </summary>
        private Int32 _id;

        /// <summary>
        /// Gets the assigned documents of current instance.
        /// </summary>
        /// <value>
        /// The assigned documents.
        /// </value>
        public List<Document> AssignedDocuments { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordForm"/> class.
        /// </summary>
        public RecordForm()
        {
            InitializeComponent();
            OverwriteRecordButton.Visibility = Visibility.Collapsed;
            AssignedDocuments = new List<Document>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordForm"/> class for editing.
        /// </summary>
        /// <param name="data">Data for edit</param>
        public RecordForm(DailyInfo data)
        {
            InitializeComponent();
            _id = data.Id;
            Date.SelectedDate = data.Date;
            IncomeTbx.Text = data.Income.ToString();
            CostsTbx.Text = data.Costs.ToString();
            NoteTbx.Text = data.Note;
            AssignedDocuments = data.Documents ?? new List<Document>();
            DocumentsList.ItemsSource = AssignedDocuments;
            SaveNewRecordButton.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Sets date in datepicker to DateTime.Today
        /// </summary>
        private void Page_Loaded(Object sender, RoutedEventArgs e)
        {
            if(Date.SelectedDate == null)
            {
                Date.SelectedDate = DateTime.Today;
            }
            IncomeTbx.Focus();
        }

        /// <summary>
        /// Adds new instance of DailyInfo into DailyBalanceCollection.
        /// </summary>
        private void SaveRecordButton_Click(Object sender, RoutedEventArgs e)
        {
            var record = new DailyInfo();
            if(Date.SelectedDate == null)
            {
                MessageBox.Show("Zvolte datum", "Chyba", MessageBoxButton.OK);
                return;
            }
            record.Date = Date.SelectedDate.Value;
            Decimal.TryParse(IncomeTbx.Text, out var income);
            Decimal.TryParse(CostsTbx.Text, out var costs);
            record.Income = income;
            record.Costs = costs;
            record.Note = NoteTbx.Text;
            record.Documents = AssignedDocuments;
            if(App.DailyBalanceCollection.Count > 0)
            {
                record.Id = App.DailyBalanceCollection.Max(p => p.Id) + 1;
            }
            else
            {
                record.Id = 1;
            }
            App.DailyBalanceCollection.Add(record);
            App.ProgressNotSaved = true;
            CloseForm();
        }
        /// <summary>
        /// Overwrites current instance of DailyInfo in DailyBalanceCollection.
        /// </summary>
        private void OverwriteRecordButton_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                App.DailyBalanceCollection.RemoveAt(App.DailyBalanceCollection.IndexOf(App.DailyBalanceCollection.Where(p => p.Id == _id).Single()));
            }
            catch(Exception)
            {
                MessageBox.Show("Došlo k chybě, zkuste pokus zopakovat.", "Chyba", MessageBoxButton.OK);
            }
            var record = new DailyInfo();
            if(Date.SelectedDate == null)
            {
                MessageBox.Show("Zvolte datum", "Chyba", MessageBoxButton.OK);
                return;
            }
            record.Date = Date.SelectedDate.Value;
            Decimal.TryParse(IncomeTbx.Text, out var income);
            Decimal.TryParse(CostsTbx.Text, out var costs);
            record.Income = income;
            record.Costs = costs;
            record.Note = NoteTbx.Text;
            record.Id = _id;
            record.Documents = AssignedDocuments;
            App.DailyBalanceCollection.Add(record);
            App.ProgressNotSaved = true;
            CloseForm();
        }

        private void CancelButton_Click(Object sender, RoutedEventArgs e) => CloseForm();

        /// <summary>
        /// Closes the form and navigates back to main page.
        /// </summary>
        private static void CloseForm()
        {
            var frame = App.Current.MainWindow.FindName("MainFrame") as Frame;
            frame.Navigate(new BalanceOverview());
        }

        private void CommandBinding_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            if(OverwriteRecordButton.Visibility == Visibility.Visible)
            {
                OverwriteRecordButton_Click(null, null);
            }
            else
            {
                SaveRecordButton_Click(null, null);
            }
        }

        private void ListBoxItem_MouseDoubleClick(Object sender, MouseButtonEventArgs e) => OpenDocument(sender);

        private void OpenDocument(Object sender)
        {
            // Open document file
            var document = ((ListBoxItem)sender).DataContext as Document;
            var extension = Path.GetExtension(document.Path);
            try
            {
                Process.Start(document.Path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
            //if(extension == ".pdf")
            //{
            //    DisplayPDF(document);
            //}
            //else if(extension == ".jpg"|| extension == ".png" || extension == ".jpeg")
            //{

            //}
        }

        private void DisplayPDF(Document document)
        {
            Process process = null;
            try
            {
                Dispatcher.Invoke(() =>
                {
                    var info = new ProcessStartInfo("AcroRd32.exe");
                    info.Arguments = "/n " + Path.GetTempPath() + "tisk.pdf";
                    process = Process.Start(info);
                });
            }
            catch(Exception)
            {
                var result = MessageBox.Show("Adobe reader nebyl nalezen. Chcete jej stáhnout?", "Chyba", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(new Uri("https://get.adobe.com/cz/reader/").AbsoluteUri));
                }
            }
        }

        private void ShowDocument_Click(Object sender, RoutedEventArgs e) => OpenDocument(sender);

        private void AddNewDocument_Click(Object sender, RoutedEventArgs e)
        {
            var filePicker = new OpenFileDialog
            {
                Filter = "All files (*.*)|*.*"
            };
            var newDocument = filePicker.ShowDialog();
            if(newDocument == false)
            {
                return;
            }
            var LocalDocumentPath = CopyDocumentLocaly(filePicker.FileName);
            var documentName = Path.GetFileNameWithoutExtension(filePicker.FileName);
            AssignedDocuments.Add(new Document(documentName, LocalDocumentPath));
            RefreshDocumentList();
        }

        private String CopyDocumentLocaly(String fileName)
        {
            var destPath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "MoneyManagerData", "Documents", Path.GetFileName(fileName));
            try
            {
                File.Copy(fileName, destPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
            return destPath;
        }

        private void RefreshDocumentList()
        {
            DocumentsList.ItemsSource = null;
            DocumentsList.ItemsSource = AssignedDocuments;
        }

        private void DeleteDocument_Click(Object sender, RoutedEventArgs e)
        {
            if(DocumentsList.SelectedItem == null)
            {
                MessageBox.Show("Vyber položku", "Chyba", MessageBoxButton.OK);
                return;
            }
            var selectedItem = DocumentsList.SelectedItem as Document;
            AssignedDocuments.Remove(selectedItem);
            DeleteDocumentLocaly(selectedItem);
            RefreshDocumentList();
        }

        private void DeleteDocumentLocaly(Document selectedItem)
        {
            try
            {
                File.Delete(selectedItem.Path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK);
            }
        }
    }
}
