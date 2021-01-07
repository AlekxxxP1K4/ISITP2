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

namespace Cliente.View
{
    /// <summary>
    /// Interaction logic for DocWindow.xaml
    /// </summary>
    public partial class DocWindow : Window
    {
        static int idloged;
        static string token;
        public DocWindow(int id,string token1)
        {
            InitializeComponent();
            idloged = id;
            token = token1;

        }
    }
}
