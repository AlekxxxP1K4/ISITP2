//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>
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
        static string token;
        public MainWindow()
        {
            
            InitializeComponent();
            lbl_UserName.Content = LoginController.namelogedin(14);
        }
        public MainWindow(int id,string token1)
        {
            InitializeComponent();
            lbl_UserName.Content=LoginController.namelogedin(id);
            iduser = id;
            token = token1;
            //MessageBox.Show(iduser.ToString() +"\n" + token);
        }


        private void Button_ClickMarcarConsulta(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Cons = new MarcarCon(iduser,token);
            Cons.Closed += (s, args) => this.Close();
            Cons.Show();
        }

        private void Button_ClickVerConsultas(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var showConsultas = new ConsultasShow(iduser, token);
            showConsultas.Closed += (s, args) => this.Close();
            showConsultas.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var update = new UpdateUser(iduser, token);
            update.Closed += (s, args) => this.Close();
            update.Show();
        }
    }
}
