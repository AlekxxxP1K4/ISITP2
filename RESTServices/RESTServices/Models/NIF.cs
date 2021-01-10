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

namespace RESTServices.Models
{
    public class NIF
    {

        public class Root
        {
            public string result { get; set; }
            public bool nif_validation { get; set; }
            public bool is_nif { get; set; }
        }
    }
}
