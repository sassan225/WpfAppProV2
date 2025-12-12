using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class PanelSuperAdmin : Window
    {
        private readonly string _rolUsuario;
        private readonly string rutaCodigos = @"C:\cosmetiqueSoftware\adminCodes.txt";

        public PanelSuperAdmin(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

        }

        // --- Eventos de ventana ---
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Cerrar sesión y volver al login
        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        // --- Abrir otras ventanas ---
        private void AbrirVentana(Window ventana)
        {
            ventana.Show();
            Hide();
        }

        private void BtnRegistrarAdmin_Click(object sender, RoutedEventArgs e)
        {
            RegistrarAdmin ventana = new RegistrarAdmin(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnVerAdmins_Click(object sender, RoutedEventArgs e)
        {
            VerAdmins ventana = new VerAdmins(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            RegistrarClienteF ventana = new RegistrarClienteF(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnVerClientes_Click(object sender, RoutedEventArgs e)
        {
            VerClienteF ventana = new VerClienteF(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto ventana = new RegistrarProducto(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnVerProductos_Click(object sender, RoutedEventArgs e)
        {
            VerProductos ventana = new VerProductos(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnRegistrarProveedor_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProveedor ventana = new RegistrarProveedor(_rolUsuario);
            AbrirVentana(ventana);
        }

        private void BtnVerProveedores_Click(object sender, RoutedEventArgs e)
        {
            VerProveedores ventana = new VerProveedores(_rolUsuario);
            AbrirVentana(ventana);
        }

        // --- Generar código de admin ---
   

        private string GenerarCodigoAdmin(int longitud = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            char[] codigo = new char[longitud];

            for (int i = 0; i < longitud; i++)
            {
                codigo[i] = chars[rnd.Next(chars.Length)];
            }

            return new string(codigo);
        }

        private void BtnGenerarCodigoAdmin_Click(object sender, RoutedEventArgs e)
        {
            string codigo = GenerarCodigoAdmin();

            if (!File.Exists(rutaCodigos))
                File.WriteAllText(rutaCodigos, string.Empty, Encoding.UTF8);

            File.AppendAllText(rutaCodigos, codigo + Environment.NewLine, Encoding.UTF8);
            MessageBox.Show("Código de registro generado: " + codigo, "Código Admin", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
