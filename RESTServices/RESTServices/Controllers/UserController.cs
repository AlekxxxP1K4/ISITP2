using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace RESTServices.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _connfiguration;

        private readonly string connStr;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        string sql;


        public UserController(IConfiguration configuration)
        {
            _connfiguration = configuration;
            connStr = _connfiguration.GetConnectionString("PostGreConnectionString");
            conn = new NpgsqlConnection(connStr);
        }

        [HttpGet("login/{user}&{pw}")]
        public int GetLogin(string user, string pw)
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

        [HttpGet("role/{id}")]
        public int GetRoles(int id)
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

        
    }
}
