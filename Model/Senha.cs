using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeSenhas.Model
{
    public class Senha
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }

        public Senha(int id, string name, string descricao, string valor)
        {
            Id = id;
            Name = name;
            Descricao = descricao;
            Valor = valor;
        }
        public Senha(string name, string descricao, string valor)
        {
            Name = name;
            Descricao = descricao;
            Valor = valor;
        }

    }
}
