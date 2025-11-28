
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfAppProV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string rutaArchLogin = "c://LOGINS//loginsPro.txt";
        public MainWindow()
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



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

    
            string usuarioIngresado = txtUser.Text.Trim();
            string contraseniaIngresado = txtpass.Password.Trim();

            bool encontrado = false;

            foreach (string linea in File.ReadAllLines(rutaArchLogin, Encoding.UTF8))
            {
                string[] datos = linea.Split(',');

                string correo = datos[1];
                string pwd = datos[3];

                if (usuarioIngresado == correo && contraseniaIngresado == pwd)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                MessageBox.Show("Usuario encontrado, pasaste bro!");
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