using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class SignUp : Window
    {
        // Ruta centralizada
        private readonly string rutaArchLogin = @"C:\cosmetiqueSoftware\loginsPro.txt";

        public SignUp()
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

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtName.Text.Trim();
            string apellido = txtApp.Text.Trim();
            string contrasenia = txtpwd.Password.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(contrasenia))
            {
                MessageBox.Show("DEBES LLENAR TODOS LOS CAMPOS!!");
                return;
            }

            // Validar solo letras para nombre y apellido


            string letterPattern = @"^[a-zA-Z]+$";
            if (!Regex.IsMatch(nombre, letterPattern) || !Regex.IsMatch(apellido, letterPattern))
            {
                MessageBox.Show("Nombre y apellido solo deben contener letras.");
                return;
            }

            
            string passPattern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*[\d]).+$";
            if (!Regex.IsMatch(contrasenia, passPattern))
            {
                MessageBox.Show("La contraseña debe tener al menos una mayúscula, minúscula y un número.");
                return;
            }

         
            string correo = nombre.ToLower() + "." + apellido.ToLower() + "@cosmetique.com";

         
            string datos = string.Join(",", nombre, correo, "ADMIN", contrasenia) + Environment.NewLine;
            File.AppendAllText(rutaArchLogin, datos, Encoding.UTF8);

            MessageBox.Show("Admin registrado correctamente!!\nCorreo generado: " + correo);

            // Volver al login
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
