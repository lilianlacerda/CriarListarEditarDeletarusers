using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model.Repository
{
    class UserRepository
    {
        private readonly BancoDeDados _bancoDeDados;

        public UserRepository()
        {
            _bancoDeDados = new BancoDeDados();
        }

        public List<UserModel> ListarTodos()
        {
            var usuarios = new List<UserModel>();
            string sql = ("SELECT name, email, createdAt FROM users ORDER BY createdAt DESC");

            DataTable resultado = _bancoDeDados.ExecutarConsultas(sql);

            foreach (DataRow row in resultado.Rows)
            {
                usuarios.Add(new UserModel
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["name"].ToString(),
                    Email = row["email"].ToString(),
                    DataCriacao = Convert.ToDateTime(row["createdAt"])
                });
            }

            return usuarios;
        }

        public bool EditarUsuario(int id, string novoNome, string novoEmail)
        {
            string sql = "UPDATE users SET name = @novoNome, email = @novoEmail WHERE id = @id";

            var parametros = new Dictionary<string, object>
            {
                { "@novoNome", novoNome },
                { "@novoEmail", novoEmail },
                { "@id", id }
            };

            int linhasAfetadas = _bancoDeDados.ExecutarComando(sql, parametros);
            return linhasAfetadas > 0;
        }

        public bool DelettarUsuario(int id)
        {
            string sql = "DELETE FROM users WHERE ID = @id";

            var parametros = new Dictionary<string, object>
            {
                { "@id", id }
            };

            int linhasAfetadas = _bancoDeDados.ExecutarComando(sql, parametros);
            return linhasAfetadas > 0;
        }
    }
}
