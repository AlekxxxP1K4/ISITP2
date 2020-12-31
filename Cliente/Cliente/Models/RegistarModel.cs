using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class RegistarModel
    {



        public int registar()
        {
            SOAPServices.Service1Client servico = new SOAPServices.Service1Client();
            SOAPServices.Pessoa p = new SOAPServices.Pessoa();

            //servico.RegistrarUser();


            return 0;
        }
    }
}
