using GerenciadorDeSenhas.Controller;
using GerenciadorDeSenhas.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeSenhas.Model
{
    public static class Global
    {
        public static ConexaoDB ConexaoBanco { get; set; }
        public static UsuarioController UsuarioController = new UsuarioController();
        public static ListaSenhas ListaSenhas { get; set; }
        
    }
}
