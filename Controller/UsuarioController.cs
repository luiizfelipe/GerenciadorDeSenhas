using GerenciadorDeSenhas.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GerenciadorDeSenhas.Controller
{
    public class UsuarioController
    {
        public Usuario user = new Usuario(null, null);

        

        public bool CriarUsuario(Usuario usuario) {

            if(checarExistenciaUsuario(usuario.nome))
                return false;
            if (Global.ConexaoBanco.ExecutarSQL($"INSERT INTO Users (Username, Password) VALUES ('{usuario.nome}', '{CalcularHash(usuario.senha)}')") == null)               
                return false;
            return true;
        }

        public bool checarExistenciaUsuario(String username) {
            DataTable result = Global.ConexaoBanco.ExecutarSQL($"SELECT * FROM Users WHERE Username = '{username}'");
            if (result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool validarUsuario()
        {
            DataTable result = Global.ConexaoBanco.ExecutarSQL($"SELECT * FROM Users WHERE Username = '{user.nome}' AND Password ='{CalcularHash(user.senha)}'");
            if (result == null || result.Rows.Count != 1 )
                return false;
            return true;
        }
        public bool adicionarSenha(Senha senha)
        {
            if (Global.ConexaoBanco.ExecutarSQL($"INSERT INTO Passwords (Name, Description,Value,UserID) VALUES ('{senha.Name}', '{senha.Descricao}','{Convert.ToBase64String(Encoding.UTF8.GetBytes(XorString(senha.Valor, user.senha)))}',{user.Id})") == null)
                return false;
            return true;
        }


        static string CalcularHash(string rawData)
        {
            // Cria uma instância de SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Converte a string de entrada em um array de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Converte o array de bytes em uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // "x2" para hexadecimal
                }

                return builder.ToString();
            }
        }

        public static string XorString(string text, string key)
        {
            return new string(text.Select((c, i) => (char)(c ^ key[i % key.Length])).ToArray());
        }
    }
}
