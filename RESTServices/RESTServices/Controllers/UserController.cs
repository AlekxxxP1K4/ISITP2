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
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                return utilizador.LoginResposta(idlogedin, tokenString);
            }
            else
                return utilizador.LoginResposta(-1, "");

        }

        /// <summary>
        /// Retorna a role do utilizador apos ter feito o loggin
        /// </summary>
        /// <param name="id">id da pessoa</param>
        /// <returns>retorn a id da role</returns>
        [Authorize]
        [HttpGet("role/{id}")]
        public int GetRoles(int id)
        {
            UserModel utilizador = new UserModel(_connfiguration);
            return utilizador.takeRole(id);
           
        }

        /// <summary>
        /// Muda a password do utilizador
        /// </summary>
        /// <param name="u">Recebe os dados do utilizador id pwantiga e nova</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("updateUser")]
        public string UpdateUser(User u)
        {
            UserModel utilizador = new UserModel(_connfiguration);

            return utilizador.UpdateUserPassword(u.id, u.pw, u.newpw);

        }

        private string GetTokenJWT()
        {
            var issuer = _connfiguration["Jwt:Issuer"];
            var audience = _connfiguration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);  //válido por 2 horas
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_connfiguration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }


    }
}
