using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class RegistarModel
    {


        static public string registar(SOAPServices.Pessoa p,SOAPServices.Utilizador u)
        {
            SOAPServices.Service1Client servico = new SOAPServices.Service1Client();
            
            return servico.RegistrarUser(u,p);

        }

        
    }
}
