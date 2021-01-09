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
    /// Interaction logic for ConsultasShow.xaml
    /// </summary>
    public partial class ConsultasShow : Window
    {
        static int id;
        static string token;
        public ConsultasShow(int iduser, string token1)
        {
            InitializeComponent();
            id = iduser;
            token = token1;

            dtConsultas.ItemsSource = ConsultaController.TakeConsultas(id).DefaultView;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Main = new MainWindow(id, token);
            Main.Closed += (s, args) => this.Close();
            Main.Show();
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            int idcon = int.Parse(dtConsultas.SelectedValue.ToString());
            int result=ConsultaController.DeleteConsulta(idcon,token);
            if (result == 1)
            {
                dtConsultas.ItemsSource = ConsultaController.TakeConsultas(id).DefaultView;
            }
            else
                MessageBox.Show("Nao Apagou");
        }
    }
}
