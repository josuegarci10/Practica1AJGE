using System.Windows;

namespace Practica1AJGE
{
    public partial class Window1 : Window
    {
        // Requisito: Simulación de lista de usuarios en memoria (string[,])
        private string[,] usuarios = new string[,]
        {
            { "admin", "1234" },
            { "jugador1", "1111" },
            { "jugador2", "2222" }
        };

        private int intentosFallidos = 0;

        public Window1()
        {
            InitializeComponent();
        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            // TC 05 y TC 07: Control de bloqueo tras 3 intentos
            if (intentosFallidos >= 3)
            {
                MessageBox.Show("Usuario bloqueado temporalmente por superar los intentos permitidos.", "Bloqueado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Password;

            // TC 04: Campos obligatorios vacíos
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Introduzca datos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            bool usuarioEncontrado = false;
            bool credencialesCorrectas = false;

            // Validación contra el string[,]
            for (int i = 0; i < usuarios.GetLength(0); i++)
            {
                if (usuarios[i, 0] == user)
                {
                    usuarioEncontrado = true;
                    if (usuarios[i, 1] == pass)
                    {
                        credencialesCorrectas = true;
                    }
                    break;
                }
            }

            // TC 03: Usuario inexistente
            if (!usuarioEncontrado)
            {
                MessageBox.Show("El usuario introducido no existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassword.Clear();
                txtUsuario.Focus();
                return;
            }

            // TC 02: Contraseña incorrecta
            if (!credencialesCorrectas)
            {
                intentosFallidos++;
                if (intentosFallidos >= 3)
                {
                    MessageBox.Show("Se bloquea temporalmente el usuario", "Bloqueo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("La contraseña introducida no es correcta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                return;
            }

            // TC 01: Éxito -> Abre el portal y cierra esta ventana
            MessageBox.Show("¡Inicio de sesión exitoso!", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);

            // Reiniciamos intentos por si vuelve a iniciar sesión después
            intentosFallidos = 0;

            MainWindow portal = new MainWindow();
            portal.Show();
            this.Close();
        }

        // TC 06: Salir de la aplicación
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}