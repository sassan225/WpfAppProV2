using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class SignUp : Window
    {
        private readonly string rutaAdmins = @"C:\cosmetiqueSoftware\loginsPro.txt";
        private readonly string rutaCodigos = @"C:\cosmetiqueSoftware\adminCodes.txt";

        public SignUp()
        {
            InitializeComponent();
            string carpeta = Path.GetDirectoryName(rutaAdmins);
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
            if (!File.Exists(rutaAdmins)) File.WriteAllText(rutaAdmins, string.Empty, Encoding.UTF8);
            if (!File.Exists(rutaCodigos)) File.WriteAllText(rutaCodigos, string.Empty, Encoding.UTF8);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtName.Text.Trim();
            string apellidoP = txtApp.Text.Trim();
            string apellidoM = txtApm.Text.Trim();
            string telefono = txtPhone.Text.Trim();
            string year = txtYear.Text.Trim();
            string contrasenia = txtpwd.Password.Trim();
            string regCode = txtRegCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) ||
                string.IsNullOrWhiteSpace(apellidoM) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(contrasenia) ||
                string.IsNullOrWhiteSpace(regCode))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (!Regex.IsMatch(nombre + apellidoP + apellidoM, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Nombre y apellidos solo deben contener letras.");
                return;
            }

            if (!Regex.IsMatch(telefono, @"^\d+$"))
            {
                MessageBox.Show("El teléfono solo debe contener números.");
                return;
            }

            if (!int.TryParse(year, out int anioNacimiento) || anioNacimiento < 1940 || anioNacimiento > DateTime.Now.Year - 10)
            {
                MessageBox.Show("Año de nacimiento inválido.");
                return;
            }

            if (!Regex.IsMatch(contrasenia, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            {
                MessageBox.Show("La contraseña debe contener mayúscula, minúscula y número.");
                return;
            }

            // Validar código sin lambda
            string[] codigos = File.ReadAllLines(rutaCodigos);
            bool codigoValido = false;
            for (int i = 0; i < codigos.Length; i++)
            {
                if (codigos[i] == regCode)
                {
                    codigoValido = true;
                    break;
                }
            }

            if (!codigoValido)
            {
                MessageBox.Show("Código de registro inválido o expirado.");
                return;
            }

            string correo = nombre.ToLower() + "." + apellidoP.ToLower() + "@cosmetique.com";

            string datos = string.Join(",", nombre, apellidoP, apellidoM, telefono, anioNacimiento, contrasenia, "ADMIN", correo);
            File.AppendAllText(rutaAdmins, datos + Environment.NewLine, Encoding.UTF8);


            string[] nuevosCodigos = new string[0];
            int count = 0;
            for (int i = 0; i < codigos.Length; i++)
            {
                if (codigos[i] != regCode)
                {
                    Array.Resize(ref nuevosCodigos, count + 1);
                    nuevosCodigos[count] = codigos[i];
                    count++;
                }
            }
            File.WriteAllLines(rutaCodigos, nuevosCodigos);

            MessageBox.Show("Admin registrado correctamente.\nCorreo generado: " + correo);
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
