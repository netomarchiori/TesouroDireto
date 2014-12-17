using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
VALORES DO IMPOSTO DE RENDA
i) 22,5% para aplicações com prazo de até 180 dias;             = taxaIR[0]
ii) 20% para aplicações com prazo de 181 dias até 360 dias;     = taxaIR[1]
iii) 17,5% para aplicações com prazo de 361 dias até 720 dias;  = taxaIR[2]
iv) 15% para aplicações com prazo acima de 720 dias.            = taxaIR[3]

 */

namespace Negocios
{
    public class Simulacao
    {
        private const Double taxaCustodia = 0.30;     //Taxa de custódia de 0,30% a.a. (ao dia (0,003/365)100 = 0,000008) sobre o valor dos títulos.
        private Int32 diasUteis,diasInvestidos;
        private Double valorInvestido=0,valorAdmin=0,valorAdminRet=0,valorCustodia=0;
    
        public Double JurosComposto(Double capital,Double taxa,Double tempo)
        {
            Double M = 0, i=0;
            i = (taxa / 365) / 100;
            M = Math.Pow((1+i),tempo);
            M = capital * M;
            return M;
        }


        public Double ValorAdmin(Double capital, Double taxaAdmin)
        {
            Double M = 0;
            M =capital * (taxaAdmin/100);
            return M;
        }


        
        public DataTable RealizarSimulacao(DadosCalculos dadosCalculos)
        {
            DataTable dataTable = new DataTable();

            Double [] result = new Double[11];
            result[0] = diasInvestidos = (dadosCalculos.dataVencimento - dadosCalculos.dataCompra).Days;
            result[1] = dadosCalculos.valorInvestido;
            //taxa de compra
            result[2] = dadosCalculos.taxaCompra;
            //taxa de admin
            result[3] = ValorAdmin(dadosCalculos.valorInvestido, dadosCalculos.taxaAdmin);
            //Montante
            result[4] = JurosComposto(dadosCalculos.valorInvestido-result[3], dadosCalculos.taxaCompra, diasInvestidos);
            //Valor custodia
            result[5] = JurosComposto(result[4],taxaCustodia,diasInvestidos)-result[4];
            //Valor admin na retirada
            result[6] = JurosComposto(result[4],dadosCalculos.taxaAdmin,diasInvestidos)-result[4];
            //taxa de IR
            if (result[0]<=180){result[7]=22.5;}
            if (result[0]>=181 && result[0]<=360){result[7]=20;}
            if (result[0] >= 361 && result[0] <= 720) { result[7] = 17.5; }
            if (result[0] >720){result[7]=15;}
           //impostp de renda
            result[8] = ValorAdmin(result[4] - result[1]-result[6]-result[5], result[7]);
            //Valor liquido a receber
            result[9]= result[4]-result[5]-result[6]-result[8];
             //taxa liquida
            result[10] = result[2]-taxaCustodia- ((result[2] * result[7]) / 100);

            dataTable = MontarTabela(result);



            return dataTable;
        }

        private DataTable MontarTabela(Double [] result)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("nome");
            dataTable.Columns.Add("valor");
            dataTable.Clear();
            dataTable.Rows.Add("Dias corridos entre a data de compra e a de vencimento:", result[0]);
            dataTable.Rows.Add("Dias corridos entre a data de compra e a de vencimentDias corridos entre a data de compra e a de venda:", result[0]);
            dataTable.Rows.Add("Dias úteis entre a data de compra e a de vencimento:", "0");
            dataTable.Rows.Add("Dias úteis entre a data de compra e a de venda:", "0");
            dataTable.Rows.Add("Valor investido líquido:", String.Format("{0:R$ 0,0.00}", result[1]));
            dataTable.Rows.Add("Rentabilidade bruta (a.a.):", String.Format("{0: 0.00\\%}", result[2]));
            dataTable.Rows.Add("Taxa de Negociação (0,0%):", String.Format("{0: 0.00\\%}", 0));
            dataTable.Rows.Add("Taxa de administração na entrada:", String.Format("{0:R$ 0.00}", result[3]));
            dataTable.Rows.Add("Valor investido bruto:", String.Format("{0:R$ 0,0.00}", result[1] - result[3]));
            dataTable.Rows.Add("Valor bruto do resgate:", String.Format("{0:R$ 0,0.00}",result[4]));
            dataTable.Rows.Add("Valor da taxa de custódia do resgate:", String.Format("{0:R$ #,0.00}",result[5]));
            dataTable.Rows.Add("Valor da taxa de administração do resgate:", String.Format("{0:R$ #,0.00}", result[6]));
            dataTable.Rows.Add("Alíquota média de imposto de renda:", String.Format("{0: 0.00\\%}",result[7]));
            dataTable.Rows.Add("Imposto de renda:", String.Format("{0:R$ #,0.00}",result[8]));
            dataTable.Rows.Add("Valor líquido do resgate:", String.Format("{0:R$ #,0.00}", result[9]));
            dataTable.Rows.Add("Rentabilidade líquida após taxas e I.R. (a.a.):", String.Format("{0: 0.00\\%}", result[10]));

            return dataTable;
 
        }

    }
}
