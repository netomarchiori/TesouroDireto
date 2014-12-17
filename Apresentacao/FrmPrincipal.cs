using AcessoBD;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Apresentacao
{
    public partial class FrmPrincipal : Form
    {
        Usuario usuarioLogado = new Usuario();
        Usuario usuarioNovo = new Usuario();
        private DataTable dataTableTitulos,dataTableAgentes;

        Double valorInvestido = 0;
        Double taxaCompra = 0,taxaExtra = 0,taxaAdmin = 0;
        DateTime dataVencimento,dataCompra;
        Boolean valido = true;


        public FrmPrincipal(Usuario usuario)
        {
            usuarioLogado = usuario;
            InitializeComponent();
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            lbUsuarioNome.Text = usuarioLogado.Nome.Trim();
           CarregarDadosPessoas();
            PrencherGridTitulos();
            PrencherGridAgentes();
            CarregarComboboxAgentes();
        }

        private void pbInicio_Click(object sender, EventArgs e)
        {
            panelTitulos.Visible = false;
            panelCalculadora.Visible = false;
            panelNoticias.Visible = false;
         //   panelAjuste.Visible = false;
            panelPessoal.Visible = false;


         }

        private void pbTitulos_Click(object sender, EventArgs e)
        {
            panelTitulos.Visible = true;
            panelNoticias.Visible = false;
            panelCalculadora.Visible = false;
           // panelAjuste.Visible = false;
            panelPessoal.Visible = false;

        }

        private void pbCalculadora_Click(object sender, EventArgs e)
        {
            limparCampos();
            limparMarcacao();
            panelCalculadora.Visible = true;
            panelTitulos.Visible = false;
            panelNoticias.Visible = false;
           // panelAjuste.Visible = false;
            panelPessoal.Visible = false;

        }

        private void pbNoticias_Click(object sender, EventArgs e)
        {
            panelNoticias.Visible = true;
          //  panelAjuste.Visible = false;
            panelPessoal.Visible = false;
            panelTitulos.Visible = false;
            panelCalculadora.Visible = false;
        }

   
        private void pbPessoal_Click(object sender, EventArgs e)
        {
            panelPessoal.Visible = true;
           // panelAjuste.Visible = false;
            panelTitulos.Visible = false;
            panelNoticias.Visible = false;
            panelCalculadora.Visible = false;
        }
                 

        private void cbTitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTitulo.SelectedIndex)
            {

                case 0:
                    lbExtra.Text = "";
                    txtExtra.Visible = false;
                    break;
                case 1:
                    lbExtra.Text = "Taxa Selic para o Período (%a.a.):";
                    txtExtra.Visible = true;
                    break;
                case 2:
                    lbExtra.Text = "Taxa de Inflação (IGP-M) para o Período (%a.a.):";
                    txtExtra.Visible = true;
                    break;
                case 3:
                    lbExtra.Text = "Taxa de Inflação (IPCA) para o Período (%a.a.):";
                    txtExtra.Visible = true;
                    break;
                case 4:
                    lbExtra.Text = "";
                    txtExtra.Visible = false;
                    break;
                case 5:
                    lbExtra.Text = "Taxa de Inflação (IPCA) para o Período (%a.a.):";
                    txtExtra.Visible = true;
                    break;
            }
        }
             
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            valido = true;
            limparMarcacao();
            lbValidar.Text = validarCampos();
            if (valido)
            {
                DadosCalculos dadosCalculos = new DadosCalculos();
                dadosCalculos.dataCompra = dataCompra;
                dadosCalculos.dataVencimento =dataVencimento;
                dadosCalculos.valorInvestido = valorInvestido;
                dadosCalculos.taxaAdmin = taxaAdmin;
                dadosCalculos.taxaExtra= taxaExtra;
                dadosCalculos.taxaCompra= taxaCompra;
                dadosCalculos.tipoTitulo = cbTitulo.SelectedIndex;
                txtMensCalc.Visible = true;
                Simulacao simulacao = new Simulacao();
                DataTable dataTable = simulacao.RealizarSimulacao(dadosCalculos);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView1.Visible = true;            
            }


        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            limparMarcacao();
        }
        private void txtDataCompra_KeyPress(object sender, KeyPressEventArgs e)
        {

            ApenasNumeros(e);
            if (!e.KeyChar.Equals('\b'))
            {
                if (txtDataCompra.Text.Length == 2) { txtDataCompra.Text += "/"; txtDataCompra.Select(txtDataCompra.Text.Length, 0); }
                if (txtDataCompra.Text.Length == 5) { txtDataCompra.Text += "/"; txtDataCompra.Select(txtDataCompra.Text.Length, 0); }
                if (txtDataCompra.Text.Length == 10) { e.Handled = true; }
            }

        }

        private void txtDataVencimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ApenasNumeros(e);
            if (!e.KeyChar.Equals('\b'))
            {
                if (txtDataVencimento.Text.Length == 2) { txtDataVencimento.Text += "/"; txtDataVencimento.Select(txtDataVencimento.Text.Length, 0); }
                if (txtDataVencimento.Text.Length == 5) { txtDataVencimento.Text += "/"; txtDataVencimento.Select(txtDataVencimento.Text.Length, 0); }
                if (txtDataVencimento.Text.Length == 10) { e.Handled = true; }
            }

        }

        private void txtValorInvestido_KeyPress(object sender, KeyPressEventArgs e)
        {
            ApenasNumeros(e);

        }

        private void btnAtualizarTitulos_Click(object sender, EventArgs e)
        {
            btnAtualizarTitulos.Text = "Atualizando....";
            btnAtualizarTitulos.ForeColor = Color.Green;
            btnAtualizarTitulos.Enabled = false;

            atualizarTitulos();

            btnAtualizarTitulos.Text = "Atualizar titulos agora";
            btnAtualizarTitulos.ForeColor = Color.Black;
            btnAtualizarTitulos.Enabled = true;

        }

        private void btnLimparCampos_Click(object sender, EventArgs e)
        {
            limparUsuario();
           
        }

        private void pbUsuario_Click(object sender, EventArgs e)
        {
            CarregarDadosPessoas();
            pbPessoal_Click(null,null);
        }


        //METODOS PROPRIOS
        private string validarCampos()
        {

            string validar = "";
            if(cbTitulo.SelectedIndex==-1)
            {
                cbTitulo.BackColor = Color.AntiqueWhite;
                validar += "\nSelecione um Titulo!";
                valido = false;

            }

            try { dataCompra = Convert.ToDateTime(txtDataCompra.Text); }
            catch (Exception)
            {
                txtDataCompra.BackColor = Color.AntiqueWhite;
                validar += "\nData de compra no formato invalido! dd/mm/aaaa ou dd/mm/aa!";
                valido = false;
            }
            try { dataVencimento = Convert.ToDateTime(txtDataVencimento.Text); }
            catch (Exception)
            {
                txtDataVencimento.BackColor = Color.AntiqueWhite;
                validar += "\nData de vencimento no formato invalido! dd/mm/yyyy ou dd/mm/aa!";
                valido = false;
            }
            if (dataVencimento < dataCompra)
            {
                txtDataVencimento.BackColor = Color.AntiqueWhite;
                txtDataCompra.BackColor = Color.AntiqueWhite;
                validar += "\nData de compra maior que data vencimento!"; valido = false;

            }
            try { valorInvestido = Convert.ToDouble(txtValorInvestido.Text); }
            catch (Exception)
            {
                txtValorInvestido.BackColor = Color.AntiqueWhite;
                validar += "\nValor investido invalido! Apenas numero!"; valido = false;
            }
            try { taxaCompra = Convert.ToDouble(txtTaxaCompra.Text); }
            catch (Exception)
            {
                txtTaxaAdmin.BackColor = Color.AntiqueWhite;
                validar += "\nTaxa de administração invalido! Apenas numero!"; valido = false;
            }
            try { taxaAdmin = Convert.ToDouble(txtTaxaAdmin.Text); }
            catch (Exception)
            {
                txtTaxaCompra.BackColor = Color.AntiqueWhite;
                validar += "\nTaxa de compra invalido! Apenas numero!"; valido = false;
            }
            if (txtExtra.Visible)
            {
                try { taxaExtra = Convert.ToDouble(txtExtra.Text); }
                catch (Exception)
                {
                    txtExtra.BackColor = Color.AntiqueWhite;
                    validar += "\nTaxa invalido! Apenas numero!"; valido = false;
                }
            }



            return validar;
        }
        
        private void PrencherGridTitulos()
        {
            //atualizarTitulos();
            SqlServer sqlServer = new SqlServer();
            try
            {
                dataTableTitulos = sqlServer.execultarConsulta(CommandType.StoredProcedure, "uspTitulosConsultar");
                dgvTitulos.DataSource = dataTableTitulos;
                lbAtualizadoEm.Text = dataTableTitulos.Rows[0][7].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        private void PrencherGridAgentes()
        {
            atualizarAgentes();
            SqlServer sqlServer = new SqlServer();
            try
            {
                dataTableAgentes = sqlServer.execultarConsulta(CommandType.StoredProcedure, "uspAgenteConsultar");
                dgvAgente.DataSource = dataTableAgentes;
                lbAgenteAtualizadoEm.Text = dataTableAgentes.Rows[0][6].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        private void limparMarcacao()
         {
             lbValidar.Text = "";
             cbTitulo.BackColor = Color.White;
             txtDataCompra.BackColor = Color.White;
             txtDataVencimento.BackColor = Color.White;
             txtTaxaCompra.BackColor = Color.White;
             txtTaxaAdmin.BackColor = Color.White;
             txtValorInvestido.BackColor = Color.White;
             txtExtra.BackColor = Color.White;

         }

        private void limparCampos()
        {
            lbValidar.Text = "";
            cbTitulo.Refresh();
            txtDataCompra.Clear();
            txtDataVencimento.Clear();
            txtTaxaCompra.Clear();
            txtTaxaAdmin.Clear();
            txtValorInvestido.Clear();
            txtExtra.Clear();

        }
              
        public void ApenasNumeros(KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || e.KeyChar.Equals('\b') || e.KeyChar.Equals(',')))
            {
                e.Handled = true;
            }
           
        }
           
        public void atualizarTitulos()
        {
            AtualizarInformacoes atualizar = new AtualizarInformacoes();
            string informacoes = atualizar.getInformacoes();
            if (!informacoes.Equals("suspenso"))
            {
                TitulosColecao titulosColecao = atualizar.montarColecaoTitulo(informacoes);
                atualizar.salvarBD(titulosColecao);
                PrencherGridTitulos();
            }
            else
            {
                MessageBox.Show("Mercado temporariamente suspenso, aguarde a abertura para poder atualizar os titulos", "Mercado fechado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void atualizarAgentes()
        {
            AtualizarCustodiantes atualizarCustodiantes = new AtualizarCustodiantes();
            atualizarCustodiantes.AtualizarAgentes();

        }

        public void CarregarDadosPessoas()
        {
            txtUsuarioNome.Text= usuarioLogado.Nome;
            txtUsuarioEmail.Text= usuarioLogado.Email;
            txtUsuarioUsuario.Text= usuarioLogado.UsuarioNome;
            txtUsuarioNascimento.Text = usuarioLogado.Nascimento.ToShortDateString();
                    
        }

        public void CarregarComboboxAgentes()
        {
            cbAgentes.Items.Add("SEM INSTITUIÇÃO NO MOMENTO");
            foreach (DataRow linha in dataTableAgentes.Rows)
            {
                cbAgentes.Items.Add(linha[0]+"- Taxa: "+linha[3]);

            }
        }

        


        public void limparUsuario() 
        {
            txtUsuarioEmail.Clear();
            txtUsuarioNascimento.Clear();
            txtUsuarioNome.Clear();
            txtUsuarioUsuario.Clear();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            DesmarcarErroUsuario();
            string valido = validarUsuario();
            if (valido.Equals(""))
            {
                UsuarioAtualizarInserir();
            }
            else
            { lbErros.Text = valido; }
        }

        public string validarUsuario()
        {
            string validar = "";
            if (cbAgentes.SelectedIndex == -1)
            {
                cbAgentes.BackColor = Color.AntiqueWhite;
                usuarioNovo.idAgenteCustodia = 1;//cbAgentes.Items[cbAgentes.SelectedIndex].ToString();
                validar += "\nSelecione um Agente Financeiro ou marque 'Sem agente no momento'!"; lbErros.Visible = true;
            }

            if (txtUsuarioNome.Text.Equals(""))
            {
                txtUsuarioNome.BackColor = Color.AntiqueWhite;
                validar += "\nNome invalido ou em branco!"; lbErros.Visible = true;

            }
            else { usuarioNovo.Nome = txtUsuarioNome.Text; }

            try { usuarioNovo.Nascimento = Convert.ToDateTime(txtUsuarioNascimento.Text); }
            catch (Exception)
            {
                txtUsuarioNascimento.BackColor = Color.AntiqueWhite;
                validar += "\nData de nascimento no formato invalido! dd/mm/aaaa ou dd/mm/aa!";
                lbErros.Visible = true;
            }
            Match matchEmail = Regex.Match(txtUsuarioEmail.Text, Padroes.EMAIL);
            if (!matchEmail.Success)
            {
                txtUsuarioEmail.BackColor = Color.AntiqueWhite;
                validar += "\nEmail invalido!"; lbErros.Visible = true;
            }
            else { usuarioNovo.Email = txtUsuarioEmail.Text; }


            if (txtUsuarioUsuario.Text.Equals(""))
            {
                txtUsuarioUsuario.BackColor = Color.AntiqueWhite;
                validar += "\nNome de usuario invalido ou em branco!"; lbErros.Visible = true;

            }
            else { usuarioNovo.UsuarioNome = txtUsuarioUsuario.Text; }

            if (txtSenhaAtual.Text.Equals(usuarioLogado.Senha)) { usuarioNovo.Senha = txtUsuarioSenha.Text; }
            else
            {
                txtSenhaAtual.BackColor = Color.AntiqueWhite;
                validar += "\nSenha atual incorreto!"; lbErros.Visible = true;
            }

            if (checkMudarSenha.Checked == true)
            {
                //string senhaConfirma = txtUsuarioConfirmaSenha.Text;
                if (!(txtUsuarioSenha.Text.Equals("")) && txtUsuarioSenha.Text.Equals(txtUsuarioConfirmaSenha.Text))
                {
                    usuarioNovo.Senha = txtUsuarioSenha.Text; 
                }
                else
                {
                    txtUsuarioSenha.BackColor = Color.AntiqueWhite;
                    txtUsuarioConfirmaSenha.BackColor = Color.AntiqueWhite;
                    validar += "\nSenhas não confere!"; lbErros.Visible = true; 
                }
            }
            else { usuarioNovo.Senha = usuarioLogado.Senha; }
           




            return validar;

        }

        private void checkMudarSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMudarSenha.Checked)
            {
                txtUsuarioConfirmaSenha.Visible = true;
                txtUsuarioSenha.Visible = true;
                label25.Visible = true;
                label28.Visible = true;
            }
            else
            {
                txtUsuarioConfirmaSenha.Visible = false;
                txtUsuarioSenha.Visible = false;
                label25.Visible = false;
                label28.Visible = false;
            }
        }

        public void UsuarioAtualizarInserir()
        {
            try
            {
                SqlServer sqlServer = new SqlServer();
                sqlServer.limparSqlParameterCollection();
                sqlServer.addSqlParameterCollection("@Nome", usuarioNovo.Nome);
                sqlServer.addSqlParameterCollection("@Email", usuarioNovo.Email);
                sqlServer.addSqlParameterCollection("@Usuario", usuarioNovo.UsuarioNome);
                sqlServer.addSqlParameterCollection("@Senha", usuarioNovo.Senha);
                sqlServer.addSqlParameterCollection("@Nascimento", usuarioNovo.Nascimento);
                sqlServer.addSqlParameterCollection("@idAgenteCustodia", 1);
                object a = sqlServer.excultarAcao(CommandType.StoredProcedure, "uspUsuarioInserirAtualizar");
            }
            catch (Exception ex)
            { MessageBox.Show("Erro: "+ ex.Message); }
            MessageBox.Show("Dados pessoais atualizado com sucesso!","Atualização",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        public void DesmarcarErroUsuario()
        {
            lbErros.Visible = false;
            txtUsuarioConfirmaSenha.BackColor = Color.White;
            txtUsuarioSenha.BackColor = Color.White;
            txtUsuarioUsuario.BackColor = Color.White;
            txtUsuarioEmail.BackColor = Color.White;
            txtUsuarioNascimento.BackColor = Color.White;
            txtUsuarioNome.BackColor = Color.White;
            txtSenhaAtual.BackColor = Color.White;
            cbAgentes.BackColor = Color.White;

        }

     
    
    
    
    
    }
}
