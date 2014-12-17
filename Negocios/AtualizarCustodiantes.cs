using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjetoTransferencia;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using AcessoBD;
using System.Data;
namespace Negocios
{
    public class AtualizarCustodiantes
    {

        private string AtualizadoEm = "";
        AgenteCustodiaColecao agenteCustodiaColecao = new AgenteCustodiaColecao();
        
        private static string buscarInformacao()
        {
            string statusHttpWebResponse = "";
            try
            {
                HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(new Uri("http://www3.tesouro.gov.br/tesouro_direto/consulta_titulos_novosite/consulta_ranking.asp"));
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

            catch (Exception ex)
            {
                return "ERRO 001: " + ex.Message;
            }

            return statusHttpWebResponse;

        }

        public string getInformacoes()
        {
            string informacoes = buscarInformacao();
            
            /*Encoding utf8 = Encoding.UTF8;
            Encoding iso = Encoding.GetEncoding("iso-8859-1");
            byte[] isobytes = iso.GetBytes(informacoes);
            byte[] utf8bytes =  Encoding.Convert(iso, utf8, isobytes);
            char [] utf8char = new char[utf8.GetCharCount(utf8bytes,0,utf8bytes.Length)];
            utf8.GetChars(utf8bytes,0,utf8bytes.Length,utf8char,0);
            string n = new string(utf8char);
            //informacoes = utf8.GetString(utf8bytes);*/
            return informacoes;
        }

        private void buscarCustodiantes()
        {
            string texto = getInformacoes();
          
            //BUSCA CUSTODIANTES
            Match matchGeral = Regex.Match(texto, Padroes.AGENTE_CUSTODIA);
            while (matchGeral.Success)
            {
                AgenteCustodia agenteCustodia = new AgenteCustodia();
                agenteCustodia.InstituicaoFinan = matchGeral.Groups[1].ToString();
                agenteCustodia.Integrado = matchGeral.Groups[2].ToString();
                agenteCustodia.AplicacaoProgramada = matchGeral.Groups[3].ToString();
                agenteCustodia.TaxaAdmin = Convert.ToDouble(matchGeral.Groups[4].ToString());
                agenteCustodia.TaxaDescricao = matchGeral.Groups[5].ToString();
                agenteCustodia.Repasse = matchGeral.Groups[6].ToString();
                agenteCustodiaColecao.Add(agenteCustodia);              
                matchGeral = matchGeral.NextMatch();
             }
             // BUSCA DATA DE ATUALIZAÇÃO DOS TITULOS
            matchGeral = Regex.Match(texto, Padroes.ATUALIZADOEM_CUSTODIANTE);
            AtualizadoEm = matchGeral.Groups[0].ToString();
        
        }

        public void AtualizarAgentes()
        {
            buscarCustodiantes();

            try
            {
                SqlServer sqlServer = new SqlServer();
                sqlServer.limparSqlParameterCollection();
                object a = sqlServer.excultarAcao(CommandType.StoredProcedure, "uspAgenteCustodiaDeletar");

                foreach (AgenteCustodia agenteCustodiante in agenteCustodiaColecao)
                {
                    sqlServer.addSqlParameterCollection("@InstituicaoFinan", agenteCustodiante.InstituicaoFinan);
                    sqlServer.addSqlParameterCollection("@Integrado", agenteCustodiante.Integrado);
                    sqlServer.addSqlParameterCollection("@AplicacaoProgramada", agenteCustodiante.AplicacaoProgramada);
                    sqlServer.addSqlParameterCollection("@TaxaAdimin", agenteCustodiante.TaxaAdmin);
                    sqlServer.addSqlParameterCollection("@TaxaDescricao", agenteCustodiante.TaxaDescricao);
                    sqlServer.addSqlParameterCollection("@Repasse", agenteCustodiante.Repasse);
                    sqlServer.addSqlParameterCollection("@AtualizadoEm", AtualizadoEm);
                    string retorno = sqlServer.excultarAcao(CommandType.StoredProcedure, "uspAgenteCustodia").ToString();
                    sqlServer.limparSqlParameterCollection();
                }
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }


        }


    }
}
