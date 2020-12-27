using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Biblioteca;
using Npgsql;
using System.Configuration;

namespace SOAPServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static NpgsqlConnection conn = new NpgsqlConnection();
        private NpgsqlCommand cmd;
        private string sql = null;

        public string RegistrarUser(Utilizador u, Pessoa p)
        {
            try
            {
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
                conn.Open();
                sql = @"call Insert_Utente(:_user,:_name,:_nif,:_email,:_morada,:_tele,:_data,:_pass)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_user", u.Username);
                cmd.Parameters.AddWithValue("_pass", u.Password);
                cmd.Parameters.AddWithValue("_email", u.Email);
                cmd.Parameters.AddWithValue("_name", p.Nome);
                cmd.Parameters.AddWithValue("_tele", p.Telefone);
                cmd.Parameters.AddWithValue("_nif", p.Nif);
                cmd.Parameters.AddWithValue("_morada", p.Morada);
                cmd.Parameters.AddWithValue("_data", p.Data);

                cmd.ExecuteScalar();
                conn.Close();

                return "Done";

            }
            catch (Exception exception)
            {
                conn.Close();
                return "Error" + exception.Message.ToString();
                throw;
            }

        }
    }
}
