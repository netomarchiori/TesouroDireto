using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Titulos
    {
        public int idTitulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Vencimento { get; set; }
        public float TaxaCompra { get; set; }
        public double ValorCompra { get; set; }
        public float TaxaVenda { get; set; }
        public double ValorVenda { get; set; }
        public int idTituloTipo { get; set; }
        public DateTime AtualizadoEm { get;  set; }

        
        
    }
}
