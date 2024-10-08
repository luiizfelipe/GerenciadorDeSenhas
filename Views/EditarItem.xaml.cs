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
using System.Windows.Shapes;

namespace GerenciadorDeSenhas.Views
{
    /// <summary>
    /// Lógica interna para EditarItem.xaml
    /// </summary>
    public partial class EditarItem : Window
    {
        Senha senha;
        public EditarItem(Senha _senha)
        {
            senha = _senha;
            InitializeComponent();

            NomeTextBox.Text = senha.Name;
            DescriptionBox.Text = senha.Descricao;
            ValueBox.Text = UsuarioController.XorString(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(senha.Valor)),Global.UsuarioController.user.senha);


        }

        private void Salvar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show(
                "Você deseja realmente salvar as alterações deste item?",
                "Confirmação de Edição",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (msg == MessageBoxResult.Yes)
            {
                System.Data.DataTable result = Global.ConexaoBanco.ExecutarSQL($"UPDATE Passwords SET Name = '{NomeTextBox.Text}', Description = '{DescriptionBox.Text}', Value = '{Convert.ToBase64String(Encoding.UTF8.GetBytes(UsuarioController.XorString(ValueBox.Text, Global.UsuarioController.user.senha)))}'  WHERE ID = '{senha.Id}'");
                if (result != null) {
                    Global.ListaSenhas.carregarSenhas();
                    this.Close();
                }
            }

            }
    }
}
