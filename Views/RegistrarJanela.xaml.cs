using GerenciadorDeSenhas.Controller;
using GerenciadorDeSenhas.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GerenciadorDeSenhas.Views
{
    /// <summary>
    /// Interação lógica para Registrar.xam
    /// </summary>
    public partial class RegistrarJanela : Page
    {
        Frame _mainFrame;
        public RegistrarJanela(Frame mainFrame )
        {
            _mainFrame = mainFrame;
            InitializeComponent();
        }

        private void RegistrarUsuario(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario(UsernameTextBox.Text, PasswordBox.Password);
            if (Global.UsuarioController.CriarUsuario(usuario)) {
                MessageBox.Show("Usuário criado!");
                _mainFrame.GoBack();

            }
            else
            {
                MessageBox.Show("Já existe usuário com esse nome");
            }
            
        }
    }
}
