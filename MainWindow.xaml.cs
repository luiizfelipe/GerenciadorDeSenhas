using GerenciadorDeSenhas.Controller;
using GerenciadorDeSenhas.Model;
using GerenciadorDeSenhas.Views;
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

namespace GerenciadorDeSenhas
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        ConexaoDB _cn;
        public MainWindow()
        {
            _cn = new ConexaoDB();
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_cn.ConectarDb())
            {
                Global.ConexaoBanco = _cn;
                JanelaPrincipal menu = new JanelaPrincipal();
                menu.Show();

                this.Close();
            }

                
        }
    }
}
