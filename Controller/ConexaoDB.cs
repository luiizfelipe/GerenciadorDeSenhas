using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.IdentityModel.Tokens;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data;

namespace GerenciadorDeSenhas.Controller
{
    public class ConexaoDB
    {
        string usuario = "root";
        string senha = "";
        string database_name = "gerenciadordesenha_gratuito";
        bool dbExiste = false;

        public DataTable ExecutarSQL(string sql, bool useDatabase = true) {
            string connectionString = $"Server=localhost;User ID={usuario};Password={senha};";
            connectionString += useDatabase == true ? $"database={database_name};" : "";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                try
                {
                    conn.Open();


                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable); 
                        return dataTable;
                    }

                }
                catch (Exception ex)
                {
                   MessageBox.Show("Ocorreu um erro inesperado na hora de realizar a execução de um comando SQL.\nMensagem de erro: " + ex.Message);
                   return null;

                }

            }

        }
        public bool ConectarDb()
        {
            if(!ChecarMysql())
                { return false ; }

            string connectionString = $"Server=localhost;User ID={usuario};Password={senha};";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                
                try
                {
                    conn.Open();

                    string query = "SHOW DATABASES;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            if (reader[0].ToString() == database_name) {
                                dbExiste = true;
                                if (ChecarDB())
                                    return true;
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1045) {
                        MessageBox.Show("Senha incorreta do DB, favor recompilar o programa.");
                    }

                }

                if (CriarDataBase())                
                    return true;
                

                return false;
            }
        }

        public bool ChecarMysql() {

            Process[] processes = Process.GetProcesses();
            string serviceName = "MySQL"; 


            ServiceController sc = new ServiceController(serviceName);

            try
            {
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    return true;
                }
            }
            catch (Exception ) {
            }
            


            var mysqlProcesses = processes.Where(p => p.ProcessName.StartsWith("mysql", StringComparison.OrdinalIgnoreCase));
            if (mysqlProcesses.Any())
            {
                foreach (var process in mysqlProcesses)
                {
                    if (process.ProcessName == "mysqld") {
                        return true;
                    }
                }
            }
            MessageBox.Show("O serviço do Mysql não foi encontrado.\nTente estar executando o Mysql ou contatar administrador do sistema.");
            return false;
        }

        private bool ChecarDB()
        {
            //To Do
            return true;
        }
        public bool CriarDataBase()
        {
            if (dbExiste)
            {
                if (ExecutarSQL($"DROP DATABASE {database_name};", useDatabase: false) == null)
                    return false;
            }
            if (ExecutarSQL($"CREATE DATABASE {database_name};",useDatabase: false) == null)               
               return false;
            if (ExecutarSQL($"CREATE TABLE Users(" +
                "ID INT AUTO_INCREMENT PRIMARY KEY," +
                "Username VARCHAR(50) NOT NULL UNIQUE," +
                "Password VARCHAR(255) NOT NULL," +
                "Email VARCHAR(100)," +
                "CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP)") == null)
                return false;
            if (ExecutarSQL($"CREATE TABLE Passwords(" +
                "ID INT AUTO_INCREMENT PRIMARY KEY," +
                "Name VARCHAR(50) NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "Value VARCHAR(100)," +
                "UserID INT NOT NULL)") == null)
                return false;
            return true;
                
           
        }
    }
}
