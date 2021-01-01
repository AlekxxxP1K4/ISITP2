using Nancy.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            int resposta = LoginController.login(User.Text.ToString(), Pass.Password.ToString());
            if (resposta > 0)
            {

                int role = LoginController.role(resposta);
                switch (role)
                {
                    case 1:  //utente
                        this.Hide();
                        var Main = new MainWindow(resposta);
                        Main.Closed += (s, args) => this.Close();
                        Main.Show();
                        break;
                    case 2:  //doutor
                        this.Hide();
                        //var Doc = new DocWindow(resposta);
                        //Doc.Closed += (s, args) => this.Close();
                        //Doc.Show();
                        break;
                    case 3:   //funcionario
                        this.Hide();
                        // var Func = new FuncWindow(resposta);
                        //Func.Closed += (s, args) => this.Close();
                        //Func.Show();
                        break;
                    case 4:   //admin
                        this.Hide();
                        // var Adm = new AdminWindow(resposta);
                        //Adm.Closed += (s, args) => this.Close();
                        //Adm.Show();
                        break;
                    default:
                        MessageBox.Show("Algo errado!");
                        break;
                }
            }
            else if (resposta == 0)
            {
                MessageBox.Show("Sem ligaçao ao Servidor");
            }
            else if (resposta == -1)
            {
                MessageBox.Show("Password errada");
            }
            else
            {
                MessageBox.Show("Utilizador não existe");
            }

        }

        private void ButtonRegistar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Reg = new Registar();
            Reg.Closed += (s, args) => this.Close();
            Reg.Show();
        }
    }

}
