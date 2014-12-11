using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ObjetoTransferencia;
using System.Text.RegularExpressions;
using AcessoBD;
using System.Data;

namespace Negocios
{
    public class AtualizarInformacoes
    {
        private string informacoes = "";
        const int LIMITETITULO = 50;
        private string[] desc = new string[LIMITETITULO];
        private string[] vencimento = new string[LIMITETITULO];
        private string[] txcompravenda = new string[LIMITETITULO];
        private string[] vlcompravenda = new string[LIMITETITULO];
        private string AtualizadoEm;
        int quantTitulos = 0;
        
        TitulosColecao titulosColecao = new TitulosColecao();

       
        private static string buscarInformacao()
        {
            string statusHttpWebResponse = "";
            try
            {
                HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(new Uri("http://www3.tesouro.gov.br/tesouro_direto/consulta_titulos_novosite/consultatitulos.asp"));
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                statusHttpWebResponse = httpWebResponse.StatusDescription;
                if (statusHttpWebResponse.Equals("OK"))
                {
                    Stream stream = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream);
                    statusHttpWebResponse = streamReader.ReadToEnd();
                    streamReader.Close();
                    stream.Close();
                    httpWebResponse.Close();
                    return statusHttpWebResponse; 
                 }
                
            }

            catch(Exception ex)
            {
                return "ERRO 001: " + ex.Message;
            }
            
            return statusHttpWebResponse;
            
        }


        public string getInformacoes() 
        {
            informacoes = buscarInformacao();
            return informacoes;
        }


        public void buscarDados(string texto) 
        {
            int cont = 0;
            //BUSCA DESCRIÇÃO DOS TITULOS
            Match matchGeral = Regex.Match(texto, Padroes.TITULO_DESC);
            while (matchGeral.Success && cont<LIMITETITULO)
             {   
                 desc[cont] = matchGeral.Groups[1].ToString();
                 matchGeral = matchGeral.NextMatch();
                 cont++;
                 quantTitulos += 1;
             }
            // BUSCA VENCIMENTO DOS TITULOS
            cont = 0;
            matchGeral = Regex.Match(texto, Padroes.TITULO_VENCIMENTO);
            while (matchGeral.Success && cont < LIMITETITULO)
            {
                vencimento[cont] = matchGeral.Groups[1].ToString();
                matchGeral = matchGeral.NextMatch();
                cont++;
            }
            // BUSCA TAXA DE COMPRA E VENDA DOS TITULOS
            cont = 0;
            matchGeral = Regex.Match(texto, Padroes.TITULO_COMPRA_VENDA_TAXA);
            while (matchGeral.Success && cont < LIMITETITULO)
            {
                txcompravenda[cont] = matchGeral.Groups[1].ToString();
                matchGeral = matchGeral.NextMatch();
                cont++;
            }
            // BUSCA VALOR DE COMPRA E VENDA DOS TITULOS
            cont = 0;
            matchGeral = Regex.Match(texto, Padroes.TITULO_COMPRA_VENDA_VALOR);
            while (matchGeral.Success && cont < LIMITETITULO)
            {
                vlcompravenda[cont] = matchGeral.Groups[1].ToString();
                matchGeral = matchGeral.NextMatch();
                cont++;
            }
            // BUSCA DATA DE ATUALIZAÇÃO DOS TITULOS
            matchGeral = Regex.Match(texto, Padroes.ATUALIZADOEM);
            AtualizadoEm = matchGeral.Groups[0].ToString();
           
           
            

       }

           
        public TitulosColecao montarColecaoTitulo(string texto)
        {
            buscarDados(texto);
            
            int cont = 0,aux=0;
            try
            {
                while (cont < quantTitulos && cont < LIMITETITULO)
                {
                    Titulos titulos = new Titulos();
                    titulos.Descricao = desc[cont];
                    titulos.Vencimento = Convert.ToDateTime(vencimento[cont]);
                   
                    try { titulos.TaxaCompra = float.Parse(txcompravenda[aux]); }
                    catch (Exception ex) { titulos.TaxaCompra = 0; }
                    try { titulos.ValorCompra = Convert.ToDouble(vlcompravenda[aux]); }
                    catch (Exception ex) { titulos.ValorCompra = 0; }
                  
                    try { titulos.TaxaVenda = float.Parse(txcompravenda[aux + 1]); }
                    catch (Exception ex) { titulos.TaxaVenda = 0; }
                    try { titulos.ValorVenda = Convert.ToDouble(vlcompravenda[aux + 1]); }
                    catch (Exception ex) { titulos.ValorVenda = 0; }
                    titulos.AtualizadoEm = Convert.ToDateTime(AtualizadoEm);
                    titulosColecao.Add(titulos);
                    cont++;
                    aux +=2;
                }
                return titulosColecao;
               
            }
            catch (Exception ex)
            {
               throw new Exception (ex.Message);
            }


            
        }

                    
        public void salvarBD(TitulosColecao tituloColecao)
        {
            try
            {
                SqlServer sqlServer = new SqlServer();
               sqlServer.limparSqlParameterCollection();
               object a = sqlServer.excultarAcao(CommandType.StoredProcedure, "uspTitulosDeletar");

                foreach (Titulos titulos in titulosColecao)
                {
                    sqlServer.addSqlParameterCollection("@Descricao", titulos.Descricao);
                    sqlServer.addSqlParameterCollection("@Vencimento", titulos.Vencimento);
                    sqlServer.addSqlParameterCollection("@TaxaCompra", titulos.TaxaCompra);
                    sqlServer.addSqlParameterCollection("@ValorCompra", titulos.ValorCompra);
                    sqlServer.addSqlParameterCollection("@TaxaVenda", titulos.TaxaVenda);
                    sqlServer.addSqlParameterCollection("@ValorVenda", titulos.ValorVenda);
                    if (titulos.Descricao.IndexOf("NTNB") > -1)
                    { sqlServer.addSqlParameterCollection("@idTituloTipo", 1); }
                    else if (titulos.Descricao.IndexOf("LTN") > -1 || titulos.Descricao.IndexOf("NTNF") > -1)
                    { sqlServer.addSqlParameterCollection("@idTituloTipo", 2); }
                    else if (titulos.Descricao.IndexOf("LFT") > -1)
                    { sqlServer.addSqlParameterCollection("@idTituloTipo", 3); }
                    else if (titulos.Descricao.IndexOf("NTNC") > -1)
                    { sqlServer.addSqlParameterCollection("@idTituloTipo", 4); }
                    sqlServer.addSqlParameterCollection("@AtualizadoEm", titulos.AtualizadoEm);
                    string retorno = sqlServer.excultarAcao(CommandType.StoredProcedure, "uspTituloInserir").ToString();
                    sqlServer.limparSqlParameterCollection();
                }
            }
            catch(Exception ex)
            { throw new Exception(ex.Message); }

 
        }
      

    }
}
