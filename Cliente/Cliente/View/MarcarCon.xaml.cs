//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>
using System;
using System.Collections.Generic;
using System.Data;
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
        static int iduser;
        static string token;
        DataTable dt = new DataTable();
        public MarcarCon(int idpessoa,string token1)
        {
            InitializeComponent();
            ComboBoxLoc.Items.Add("Gabinete");
            ComboBoxLoc.Items.Add("Domícilio");
            ComboBoxConv.ItemsSource = ConsultaController.TakeConvencao().DefaultView;
            ComboBoxProf.ItemsSource = ConsultaController.TakeMedicos().DefaultView;
            ComboBoxTipoCon.ItemsSource = ConsultaController.TakeTipoConsulta().DefaultView;
            DataCalendary.DisplayDateStart = DateTime.Today;
            iduser = idpessoa;
            token = token1;
            ConsultaController.token = token;


        }
        public MarcarCon()
        {
            InitializeComponent();
            ComboBoxLoc.Items.Add("Gabinete");
            ComboBoxLoc.Items.Add("Domícilio");
            ComboBoxConv.ItemsSource = ConsultaController.TakeConvencao().DefaultView;
            ComboBoxProf.ItemsSource = ConsultaController.TakeMedicos().DefaultView;
            ComboBoxTipoCon.ItemsSource = ConsultaController.TakeTipoConsulta().DefaultView;
            DataCalendary.DisplayDateStart = DateTime.Today;
            iduser = 14;


        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        

        private void Button_ClickMarcar(object sender, RoutedEventArgs e)
        {
            string desc;
            if (ComboBoxLoc.SelectedIndex == -1 || ComboBoxProf.SelectedIndex == -1 || ComboBoxTipoCon.SelectedIndex == -1 || ComboBoxConv.SelectedIndex == -1 || DataCalendary.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Falta Preencher");
            }
            else
            {
                if (ComboBoxLoc.SelectedItem.ToString() == "Gabinete")
                {
                    desc= TextDescricao.Text + " - Gabinete";
                }
                else
                {
                    desc = TextDescricao.Text;
                }

               bool aux=ConsultaController.Marcar(iduser, (int)ComboBoxProf.SelectedValue, (int)ComboBoxConv.SelectedValue, (int)ComboBoxTipoCon.SelectedValue, desc, DataCalendary.SelectedDate.Value.Date);

                if (aux)
                {
                    MessageBox.Show("Consulta Marcada");
                    this.Hide();
                    var Main = new MainWindow(iduser,token);
                    Main.Closed += (s, args) => this.Close();
                    Main.Show();
                }
                else MessageBox.Show("Consulta nao marcada");
            }
            
        }

        private void Button_ClickBack(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var Main = new MainWindow(iduser,token);
            Main.Closed += (s, args) => this.Close();
            Main.Show();
        }
    }
}
