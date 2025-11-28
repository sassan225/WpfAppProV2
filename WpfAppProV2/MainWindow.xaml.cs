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
            bool encontrado = false;

            try
            {
                foreach (string linea in File.ReadAllLines(rutaArchLogin, Encoding.UTF8))
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length < 4)
                        continue;

                    string correo = datos[1];
                    string pwd = datos[3];

                    if (usuarioIngresado == correo && contraseniaIngresado == pwd)
                    {
                        encontrado = true;
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error al leer el archivo de logins.");
                return;
            }

            if (encontrado)
            {
                MessageBox.Show("¡Login exitoso!!.");
                Welcome welcome = new Welcome(); 
                welcome.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
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
