using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeSenhas.Model
{
    public class Usuario
    {


        public int Id { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public List<Senha> listaSenhas {get;set;}
        public Usuario( string nome, string senha)
        {
            this.nome = nome;
            this.senha = senha;
        }
    }
}
