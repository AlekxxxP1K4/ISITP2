using Nancy.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cliente.Models;
using System.Net.Http;

namespace Cliente.Controller
{
    public class LoginController
    {
        static public int login(string user,string pw)
        {
            return LoginModel.LoginIn(user, pw);
        }

        static public int role(int id)
        {
            return LoginModel.TakeUser(id);
        }

        static public string namelogedin(int id)
        {
            return LoginModel.namelogedin(id);
        }
 
    }
}
