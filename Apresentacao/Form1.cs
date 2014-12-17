using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using System.IO;

namespace Apresentacao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // AtualizarInformacoes atualizar = new AtualizarInformacoes();
            AtualizarCustodiantes atualizarCustodiantes = new AtualizarCustodiantes();
            atualizarCustodiantes.AtualizarAgentes();
            
            /*string informacoes = atualizar.getInformacoes();
            TitulosColecao titulosColecao = atualizar.montarColecaoTitulo(informacoes);
            atualizar.salvarBD(titulosColecao);
            atualizarCustodiantes.AtualizarBD();*/


        }


    }
}
