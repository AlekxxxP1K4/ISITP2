//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>

using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServices.Models
{
    public class ConsultaModel
    {
        private readonly IConfiguration _connfiguration;

        private readonly string connStr;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        string sql;


        public ConsultaModel(IConfiguration configuration)
        {
            _connfiguration = configuration;
            connStr = _connfiguration.GetConnectionString("PostGreConnectionString");
            conn = new NpgsqlConnection(connStr);
        }

        /// <summary>
        /// Entra na base de dados e cria a consulta
        /// </summary>
        /// <param name="c">Consulta </param>
        /// <returns>retorna Funcionou caso sucesso ou erro</returns>
        public string NewConsulta(Consulta c)
        {
            DateTime day = DateTime.Now;
            try
            {
                conn.Open();
                sql = @"call marca_consulta(@_dataconsulta,@_descricao ,@_idtipoconvencao ,@_pessoa_idutente,@_pessoa_idprofsaude,@_tipoconsulta_idtipo ,@_datamarcacao ,@_hora)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_dataconsulta", c.dataconsulta);
                cmd.Parameters.AddWithValue("_descricao", c.descricao);
                cmd.Parameters.AddWithValue("_idtipoconvencao", c.idtipoconvencao);
                cmd.Parameters.AddWithValue("_pessoa_idutente", c.pessoa_idutente);
                cmd.Parameters.AddWithValue("_pessoa_idprofsaude", c.pessoa_idprofsaude);
                cmd.Parameters.AddWithValue("_tipoconsulta_idtipo", c.tipoconsulta_idtipo);
                cmd.Parameters.AddWithValue("_datamarcacao", day.Date);
                cmd.Parameters.AddWithValue("_hora", c.dataconsulta);

                cmd.ExecuteScalar();
                conn.Close();
                string result = "Funcionou";
                return result;


            }
            catch (Exception ex)
            {
                conn.Close();
                return "Erro no servico" + ex.Message;
                throw;
            }
        }


        /// <summary>
        /// Vai a base de dados e tras a tabela com as informacoes da consulta
        /// </summary>
        /// <param name="idConsulta">Consulta id</param>
        /// <returns>Objeto consulta</returns>
        public Consulta TakeConsultaInfo(int idConsulta)
        {
            Consulta c = new Consulta();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                sql = @"select * from consulta_info(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@_id", NpgsqlDbType.Integer).Value = idConsulta;

                dt.Load(cmd.ExecuteReader());

                conn.Close();
                DataRow dr = dt.Rows[0];
                c.idconsulta = idConsulta;
                c.dataconsulta = (DateTime)dr["_dataconsulta"];
                c.descricao = (string)dr["_descricao"];
                c.estado = (int)dr["_estado"];
                c.idtipoconvencao = (int)dr["_idtipoconvencao"];
                c.pessoa_idutente = (int)dr["_pessoa_idutente"];
                c.pessoa_idprofsaude = (int)dr["_pessoa_idprofsaude"];
                c.tipoconsulta_idtipo = (int)dr["_tipoconsulta_idtipo"];
                c.local_idlocal = (int)dr["_local_idlocal"];

                return c;


            }
            catch (Exception)
            {
                conn.Close();
                return null;
                throw;
            }
        }


        public int DeleteConsulta(int idConsulta)
        {
            Consulta c = new Consulta();

            try
            {
                conn.Open();
                sql = @"select * from consulta_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@_id", NpgsqlDbType.Integer).Value = idConsulta;

                int result=(int)cmd.ExecuteScalar();

                conn.Close();
                

                return result;


            }
            catch (Exception)
            {
                conn.Close();
                return -1;
                throw;
            }
        }

    }
     public class Consulta
        {
            #region atributos
            public int idconsulta { get; set; }
            public DateTime dataconsulta { get; set; }
            public string descricao { get; set; }
            public int estado { get; set; }
            public int idtipoconvencao { get; set; }
            public int pessoa_idutente { get; set; }
            public int pessoa_idprofsaude { get; set; }
            public int tipoconsulta_idtipo { get; set; }
            public int local_idlocal { get; set; }



            #endregion
        }  
}
