using System.Windows;
using Glimpse.Mvc.Tab;

namespace WpfAppProV2
{
    public partial class PanelSuperAdmin : Window
    {
        public PanelSuperAdmin()
        {
            InitializeComponent();

            // Asignar eventos normales
            btnLogout.Click += BtnLogout_Click;
            btnAdmins.Click += BtnAdmins_Click;
            btnProveedores.Click += BtnProveedores_Click;
            btnInventario.Click += BtnInventario_Click;
            btnClientes.Click += BtnClientes_Click;
        }

        private void BtnAdmins_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new Views.AdminsPage());
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new Views.ProveedoresPage());
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new Views.InventarioPage());
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new Views.ClientesPage());
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void LoadContent(UIElement element)
        {
            ContentArea.Child = element;
        }
    }
}
