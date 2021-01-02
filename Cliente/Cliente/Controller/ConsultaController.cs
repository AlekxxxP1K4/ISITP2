using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliente.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Cliente.Controller
{
    class ConsultaController
    {
        public static int Marcar(Consulta c)
        {
            return ConsultaModel.AddConsulta1(c);
        }

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

        public List<string> GetConvencao()
        {
            List<string> conv = new List<string>();

            try
            {
                conn.Open();
                sql = @"select identificacao from convencao";
                cmd = new NpgsqlCommand(sql, conn);

                string result = Convert.ToString(cmd.ExecuteScalar());
                conv.Add(result);

                conn.Close();

                return conv;
            }
            catch (Exception)
            {
                conn.Close();
                return conv;
                throw;
            }
        }
    }
}
