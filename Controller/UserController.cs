using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model.Repository;

namespace WindowsFormsApp1.Controller
{
    class UserController
    {
        private readonly Model.Repository.UserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public List<Model.UserModel> ListarUsuarios()
        {
            return _userRepository.ListarTodos();
        }

        public (bool sucesso, string mensagem) EditarUsuario(int id, string novoNome, string novoEmail)
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(novoNome))
                return (false, "Nome não pode ser vazio");

            if (string.IsNullOrWhiteSpace(novoEmail))
                return (false, "E-mail não pode ser vazio");

            try
            {
                bool resultado = _userRepository.EditarUsuario(id, novoNome, novoEmail);
                return (resultado, resultado ? "Usuário atualizado!" : "Usuário não encontrado");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao editar: {ex.Message}");
            }
        }

        public (bool sucesso, string mensagem) DeletarUsuario(int id)
        {
            try
            {
                bool resultado = _userRepository.DelettarUsuario(id);
                return (resultado, resultado ? "Usuário deletado!" : "Usuário não encontrado");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao deletar: {ex.Message}");
            }
        }
    }
}
