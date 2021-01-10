//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.Models
{
    public class Consulta
    {
        #region atributos
        public int idconsulta { get; set; }
        public DateTime dataconsulta { get; set; }
        public string descricao { get; set; }
        public int estado { get; set; }
        public int idtipoconvencao { get; set; }
        public int pessoa_idutente { get; set; }
        public int pessoa_idprofsaude { get; set; }
        public int tipoconsulta_idtipo { get; set; }
        public int local_idlocal { get; set; }

        internal static int Parse(string content)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
