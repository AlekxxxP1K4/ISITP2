﻿using System;
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
        DataTable dt = new DataTable();
        public MarcarCon()
        {
            InitializeComponent();
            ComboBoxLoc.Items.Add("Gabinete 1");
            ComboBoxLoc.Items.Add("Gabinete 2");
            ComboBoxLoc.Items.Add("Domícilio");
            ComboBoxProf.Items.Add("Quim");
            ComboBoxProf.Items.Add("Alberto");
            MessageBox.Show(ConsultaController.TakeConvencao());
            
            
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

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
