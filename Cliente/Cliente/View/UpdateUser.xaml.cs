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
    /// Interaction logic for UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Window
    {
        static int id;
        static string token;
        public UpdateUser(int idloged,string token1)
        {
            InitializeComponent();
            id = idloged;
            token = token1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pwlast.Text == "" || pwnew.Password=="")
            {
                MessageBox.Show("Falta Preencher");
            }
            else if (pwnew.Password == pwnewrep.Password)
            {
                var result=RegistarController.UpdatePw(id, pwlast.Text, pwnew.Password,token);
                MessageBox.Show(result);
                //if (result)
                //{
                //    MessageBox.Show("Sucesso");
                //    this.Hide();
                //    var Main = new MainWindow(id, token);
                //    Main.Closed += (s, args) => this.Close();
                //    Main.Show();
                //}
            }
            else MessageBox.Show("Palavra pass nao corresponde");
        }

        private void Button_ClickBack(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Main = new MainWindow(id, token);
            Main.Closed += (s, args) => this.Close();
            Main.Show();
        }
    }
}
