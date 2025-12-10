using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class PanelSuperAdmin : Window
    {
        private readonly string _rolUsuario = "superadmin"; // Rol de este panel

        public PanelSuperAdmin()
        {
            InitializeComponent();

            // Conectar botones a eventos
            btnAdmins.Click += BtnAdmins_Click;
            btnProveedores.Click += BtnProveedores_Click;
            btnInventario.Click += BtnInventario_Click;
            btnClientes.Click += BtnClientes_Click;
            btnCerrarSesion.Click += BtnLogout_Click;
        }

        // Abrir ventana de administradores
        private void BtnAdmins_Click(object sender, RoutedEventArgs e)
        {
            VerAdmins ventana = new VerAdmins();
            ventana.Show();
            this.Hide();
        }

        // Abrir ventana de proveedores
        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            VerProveedores ventana = new VerProveedores(_rolUsuario);
            ventana.Show();
            this.Hide();
        }

        // Abrir ventana de inventario
        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto ventana = new RegistrarProducto(_rolUsuario);
            ventana.Show();
            this.Hide();
        }

        // Abrir ventana de clientes frecuentes
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            VerClienteF ventana = new VerClienteF(_rolUsuario);
            ventana.Show();
            this.Hide();
        }

        // Permite mover la ventana arrastrando con el mouse
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Cerrar sesión y volver al login
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        // Método opcional para volver desde ventanas hijas
        public static void VolverAlPanel(Window ventanaHija)
        {
            ventanaHija.Close();
            var panel = Application.Current.Windows
                                     .OfType<PanelSuperAdmin>()
                                     .FirstOrDefault();
            panel?.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void btnClientes_Click_1(object sender, RoutedEventArgs e)
        {
            VerClienteF ventana = new VerClienteF(_rolUsuario);
            ventana.Show();
            this.Hide();
        }
    }
}
