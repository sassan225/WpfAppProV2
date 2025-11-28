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


namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {
        private readonly string rutaArchivo = "c://LOGINS//productos.txt";

        public RegistrarProducto()
        {
            InitializeComponent();
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.CreateDirectory("c://LOGINS");

                int id = new Random().Next(1000, 9999);

                Producto p = new Producto(
                    id,
                    txtNombre.Text,
                    txtCategoria.Text,
                    Convert.ToDecimal(txtPrecio.Text),
                    Convert.ToInt32(txtStock.Text)
                );

                string linea = $"{p.IdProducto},{p.Nombre},{p.Categoria},{p.Precio},{p.Stock}";
                File.AppendAllText(rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show("Producto guardado correctamente!");
                Limpiar();
            }
            catch
            {
                MessageBox.Show("Error al guardar");
            }
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtCategoria.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.Close();
        private void BtnMinimize_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Welcome welcome = new Welcome();
            welcome.Show();

        }
    }
}

