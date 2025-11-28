
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para VerProveedores.xaml
    /// </summary>
    public partial class VerProveedores : Window
    {
        private string carpetaBase = @"C:\cosmetiqueSoftware";

        private string rutaArchivo;

        public VerProveedores()
        {
            InitializeComponent();

            if (!Directory.Exists(carpetaBase))
                Directory.CreateDirectory(carpetaBase);

            rutaArchivo = Path.Combine(carpetaBase, "proveedores.txt");

            CargarProveedores();
        }

        private void CargarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length == 4) 
                    {
                        lista.Add(new Proveedor
                        {
                       
                            IdProveedor = int.Parse(datos[0]),
                            NombreProveedor = datos[1],
                            ContactoProveedor = datos[2],
                            Ciudad = datos[3]
                        });
                    }
                }
            }

            dgProveedores.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Welcome w = new Welcome();
            w.Show();
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
