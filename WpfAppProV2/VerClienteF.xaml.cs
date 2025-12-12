using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class VerClienteF : Window
    {
        private readonly string _rolUsuario;
        private readonly string _carpetaBase = @"C:\cosmetiqueSoftware";
        private readonly string _rutaArchivo;
        private ObservableCollection<ClienteF> _listaClientes;

        public VerClienteF(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            if (!Directory.Exists(_carpetaBase))
            {
                Directory.CreateDirectory(_carpetaBase);
            }

            _rutaArchivo = Path.Combine(_carpetaBase, "clientesF.txt");
            _listaClientes = new ObservableCollection<ClienteF>();
            dgClientes.ItemsSource = _listaClientes;

            CargarClientes();
        }

        private void CargarClientes()
        {
            _listaClientes.Clear();

            if (!File.Exists(_rutaArchivo))
                return;

            string[] lineas = File.ReadAllLines(_rutaArchivo);
            for (int i = 0; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split(',');
                if (datos.Length >= 4) // Id, Nombre, Correo, Telefono
                {
                    ClienteF cliente = new ClienteF();
                    cliente.IdCliente = int.Parse(datos[0]);
                    cliente.Nombre = datos[1];
                    cliente.Correo = datos[2];
                    cliente.Telefono = datos[3];
                    _listaClientes.Add(cliente);
                }
            }
        }

        private void GuardarClientes()
        {
            string[] lineas = new string[_listaClientes.Count];
            for (int i = 0; i < _listaClientes.Count; i++)
            {
                ClienteF c = _listaClientes[i];
                lineas[i] = c.IdCliente + "," + c.Nombre + "," + c.Correo + "," + c.Telefono;
            }
            File.WriteAllLines(_rutaArchivo, lineas);
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarClienteF ventana = new RegistrarClienteF(_rolUsuario); // Se pasa la referencia de la ventana actual
            ventana.Show();
            this.Hide();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem != null)
            {
                ClienteF seleccionado = (ClienteF)dgClientes.SelectedItem;

                MessageBoxResult resultado = MessageBox.Show(
                    "¿Deseas borrar a " + seleccionado.Nombre + "?",

                    "Confirmar",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    _listaClientes.Remove(seleccionado);
                    GuardarClientes();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente primero.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            }
            else
            {
                ventanaDestino = new AdminPanel(_rolUsuario);
            }

            ventanaDestino.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Método público para agregar un cliente desde RegistrarClienteF
        public void AgregarCliente(ClienteF nuevoCliente)
        {
            _listaClientes.Add(nuevoCliente); // Agrega el cliente a la ObservableCollection
            GuardarClientes();
        }
    }
}
