using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliente.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Cliente.View;

namespace Cliente.Controller
{
    class ConsultaController
    {
        public static bool Marcar(int idpessoa,int idprofissional,int idconvencao, int idtipoconsulta ,string desc,DateTime dataconsulta)
        {
            Consulta c = new Consulta();
            c.dataconsulta = dataconsulta;
            c.descricao = desc;
            c.idtipoconvencao = idconvencao;
            c.pessoa_idprofsaude = idprofissional;
            c.pessoa_idutente = idpessoa;
            c.tipoconsulta_idtipo = idtipoconsulta;

            if (ConsultaModel.MarcaConsulta(c) == "Funcionou")
            {
                return true;
            }
            else return false;
        }

        public static DataTable TakeConvencao()
        {
            return ConsultaModel.convencaoNomes();
        }

        public static DataTable TakeMedicos()
        {
            return ConsultaModel.medicosNomes();
        }

        public static DataTable TakeTipoConsulta()
        {
            return ConsultaModel.tipoconsultaNomes();
        }
        

    }
}
