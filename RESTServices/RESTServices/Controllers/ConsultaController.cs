using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTServices.Models;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace RESTServices.Controllers
{
    [ApiController]
    [Route("/consulta")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConfiguration _connfiguration;

        private readonly string connStr;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        string sql;


        public ConsultaController(IConfiguration configuration)
        {
            _connfiguration = configuration;
            connStr = _connfiguration.GetConnectionString("PostGreConnectionString");
            conn = new NpgsqlConnection(connStr);
        }

        [HttpPost("registar")]
        public string RegistaConsulta(Consulta c)
        {
            //bool result=false;
            DateTime day = DateTime.Now;
            try
            {
                conn.Open();
                sql = @"call marca_consulta(@_dataconsulta,@_descricao ,@_idtipoconvencao ,@_pessoa_idutente,@_pessoa_idprofsaude,@_tipoconsulta_idtipo ,@_local_idlocal,@_datamarcacao ,@_hora)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_dataconsulta", c.dataconsulta);
                cmd.Parameters.AddWithValue("_descricao", c.descricao);
                cmd.Parameters.AddWithValue("_idtipoconvencao", c.idtipoconvencao);
                cmd.Parameters.AddWithValue("_pessoa_idutente", c.pessoa_idutente);
                cmd.Parameters.AddWithValue("_pessoa_idprofsaude", c.pessoa_idprofsaude);
                cmd.Parameters.AddWithValue("_tipoconsulta_idtipo", c.tipoconsulta_idtipo);
                cmd.Parameters.AddWithValue("_local_idlocal", c.local_idlocal);
                cmd.Parameters.AddWithValue("_datamarcacao", day.Date);
                cmd.Parameters.AddWithValue("_hora", c.dataconsulta);

                cmd.ExecuteScalar();
                conn.Close();
                string result = "true";
                return result;


            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }
        
        [HttpGet("consulta")]
        public Consulta GetConsulta(int id)
        {
            Consulta c=new Consulta();
            DataTable dt= new DataTable();
           
            try
            {
                conn.Open();
                sql = @"select * from consulta_info(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", id);
                
                dt.Load(cmd.ExecuteReader());
              
                conn.Close();
                DataRow dr = dt.Rows[0];
                c.idconsulta = id;
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
                return null;
                throw;
            }

                
        }

    }
}
