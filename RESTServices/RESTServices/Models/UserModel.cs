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
using System.Linq;
using System.Threading.Tasks;

namespace RESTServices.Models
{
    public class UserModel
    {
        private readonly IConfiguration _connfiguration;

        private readonly string connStr;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        string sql;


        public UserModel(IConfiguration configuration)
        {
            _connfiguration = configuration;
            connStr = _connfiguration.GetConnectionString("PostGreConnectionString");
            conn = new NpgsqlConnection(connStr);
        }

        public int login(string user,string pw)
        {
            try
            {
                conn.Open();
                sql = @"select * from Login_User(:_username,:_password)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", user);
                cmd.Parameters.AddWithValue("_password", pw);

                int result = (int)cmd.ExecuteScalar();

                conn.Close();

                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return -3;
                throw;
            }
        }
        

        public AuthResponse LoginResposta(int idloged,string token)
        {
            AuthResponse response = new AuthResponse();
            response.logedid = idloged;
            response.token = token;
            return response;
        }

        
        public int takeRole(int id)
        {
            try
            {
                conn.Open();
                sql = @"select role_idrole from userroles where utilizador_iduser = @_id";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", id);

                int result = (int)cmd.ExecuteScalar();

                conn.Close();

                return result;
            }
            catch (Exception)
            {
                conn.Close();
                return -3;
                throw;
            }

        }


        public string UpdateUserPassword(int id,string pw, string newpw)
        {
            try
            {
                conn.Open();
                sql = @"select * from user_update(@_iduser,@pw,@newpw)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@_iduser", NpgsqlDbType.Integer).Value = id;
                cmd.Parameters.Add("@pw", NpgsqlDbType.Varchar).Value = pw;
                cmd.Parameters.Add("@newpw", NpgsqlDbType.Varchar).Value = newpw;

                int result = (int)cmd.ExecuteScalar();

                conn.Close();
                if (result == 1)
                return "sucesso";
                return "Pw errada";
            } 
            catch (Exception ex)
            {
                conn.Close();
                return "Fail " + ex.Message;
                throw;
            }
        }

    }


    public class User
    {
        public int id { get; set; }
        public string pw { get; set; }
        public string newpw { get; set; }
    }

    public class AuthResponse
    {
        public int logedid { get; set; }
        public string token { get; set; }
    }
}
