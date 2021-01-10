//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>
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
        public static string token;
        public static bool Marcar(int idpessoa,int idprofissional,int idconvencao, int idtipoconsulta ,string desc,DateTime dataconsulta)
        {
            Consulta c = new Consulta();
            c.dataconsulta = dataconsulta;
            c.descricao = desc;
            c.idtipoconvencao = idconvencao;
            c.pessoa_idprofsaude = idprofissional;
            c.pessoa_idutente = idpessoa;
            c.tipoconsulta_idtipo = idtipoconsulta;
            ConsultaModel.token = token;
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

        public static DataTable TakeConsultas(int id)
        {
            return ConsultaModel.takeConsultas(id);
        }

        public static int DeleteConsulta(int idcon,string token)
        {
            return ConsultaModel.DeleteConsulta(idcon,token);
        }

    }
}
