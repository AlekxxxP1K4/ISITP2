using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Controller
{
    class RegistarController
    {
        static public int registar(string user,string nome,int nif,string email,string morada,string contacto,DateTime dataNascimento,string password)
        {
            SOAPServices.Pessoa p = new SOAPServices.Pessoa();
            SOAPServices.Utilizador u = new SOAPServices.Utilizador();

            u.Username = user;
            u.Password = password;
            u.Email = email;
            p.Data = dataNascimento;
            p.Morada = morada;
            p.Nif = nif;
            p.Nome = nome;
            p.Telefone = contacto;
            return 1;
        }
    }
}
