using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class PanelSuperAdmin : Window
    {
        public PanelSuperAdmin()
        {
            InitializeComponent();

            // Conectar botones a eventos
          
            btnAdmins.Click += BtnAdmins_Click;
            btnProveedores.Click += BtnProveedores_Click;
            btnInventario.Click += BtnInventario_Click;
            btnClientes.Click += BtnClientes_Click;
            //btnLogout.Click += btnLogout_Click;
        }

        // ABRIR ADMINISTRADORES
        private void BtnAdmins_Click(object sender, RoutedEventArgs e)
        {
            VerAdmins admins = new VerAdmins();
            admins.Show();
            this.Hide(); // ocultar panel mientras se abre ventana
        }

        // ABRIR PROVEEDORES
        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            VerProveedores proveedores = new VerProveedores();
            proveedores.Show();
            this.Hide();
        }

        // ABRIR INVENTARIO
        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto inventario = new RegistrarProducto();
            inventario.Show();
            this.Hide();
        }

        // ABRIR CLIENTES FRECUENTES
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para manejar el clic en el botón "Clientes Frecuentes"
            MessageBox.Show("Botón Clientes Frecuentes pulsado.");
        }

        // CERRAR SESIÓN
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Aquí puedes agregar la lógica para cerrar la sesión o cerrar la ventana
            this.Close();
        }

        // MÉTODO PARA VOLVER DESDE VENTANAS HIJAS
        public static void VolverAlPanel(Window ventanaHija)
        {
            ventanaHija.Close();
            var panel = Application.Current.Windows.OfType<PanelSuperAdmin>().FirstOrDefault();
            if (panel != null)
            {
                panel.Show();
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();    
        }

        private void btnClientes_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}