using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para Welcome.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto ventana = new RegistrarProducto();
            ventana.Show();
            this.Hide();
        }

        private void btnVerProductos_Click(object sender, RoutedEventArgs e)
        {
            VerProductos ventana = new VerProductos();
            ventana.Show();
            this.Hide();
        }

        private void btnRegistrarProveedor_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProveedor ventana = new RegistrarProveedor();
            ventana.Show();
            this.Hide();
        }

        private void btnVerProveedores_Click(object sender, RoutedEventArgs e)
        {
            VerProveedores ventana = new VerProveedores();
            ventana.Show();
            this.Hide();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
