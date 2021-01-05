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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cliente.Controller;

namespace Cliente.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int iduser;
        public MainWindow()
        {
            
            InitializeComponent();
            lbl_UserName.Content = LoginController.namelogedin(14);
        }
        public MainWindow(int id)
        {
            InitializeComponent();
            lbl_UserName.Content=LoginController.namelogedin(id);
            iduser = id;
        }


        private void Button_ClickMarcarConsulta(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Cons = new MarcarCon(iduser);
            Cons.Closed += (s, args) => this.Close();
            Cons.Show();
        }

        private void Button_ClickVerConsultas(object sender, RoutedEventArgs e)
        {

        }
    }
}
