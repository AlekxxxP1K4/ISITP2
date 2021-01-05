using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using RESTServices.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RESTServices.Controllers
{
    [ApiController]
    [Route("/[CONTROLLER]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _connfiguration;

        public UserController(IConfiguration configuration)
        {
            _connfiguration = configuration;
        }

        /// <summary>
        /// Executa loggin na aplicacao
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="pw">palavra pass</param>
        /// <returns>Retorna id de utilizador e token</returns>
        [HttpGet("login/{user}&{pw}")]
        public AuthResponse GetLogin(string user, string pw)
        {
            UserModel utilizador = new UserModel(_connfiguration);
            int idlogedin = utilizador.login(user, pw);

            if (idlogedin > 0)
            {
                var tokenString = GetTokenJWT();
                return utilizador.LoginResposta(idlogedin,tokenString);
                //cria token
            }
            else
                return idlogedin;

        }

        /// <summary>
        /// Retorna a role do utilizador apos ter feito o loggin
        /// </summary>
        /// <param name="id">id da pessoa</param>
        /// <returns>retorn a id da role</returns>
        [HttpGet("role/{id}")]
        public int GetRoles(int id)
        {
            UserModel utilizador = new UserModel(_connfiguration);
            return utilizador.takeRole(id);
           
        }

        
    }
}
