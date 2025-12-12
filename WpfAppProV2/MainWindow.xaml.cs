using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class MainWindow : Window
    {
        private readonly string rutaSuperadmins = @"C:\cosmetiqueSoftware\superadmins.txt";
        private readonly string rutaAdmins = @"C:\cosmetiqueSoftware\loginsPro.txt";

        public MainWindow()
        {
            InitializeComponent();

            string carpeta = @"C:\cosmetiqueSoftware";
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            if (!File.Exists(rutaSuperadmins))
                File.WriteAllText(rutaSuperadmins, "Super,Admin,N/A,N/A,N/A,1234,SUPERADMIN,super@cosmetique.com", Encoding.UTF8);

            if (!File.Exists(rutaAdmins))
                File.WriteAllText(rutaAdmins, string.Empty, Encoding.UTF8);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void btnCerrar_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUser.Text.Trim();
            string contrasenia = txtpass.Password.Trim();

            if (ValidarLogin(rutaSuperadmins, usuario, contrasenia, out string rol, out string nombre) ||
                ValidarLogin(rutaAdmins, usuario, contrasenia, out rol, out nombre))
            {
                MessageBox.Show($"Bienvenido {nombre} ({rol})", "Login exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                App.Current.Properties["RolUsuario"] = rol;
                App.Current.Properties["NombreUsuario"] = nombre;

                if (rol == "SUPERADMIN")
                    new PanelSuperAdmin(rol).Show();
                else
                    new AdminPanel(rol).Show();

                Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de login", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidarLogin(string rutaArchivo, string usuario, string contrasenia, out string rol, out string nombre)
        {
            rol = null;
            nombre = null;

            try
            {
                string[] lineas = File.ReadAllLines(rutaArchivo, Encoding.UTF8);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    if (datos.Length < 8) continue;

                    string n = datos[0].Trim();
                    string ap = datos[1].Trim();
                    string am = datos[2].Trim();
                    string contr = datos[5].Trim();
                    string r = datos[6].Trim();
                    string correo = datos[7].Trim();

                    if (usuario == correo && contrasenia == contr)
                    {
                        rol = r;
                        nombre = $"{n} {ap} {am}";
                        return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show($"Error al leer el archivo: {rutaArchivo}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            new SignUp().Show();
            Close();
        }
    }
}
