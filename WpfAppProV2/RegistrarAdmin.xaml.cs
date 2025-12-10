using System;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para RegistrarAdmin.xaml
    /// </summary>
    public partial class RegistrarAdmin : Window
    {
        public RegistrarAdmin()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
            
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para guardar el nuevo admin
            // Por ejemplo:
            // string nombre = txtNombre.Text;
            // string correo = txtCorreo.Text;
            // string contrasena = txtContrasena.Password;
            // Aquí puedes agregar la lógica de guardado según tu aplicación
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            PanelSuperAdmin panelSuperAdmin = new PanelSuperAdmin();
            panelSuperAdmin.Show();
            this.Close();
        }
    }
}
