using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class RegistarModel
    {


        static public string registar(SOAPServices.Pessoa p,SOAPServices.Utilizador u,int role)
        {
            SOAPServices.ServiceClient servico = new SOAPServices.ServiceClient();
            
            return servico.RegistrarUser(u,p,role);

        }

        
    }
}
