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

namespace WpfAppProV2
{           
    /// <summary>
    /// Lógica de interacción para RegistrarClienteF.xaml
    /// </summary>
    public partial class RegistrarClienteF : Window
    {
        public RegistrarClienteF()          
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            VerClienteF verClienteF = new VerClienteF();
            verClienteF.Show();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }   
  
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.Close(); }
    }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
