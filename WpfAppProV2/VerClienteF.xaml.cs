using System;
using System.Collections.Generic;
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
        private List<ClienteF> _listaClientes = new List<ClienteF>();

        public VerClienteF(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            if (!Directory.Exists(_carpetaBase))
                Directory.CreateDirectory(_carpetaBase);

            _rutaArchivo = Path.Combine(_carpetaBase, "clientesF.txt");
            CargarClientes();
        }

        private void CargarClientes()
        {
            _listaClientes.Clear();
            if (!File.Exists(_rutaArchivo)) return;

            foreach (var linea in File.ReadAllLines(_rutaArchivo))
            {
                var datos = linea.Split(',');
                if (datos.Length == 3)
                {
                    _listaClientes.Add(new ClienteF
                    {
                        IdCliente = int.Parse(datos[0]),
                        Nombre = datos[1],
                        Correo = datos[2]
                    });
                }
            }

            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = _listaClientes;
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            new RegistrarClienteF(_rolUsuario).Show();
            this.Hide();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem is ClienteF seleccionado)
            {
                if (MessageBox.Show($"¿Deseas borrar a {seleccionado.Nombre}?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _listaClientes.Remove(seleccionado);
                    GuardarClientes();
                    CargarClientes();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente primero.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GuardarClientes()
        {
            var lineas = new List<string>();
            foreach (var c in _listaClientes)
                lineas.Add($"{c.IdCliente},{c.Nombre},{c.Correo}");

            File.WriteAllLines(_rutaArchivo, lineas);
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;
            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            else
                ventanaDestino = new AdminPanel(_rolUsuario);

            ventanaDestino.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
