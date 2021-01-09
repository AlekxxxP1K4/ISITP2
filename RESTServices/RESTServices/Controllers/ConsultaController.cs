using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTServices.Models;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace RESTServices.Controllers
{
    [ApiController]
    [Route("/[CONTROLLER]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConfiguration _connfiguration;

        public ConsultaController(IConfiguration configuration)
        {
            _connfiguration = configuration;
        }

        /// <summary>
        /// Post para registar uma consulta
        /// </summary>
        /// <param name="c">Recebe Consulta Objeto como parametro</param>
        /// <returns> retorna "Funcionou" caso sucesso</returns>
        [Authorize]
        [HttpPost("registar")]
        public string RegistaConsulta(Consulta c)
        {
            ConsultaModel consulta = new ConsultaModel(_connfiguration);
            
            return consulta.NewConsulta(c);
        }
        
        /// <summary>
        /// Get para trazer informacoes da consulta
        /// </summary>
        /// <param name="id">id consulta</param>
        /// <returns>Retorna o Objeto Consulta com os parametros</returns>
        [HttpGet("consulta/{id}")]
        public Consulta GetConsulta(int id)
        {

            ConsultaModel consulta = new ConsultaModel(_connfiguration);

            return consulta.TakeConsultaInfo(id);

        }

        /// <summary>
        /// "Apaga consulta" altera o seu estado, nada da base de dados é apagado
        /// </summary>
        /// <param name="id">id daconsulta</param>
        /// <returns>1 sucesso, 0/-1 nao executou ou erro</returns>
        [Authorize]
        [HttpDelete("DeleteConsulta/{id}")]
        public int DeleteConsulta(int id)
        {

            ConsultaModel consulta = new ConsultaModel(_connfiguration);

            return consulta.DeleteConsulta(id);

        }



    }
}
