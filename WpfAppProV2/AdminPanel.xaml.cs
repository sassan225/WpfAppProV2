using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class AdminPanel : Window
    {
        private string _rolUsuario;
        private string _nombreUsuario;

        // Constructor recibe rol y nombre del usuario
        public AdminPanel(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            // Mostrar bienvenida personalizada
            WelcomeText.Text = "WELCOME, " + _nombreUsuario.ToUpper() + "!";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void btnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrarProducto ventana = new RegistrarProducto(_rolUsuario);
                ventana.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error al abrir la ventana de registro de productos.");
            }
        }

        private void btnVerProductos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VerProductos ventana = new VerProductos(_rolUsuario);
                ventana.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error al abrir la ventana de productos registrados.");
            }
        }

        private void btnRegistrarProveedor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrarProveedor ventana = new RegistrarProveedor(_rolUsuario);
                ventana.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error al abrir la ventana de registro de proveedores.");
            }
        }

        private void btnVerProveedores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VerProveedores ventana = new VerProveedores(_rolUsuario);
                ventana.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error al abrir la ventana de proveedores registrados.");
            }
        }
        private void BtnGenerarCodigoAdmin_Click(object sender, RoutedEventArgs e)
        {
          
            var random = new Random();
            string codigo = random.Next(100000, 999999).ToString();

          
            MessageBox.Show($"Código de Admin generado: {codigo}", "Código Admin", MessageBoxButton.OK, MessageBoxImage.Information);


        }

    }
}
