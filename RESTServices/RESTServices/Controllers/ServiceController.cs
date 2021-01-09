using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;
using System.IO;
using RESTServices.Models;
using System.Text.RegularExpressions;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace RESTServices.Controllers
{
    [ApiController]
    [Route("/[CONTROLLER]")]
    public class ServiceController:ControllerBase
    {
        private readonly IConfiguration _connfiguration;


        public ServiceController(IConfiguration configuration)
        {
            _connfiguration = configuration;

        }
        /// <summary>
        /// get para verificar se o nif é valido e se o email esta sintaticamente correto
        /// </summary>
        /// <param name="nif">NIF da pessoa</param>
        /// <param name="email">Email da pessoa</param>
        /// <returns>true se tudo ok, false caso nao aceite algum</returns>
        [HttpGet("verificaNifeEmail/{nif}&{email}")]
        public bool GetNifEmail(string nif,string email)
        {
                ServiceModel service = new ServiceModel(_connfiguration);
                return service.GetNifEmail(nif, email);
        }


        //verifica se o nif e email esta na tabela
        /// <summary>
        /// Verifica se o nif existe na tabela
        /// </summary>
        /// <param name="nif">Nif</param>
        /// <returns>Retora o resultado da query</returns>
        [HttpGet("nif/{nif}")]
        public int CheckNIF(int nif)
        {
            ServiceModel service = new ServiceModel(_connfiguration);
            return service.CheckNIF(nif);
        }

        /// <summary>
        /// Verifica se existe email na tabela
        /// </summary>
        /// <param name="email">Email da pessoa</param>
        /// <returns>Retorna resultado da query</returns>
        [HttpGet("email/{email}")]
        public int CheckEmail(string email)
        {
            ServiceModel service = new ServiceModel(_connfiguration);
            return service.CheckEmail(email);
        }


    }
}
