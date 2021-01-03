using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliente.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Cliente.Controller
{
    class ConsultaController
    {
        public static int Marcar(Consulta c)
        {
            return ConsultaModel.AddConsulta1(c);
        }

        public static DataTable TakeConvencao()
        {
            return ConsultaModel.convencaoNomes();
        }

        
    }
}
