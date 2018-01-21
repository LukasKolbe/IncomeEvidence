using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace IncomeEvidenceOSVC
{
    /// <summary>
    /// Interaction logic for SavesManager.xaml
    /// </summary>
    public partial class SavesManager : Window
    {
        private static SavesManager dialogWindow;
        public SavesManager() => InitializeComponent();

        internal static void Show(List<Tuple<String, String>> data)
        {
            dialogWindow = new SavesManager();
            dialogWindow.SavesList.ItemsSource = data;
            dialogWindow.ShowDialog();
        }

        private void OpenSave_Click(Object sender, RoutedEventArgs e)
        {
            if(SavesList.SelectedItem==null)
            {
                MessageBox.Show("Vyber položku", "Chyba", MessageBoxButton.OK);
            }
            else
            {
                var data = SavesList.SelectedItem as Tuple<String,String>;
                App.SetFileName(data.Item2);
            }
            Close();
        }

        private void Cancel_Click(Object sender, RoutedEventArgs e) => Close();

        private void ListViewItem_MouseDoubleClick(Object sender, MouseButtonEventArgs e) => OpenSave_Click(null, null);
    }
}
