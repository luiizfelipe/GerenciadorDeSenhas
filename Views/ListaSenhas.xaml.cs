using GerenciadorDeSenhas.Model;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace GerenciadorDeSenhas.Views
{
    /// <summary>
    /// Interação lógica para ListaSenhas.xam
    /// </summary>
    public partial class ListaSenhas : Page
    {
        Usuario _user;
        public ListaSenhas(Usuario user)
        {
            _user = user;
            InitializeComponent();
            Global.ListaSenhas = this;
            carregarSenhas();
        }


        public Grid ItemSenha(Senha senha)
        {
            Grid itemGrid = new Grid();
            itemGrid.Name = $"ID_{senha.Id}";
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            TextBlock itemText = new TextBlock
            {
                Text = senha.Name,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 32
            };
            Grid.SetColumn(itemText, 0); 


            StackPanel imageStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            Grid.SetColumn(imageStackPanel, 1);

            Image editImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Imagens/edit.png")),
                Width = 50,
                Height = 50
            };

            Image trashImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Imagens/trash.png")),
                Width = 50,
                Height = 50
            };
            
            trashImage.Name = $"ID_{senha.Id}"; 
            trashImage.MouseLeftButtonDown += deleteItem;

            editImage.Name = $"ID_{senha.Id}";
            editImage.MouseLeftButtonDown += editarPass;

            imageStackPanel.Children.Add(editImage);
            imageStackPanel.Children.Add(trashImage);

            // Adicionando o TextBlock e o StackPanel ao Grid
            itemGrid.Children.Add(itemText);
            itemGrid.Children.Add(imageStackPanel);

            return itemGrid;

        }

        private void deleteItem(object sender , MouseButtonEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show(
                "Você deseja realmente excluir este item?", 
                "Confirmação de Exclusão",                  
                MessageBoxButton.YesNo,                     
                MessageBoxImage.Question                   
            );
            if (msg == MessageBoxResult.Yes)
            {
                Image clickedImage = sender as Image;

                string[] idSenha = clickedImage.Name.Split(new[] { "ID_" }, StringSplitOptions.None) ;
                DataTable result = Global.ConexaoBanco.ExecutarSQL($"DELETE FROM Passwords WHERE userID = '{Global.UsuarioController.user.Id}' AND ID = '{idSenha[1]}'");
                if (result != null)
                    MessageBox.Show("Item excluído com sucesso!", "Operação Concluída", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            carregarSenhas();

        }
        

        private void editarPass(object sender, MouseButtonEventArgs e)
        {
            Senha senha = null;
            Image clickedImage = sender as Image;
            string[] idSenha = clickedImage.Name.Split(new[] { "ID_" }, StringSplitOptions.None);
            DataTable result = Global.ConexaoBanco.ExecutarSQL($"SELECT * FROM Passwords WHERE userID = '{Global.UsuarioController.user.Id}' AND ID = '{idSenha[1]}'");
            foreach (DataRow row in result.Rows)
            {
                int id = int.Parse(idSenha[1]);
                if ((int)row[0] == id) {
                    senha = new Senha(
                        id: (int)row[0], name: (string)row[1], descricao: (string)row[2], valor: (string)row[3]
                    );
                }     
                        
            }
            if (senha !=null)
                new EditarItem(senha).Show();
        }
        public void carregarSenhas()
        {
            painelSenhas.Children.Clear();
            DataTable result = Global.ConexaoBanco.ExecutarSQL($"SELECT * FROM Passwords WHERE userID = {Global.UsuarioController.user.Id}");
            foreach (DataRow row in result.Rows)
            {
                painelSenhas.Children.Add(
                    ItemSenha(
                        new Senha(
                            id: (int)row[0], name: (string)row[1],descricao:(string)row[2], valor: (string)row[3]
                        )
                        )
                    );
            }
        }

        private void addPass(object sender, RoutedEventArgs e)
        {
            Global.UsuarioController.adicionarSenha(new Senha(name:"Insira um titulo/nome para essa senha", descricao: "Insira uma descrição", valor:"Insira a senha que deseja salva"));
            carregarSenhas();
        }
    }
}
