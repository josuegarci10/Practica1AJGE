using System.Windows;

namespace Practica1AJGE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Requisito: Botón de "Cerrar Sesión" que devuelve al formulario de login
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Window1 login = new Window1();
            login.Show();
            this.Close(); // Cierra el portal, dejando solo el login activo
        }

        // Requisito: Botón "Salir" para cerrar toda la aplicación
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}