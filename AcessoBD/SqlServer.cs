using AcessoBD.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessoBD
{
    public class SqlServer
    {

        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        private SqlConnection criarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //METODO PARA LIMPAR COLEÇÃO DE PARAMETRO ANDE DE ADICIONA NOVOS
        public void limparSqlParameterCollection()
        {
            sqlParameterCollection.Clear();
        }

        //METODO PARA ADICIONAR PARAMETRO A COLEÇÃO
        public void addSqlParameterCollection(String nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }


        // METODO PARA INSERT,UPDATE,DELETE 
        public Object excultarAcao(CommandType commandType, String comandoSqlStoredProcedure)
        {
            try
            {
                SqlConnection sqlConnection = criarConexao();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 7200; //MILESEGUNDOS
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = comandoSqlStoredProcedure;
                sqlCommand.Parameters.Clear();
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //METODO PARA REALIZAR CONSULTAR
        public DataTable execultarConsulta(CommandType commandType, String comandoSqlStoredProcedure)
        {
            try
            {
                SqlConnection sqlConnection = criarConexao();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 7200;
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = comandoSqlStoredProcedure;
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




    }
}
