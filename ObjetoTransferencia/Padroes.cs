using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Padroes
    {
        public const string TITULO_TIPO = "FFFF9C\"><b>(.+)</b>";
        public const string TITULO_DESC = "listing[0]\".*>(.+)</TD>";
        public const string TITULO_VENCIMENTO = "listing\".*>(\\w{2}/\\w{2}/\\w{4})</TD>";
        public const string TITULO_COMPRA_VENDA_TAXA = "listing\".*>(\\d{1,2},\\d{2})%</TD>|listing\".*r>(-)</c";    // NA QUARTA RECUPERA DUAS TAXA =>  1 - COMPRA 2 - VENDA
        public const string TITULO_COMPRA_VENDA_VALOR = "listing\".*>?R\\$\\s*(\\d{0,3}.*\\d{0,3},\\d{0,2})</TD>|listing\".*r>?\\s(-)\\s</c";  // NA QUARTA RECUPERA DOIS VALORES 1 - COMPRA 2 - VENDA
        public const string ATUALIZADOEM = "\\d{2}-\\d{2}-\\d{4}\\s\\d{2}:\\d{2}:\\d{2}";
        public const string NUMEROS = "[a-z]*";
        public const string DATAS = "\\d{2}/\\d{2}/\\d{4}";



    }
}
