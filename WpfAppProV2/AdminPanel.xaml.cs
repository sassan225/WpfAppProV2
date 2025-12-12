using System;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class AdminPanel : Window
    {
        private string _rolUsuario;

        // Constructor recibe rol del usuario
        public AdminPanel(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;
        }

        // Permitir mover la ventana arrastrando
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Cerrar aplicación
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Cerrar sesión y volver al login
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        // Abrir ventana para registrar productos
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

        // Abrir ventana para ver productos registrados
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

        // Abrir ventana para registrar proveedores
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

        // Abrir ventana para ver proveedores registrados
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

        // Generar código de Admin
        private void BtnGenerarCodigoAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                string codigo = random.Next(100000, 999999).ToString();
                MessageBox.Show($"Código de Admin generado: {codigo}", "Código Admin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Error al generar código de admin.");
            }
        }

        // Abrir ventana para registrar cliente frecuente
        private void btnRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrarClienteF ventana = new RegistrarClienteF(_rolUsuario);
                ventana.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error al abrir la ventana de registro de clientes frecuentes.");
            }
        }

        // Abrir ventana para ver clientes frecuentes
        private void btnVerClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VerClienteF ventana = new VerClienteF(_rolUsuario);
                ventana.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error al abrir la ventana de clientes frecuentes.");
            }
        }
    }
}
