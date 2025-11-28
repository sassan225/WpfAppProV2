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
using System.IO;
using System.Text.RegularExpressions;

namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private readonly string rutaArchLogin = "c://LOGINS//loginsPro.txt";
        public SignUp()
        {
            InitializeComponent();
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

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtName.Text.Trim();
            string apellidoP = txtApp.Text.Trim();
            string apellidoM = txtApm.Text.Trim();
            string celular = txtPhone.Text.Trim();
            string contrasenia = txtpwd.Password.Trim();
            string cumple = txtYear.Text.Trim();
            string correo = " ";

            string letterPatern = @"^[a-zA-Z]+$";
            string numericPatern = @"^[0-9]{8}$";
            string yearPattern = "^(19[5-9]\\d|20[0-1]\\d|202[0-5])$";


            //string passPatern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*[/d])(?=.*[!@#$%^&*]).+$";
            string passPatern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*[\d]).+$";

            if (nombre == "" || apellidoP == "" || apellidoM == "" || celular == "" || contrasenia == "")
            {

                MessageBox.Show("DEBES LLENAR TODOS LOS CAMPOS!!");
                return;

            }

            if (!Regex.IsMatch(nombre, letterPatern))
            {
                MessageBox.Show("El nombre no puede incluir números. ");
                return;
            }

            if (!Regex.IsMatch(apellidoP, letterPatern))
            {
                MessageBox.Show("El apellido no puede incluir números. ");
                return;
            }
            if (!Regex.IsMatch(apellidoM, letterPatern))
            {
                MessageBox.Show("El apellido no puede incluir números. ");
                return;
            }
            if (!Regex.IsMatch(celular, numericPatern))
            {
                MessageBox.Show("Formato incorrecto del número. Debe tener 8 números.");
                return;
            }
            if (!Regex.IsMatch(contrasenia, passPatern))
            {
                MessageBox.Show("La contraseña debe tener por lo menos una mayuscula, minuscula, y un número.");
                return;
            }
            if (!Regex.IsMatch(cumple, yearPattern))
            {
                MessageBox.Show("INGRESE UN CUMPLEAÑOS VALIDO");
                return;
            }



            correo = nombre.ToLower()[0] + apellidoP.ToLower() + apellidoM.ToLower()[0] + "@est.univalle.edu";


            string datos = string.Join(",", nombre, correo, celular, contrasenia, cumple) + Environment.NewLine;

            File.AppendAllText(rutaArchLogin, datos, Encoding.UTF8);

            MessageBox.Show("Registro guardado correctamente!!");



        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
