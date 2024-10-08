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
    /// Interação lógica para Login.xam
    /// </summary>
    public partial class LoginJanela : Page
    {
        Frame _mainFrame;
        public LoginJanela(Frame mainFrame)
        {
            _mainFrame = mainFrame;
            InitializeComponent();
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {
            Global.UsuarioController.user.nome = UsernameTextBox.Text;
            Global.UsuarioController.user.senha = PasswordBox.Password;


            if (Global.UsuarioController.user.nome == null || Global.UsuarioController.user.senha == null)
                return;

            if(Global.UsuarioController.validarUsuario())
                _mainFrame.Content = new ListaSenhas(Global.UsuarioController.user);
            
        }

        private void lnkRegistrar(object sender, MouseButtonEventArgs args)
        {
            _mainFrame.Content = new RegistrarJanela(_mainFrame);
        }
    }
}
