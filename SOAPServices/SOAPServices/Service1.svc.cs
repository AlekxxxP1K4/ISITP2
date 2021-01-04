using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Npgsql;
using System.Configuration;
using SOAPServices.Model;
using System.Data;

namespace SOAPServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static NpgsqlConnection conn = new NpgsqlConnection();
        private NpgsqlCommand cmd;
        private string sql = null;
        /// <summary>
        /// Registar Utilizador na Base de dados
        /// </summary>
        /// <param name="u">Utilizador</param>
        /// <param name="p">Pessoa</param>
        /// <returns></returns>
        public string RegistrarUser(Utilizador u, Pessoa p,int role)
        {
            try
            {
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
                conn.Open();
                sql = @"call Insert_Utente(@_user,@_name,@_nif,@_email,@_morada,@_tele,@_data,@_pass,@_role)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_user", u.Username);
                cmd.Parameters.AddWithValue("_pass", u.Password);
                cmd.Parameters.AddWithValue("_email", u.Email);
                cmd.Parameters.AddWithValue("_name", p.Nome);
                cmd.Parameters.AddWithValue("_tele", p.Telefone);
                cmd.Parameters.AddWithValue("_nif", p.Nif);
                cmd.Parameters.AddWithValue("_morada", p.Morada);
                cmd.Parameters.AddWithValue("_data", p.Data);
                cmd.Parameters.AddWithValue("_role", role);

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

        /// <summary>
        /// Verifica a existencia de um utilizador com esse nome
        /// </summary>
        /// <param name="user">user name</param>
        /// <returns></returns>
        public int VerificarUserinTable(string user)
        {
            try
            {
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
                conn.Open();
                sql = @"select count(*) from utilizador where username = @_username";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", user);

                int result = Convert.ToInt32(cmd.ExecuteScalar());

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


        public string nameLogedin(int id)
        {
            try
            {
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
                conn.Open();
                sql = @"select nome from pessoa where idpessoa = @_id";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", id);

                string result = cmd.ExecuteScalar().ToString();

                conn.Close();

                return result;
            }
            catch (Exception es)
            {
                conn.Close();
                return es.Message;
                throw;
            }
        }


        public DataTable ConsultasUtente(int id)
        {
            DataTable dt = new DataTable("Consultas");
            try
            {
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
                conn.Open();
                sql = @"select * from consultas(@_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", id);

                dt.Load(cmd.ExecuteReader());
                
                conn.Close();

                return dt;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
                throw;
            }
        }


        public DataTable ConvencaoInfo()
        {
           
            DataTable dt = new DataTable("Convencao");
            try
            {
                conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
                conn.Open();
                sql = @"select idtipoconv,tipo from tipoconvencao";
                cmd = new NpgsqlCommand(sql, conn);
                
                dt.Load(cmd.ExecuteReader());

                
                conn.Close();

                return dt;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
                throw;
            }
        }
    }
}
