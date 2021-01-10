//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>

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


        static public string UpdatePw(int id, string pw, string newpw,string token)
        {
            string var = RegistarModel.UpdateUser(id, pw, newpw, token);
            if(var== "sucesso")
            return var;
            return var;
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
