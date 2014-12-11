using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjetoTransferencia;
using AcessoBD;
using System.Text.RegularExpressions;
using Negocios;

namespace Apresentacao
{
    public partial class FrmPrincipal : Form
    {
        Usuario usuarioLogado = new Usuario();
        private DataTable dataTable;

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
            PrencherGridTitulos();
        }

        private void pbInicio_Click(object sender, EventArgs e)
        {
            panelTitulos.Visible = false;
            panelCalculadora.Visible = false;
            panelNoticias.Visible = false;
            panelAjuste.Visible = false;
            panelSobre.Visible = false;


         }

        private void pbTitulos_Click(object sender, EventArgs e)
        {
            panelTitulos.Visible = true;
            panelNoticias.Visible = false;
            panelCalculadora.Visible = false;
            panelAjuste.Visible = false;
            panelSobre.Visible = false;

        }

        private void pbCalculadora_Click(object sender, EventArgs e)
        {
            limparCampos();
            panelCalculadora.Visible = true;
            panelTitulos.Visible = false;
            panelNoticias.Visible = false;
            panelAjuste.Visible = false;
            panelSobre.Visible = false;

        }

        private void pbNoticias_Click(object sender, EventArgs e)
        {
            panelNoticias.Visible = true;
            panelAjuste.Visible = false;
            panelSobre.Visible = false;
            panelTitulos.Visible = false;
            panelCalculadora.Visible = false;
        }

        private void pbAjuste_Click(object sender, EventArgs e)
        {
            panelAjuste.Visible = true;
            panelSobre.Visible = false;
            panelTitulos.Visible = false;
            panelNoticias.Visible = false;
            panelCalculadora.Visible = false;


        }

        private void pbSobre_Click(object sender, EventArgs e)
        {
            panelSobre.Visible = true;
            panelAjuste.Visible = false;
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
            DateTime x = Convert.ToDateTime("01/03/2010");            
            DateTime y = Convert.ToDateTime("01/01/2025");
            
            //MessageBox.Show(((y-x).Days/365).ToString());
            limparCampos();
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
                Simulacao simulacao = new Simulacao();
                DataTable dataTable = simulacao.RealizarSimulacao(dadosCalculos);
                dgvReSimulacao.DataSource = dataTable;
            }


        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
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
            catch (Exception ex)
            {
                txtDataCompra.BackColor = Color.AntiqueWhite;
                validar += "\nData de compra no formato invalido! dd/mm/yyyy!";
                valido = false;
            }
            try { dataVencimento = Convert.ToDateTime(txtDataVencimento.Text); }
            catch (Exception ex)
            {
                txtDataVencimento.BackColor = Color.AntiqueWhite;
                validar += "\nData de vencimento no formato invalido! dd/mm/yyyy!";
                valido = false;
            }
            if (dataVencimento < dataCompra)
            {
                txtDataVencimento.BackColor = Color.AntiqueWhite;
                txtDataCompra.BackColor = Color.AntiqueWhite;
                validar += "\nData de compra maior que data vencimento!"; valido = false;

            }
            try { valorInvestido = Convert.ToDouble(txtValorInvestido.Text); }
            catch (Exception ex)
            {
                txtValorInvestido.BackColor = Color.AntiqueWhite;
                validar += "\nValor investido invalido! Apenas numero!"; valido = false;
            }
            try { taxaCompra = Convert.ToDouble(txtTaxaCompra.Text); }
            catch (Exception ex)
            {
                txtTaxaAdmin.BackColor = Color.AntiqueWhite;
                validar += "\nTaxa de administração invalido! Apenas numero!"; valido = false;
            }
            try { taxaAdmin = Convert.ToDouble(txtTaxaAdmin.Text); }
            catch (Exception ex)
            {
                txtTaxaCompra.BackColor = Color.AntiqueWhite;
                validar += "\nTaxa de compra invalido! Apenas numero!"; valido = false;
            }
            if (txtExtra.Visible)
            {
                try { taxaExtra = Convert.ToDouble(txtExtra.Text); }
                catch (Exception ex)
                {
                    txtExtra.BackColor = Color.AntiqueWhite;
                    validar += "\nTaxa invalido! Apenas numero!"; valido = false;
                }
            }



            return validar;
        }


        private void PrencherGridTitulos()
        {
            SqlServer sqlServer = new SqlServer();
            try
            {
                dataTable = sqlServer.execultarConsulta(CommandType.StoredProcedure, "uspTitulosConsultar");
                dgvTitulos.DataSource = dataTable;
                lbAtualizadoEm.Text = dataTable.Rows[0][7].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void limparCampos()
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

       
    }
}
