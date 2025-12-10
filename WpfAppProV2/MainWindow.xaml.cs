using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class MainWindow : Window
    {
        private readonly string rutaArchLogin = @"C:\cosmetiqueSoftware\loginsPro.txt";

        public MainWindow()
        {
            InitializeComponent();

            // Crear carpeta y archivo si no existen
            string carpeta = Path.GetDirectoryName(rutaArchLogin);
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            if (!File.Exists(rutaArchLogin))
                File.WriteAllText(rutaArchLogin, string.Empty, Encoding.UTF8);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuarioIngresado = txtUser.Text.Trim();
            string contraseniaIngresado = txtpass.Password.Trim();

            string rolUsuario = null;
            string nombreUsuario = null;

            try
            {
                foreach (string linea in File.ReadAllLines(rutaArchLogin, Encoding.UTF8))
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length < 4) continue;

                    string nombre = datos[0];
                    string correo = datos[1];
                    string rol = datos[2];
                    string pwd = datos[3];

                    if (usuarioIngresado == correo && contraseniaIngresado == pwd)
                    {
                        rolUsuario = rol;
                        nombreUsuario = nombre;
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo de logins.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (rolUsuario != null)
            {
                MessageBox.Show($"Bienvenido {nombreUsuario} ({rolUsuario})", "Login exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Abrir ventana según rol
                if (rolUsuario == "SUPERADMIN")
                {
                    PanelSuperAdmin panel = new PanelSuperAdmin();
                    panel.Show();
                }
                else if (rolUsuario == "ADMIN")
                {
                    AdminPanel panel = new AdminPanel();
                    panel.Show();
                }
                //else if (rolUsuario == "CLIENTE")
                //{
                //    PanelCliente panel = new PanelCliente();
                //    panel.Show();
                //}

                this.Close();
            }
            else
            {
                MessageBox.Show("⚠ Usuario o contraseña incorrectos.");
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Close();
        }
    }
}
