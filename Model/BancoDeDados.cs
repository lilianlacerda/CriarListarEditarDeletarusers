using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Model
{
    class BancoDeDados
    {
        private readonly string _conexao;

        public BancoDeDados()
        {
            // Obtém a string de conexão do App.config
            _conexao = ConfigurationManager.ConnectionStrings["MyMetasDB"].ConnectionString;

            // Verifica se a conexão foi encontrada
            if (string.IsNullOrEmpty(_conexao))
            {
                MessageBox.Show("String de conexão não encontrada no App.config!");
                throw new InvalidOperationException("String de conexão não configurada");
            }
        }

        public DataTable ExecutarConsultas(string sql)
        {
            DataTable tabela = new DataTable();

            try
            {
                using (var minhaConexao = new MySqlConnection(_conexao))
                {
                    minhaConexao.Open();
                    using (var comando = new MySqlCommand(sql, minhaConexao))
                    {
                        var adapter = new MySqlDataAdapter(comando);
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao acessar o banco: " + ex.Message);
            }

            return tabela;
        }

        public int ExecutarComando(string sql, Dictionary<string, object> parametros)
        {
            int linhasAfetadas = 0;

            try
            {
                using (var minhaConexao = new MySqlConnection(_conexao))
                {
                    minhaConexao.Open();

                    using (var comando = new MySqlCommand(sql, minhaConexao))
                    {
                        // Adiciona os parâmetros
                        foreach (var param in parametros)
                        {
                            comando.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        linhasAfetadas = comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao executar comando: " + ex.Message);
            }

            return linhasAfetadas;
        }

    }
}
