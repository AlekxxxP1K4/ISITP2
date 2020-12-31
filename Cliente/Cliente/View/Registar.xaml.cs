using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Registar.xaml
    /// </summary>
    public partial class Registar : Window
    {
        string defaultText;
        public Registar()
        {
            InitializeComponent();
        }
        #region Focus
        private void Utilizador_GotFocus(object sender, RoutedEventArgs e)
        {
            defaultText = "Utilizador";
            Utilizador.Text = Utilizador.Text == defaultText ? string.Empty : Utilizador.Text;
        }
        
        private void Nome_GotFocus(object sender, RoutedEventArgs e)
        {
            defaultText = "Nome Completo";
            Nome.Text = Nome.Text == defaultText ? string.Empty : Nome.Text;
        }

        private void Nif_GotFocus(object sender, RoutedEventArgs e)
        {
            defaultText = "NIF";
            Nif.Text = Nif.Text == defaultText ? string.Empty : Nif.Text;
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            defaultText = "Email";
            Email.Text = Email.Text == defaultText ? string.Empty : Email.Text;
        }

        private void Morada_GotFocus(object sender, RoutedEventArgs e)
        {
            defaultText = "Morada";
            Morada.Text = Morada.Text == defaultText ? string.Empty : Morada.Text;
        }

        private void Contacto_GotFocus(object sender, RoutedEventArgs e)
        {
            defaultText = "Contacto";
            Contacto.Text = Contacto.Text == defaultText ? string.Empty : Contacto.Text;
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_pass.Content = "";
        }

        private void Password_Copy_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_passr.Content = "";
        }
        #endregion

        private void Nif_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
