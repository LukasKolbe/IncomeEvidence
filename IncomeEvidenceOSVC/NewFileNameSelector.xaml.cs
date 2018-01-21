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
    /// Interaction logic for NewFileNameSelector.xaml
    /// </summary>
    public partial class NewFileNameSelector : Window
    {
        private static NewFileNameSelector dialogWindow;
        public NewFileNameSelector()
        {
            InitializeComponent();
            FileNameTbx.Focus();
        }

        internal new static void Show()
        {
            dialogWindow = new NewFileNameSelector();
            dialogWindow.ShowDialog();
        }

        private void CreateNewFile_Click(Object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(FileNameTbx.Text))
            {
                MessageBox.Show("Název nesmí být prázdný", "Chyba", MessageBoxButton.OK);
                return;
            }
            App.SetFileName(FileNameTbx.Text+".json");
            Close();
        }
    }
}
