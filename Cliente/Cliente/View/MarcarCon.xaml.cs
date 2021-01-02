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
using Cliente.Controller;


namespace Cliente.View
{
    /// <summary>
    /// Interaction logic for MarcarCon.xaml
    /// </summary>
    public partial class MarcarCon : Window
    {
        public MarcarCon()
        {
            InitializeComponent();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> conv = new List<string>();

            foreach (string val in conv)
            {
                ComboBoxConv.Items.Add(val);
            }
        }

        private void ComboBoxLoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxLoc.Items.Add("Gabinete 1");
            ComboBoxLoc.Items.Add("Gabinete 2");
            ComboBoxLoc.Items.Add("Domícilio");
        }

        private void ComboBoxProf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxProf.Items.Add("Quim");
            ComboBoxProf.Items.Add("Alberto");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Main = new MainWindow();
            Main.Closed += (s, args) => this.Close();
            Main.Show();
        }
    }
}
