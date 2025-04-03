using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;

namespace WindowsFormsApp1
{
    public partial class Inicio: Form
    {
        private readonly UserController _userController = new UserController();
        public Inicio()
        {
            InitializeComponent();
            CarregarUsuarios();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            dgvUsuarios.AutoGenerateColumns = false;
        }

        private void CarregarUsuarios()
        {
            List<Model.UserModel> usuarios = _userController.ListarUsuarios();

            dgvUsuarios.DataSource = usuarios;

            dgvUsuarios.AutoResizeColumns();
        }
    }
}
