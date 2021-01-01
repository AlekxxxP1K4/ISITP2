using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliente.Models;

namespace Cliente.Controller
{
    class RegistarController
    {
        static public string registar(string user,string nome,int nif,string email,string morada,string contacto,DateTime dataNascimento,string password,int role)
        {
            try
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

                return RegistarModel.registar(p, u,role);


            }catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

        static public bool CheckEmailandNif(string email,int nif)
        {
            return ServicesModel.checkEmailandNif(email, nif);
        }

        static public int CheckifEmailExist(string email)
        {
            return ServicesModel.CheckEmailinTable(email);
        }

        static public int CheckifNifExist(int nif)
        {
            return ServicesModel.CheckNIFinTable(nif);
        }

        static public int CheckifUserExists(string user)
        {
            return ServicesModel.CheckUserinTable(user);
        }
    }
}
