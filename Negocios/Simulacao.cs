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
        private const Double taxaCustodia = 0.000008;     //Taxa de custódia de 0,30% a.a. (ao dia (0,003/365)100 = 0,000008) sobre o valor dos títulos.
        private Double[] taxaIR = new Double[4] { 0.225, 0.2, 0.175, 0.150 }; //Ver cometario no inicio (VALORES DO IMPOSTO DE RENDA) 
        private Int32 diasUteis,diasInvestidos; 
    
    
        public Double JurosComposto(Double capital,double taxa,Double tempo)
        {
            Double M = 0;
            M = Math.Pow((1+taxa),tempo);
            M = capital * M;
            return M;
        }
    
        public DataTable RealizarSimulacao(DadosCalculos dadosCalculos)
        {
            DataTable dataTable = new DataTable();
            diasInvestidos = (dadosCalculos.dataVencimento - dadosCalculos.dataCompra).Days;
            dataTable.NewRow();
            dataTable.Rows.Add("Dias corridos entre a data de compra e a de vencimento:", diasInvestidos);






            return dataTable;
        }
    
    }
}
