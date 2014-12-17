using AcessoBD;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlServer sqlServer = new SqlServer();
                sqlServer.limparSqlParameterCollection();
                sqlServer.addSqlParameterCollection("@Usuario", txtUsuario.Text);
                sqlServer.addSqlParameterCollection("@Senha", txtSenha.Text);
                DataTable dataTable = sqlServer.execultarConsulta(CommandType.StoredProcedure, "uspUsuarioConsultarSenha");
                int cont = dataTable.Rows.Count;
              //  MessageBox.Show(dataTable.Rows[0][0].ToString());
                if (cont > 0)
                {
                    Usuario usuario = new Usuario();
                    usuario.idUsuario = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                    usuario.Nome = dataTable.Rows[0][1].ToString();
                    usuario.Email = dataTable.Rows[0][2].ToString();
                    usuario.UsuarioNome = dataTable.Rows[0][3].ToString();
                    usuario.Senha = dataTable.Rows[0][4].ToString();
                    usuario.Nascimento = Convert.ToDateTime(dataTable.Rows[0][5].ToString());
                    usuario.idAgenteCustodia = Convert.ToInt32(dataTable.Rows[0][6].ToString());
                    
                    FrmPrincipal frmPrincipal = new FrmPrincipal(usuario);
                    frmPrincipal.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Usuario não cadastrado ou usuario/senha invalido","Identificação",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            { MessageBox.Show("OPS:-) Ocorreu um erro ao realizar identificação\n"+ex.Message); }

        }
                                                 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
