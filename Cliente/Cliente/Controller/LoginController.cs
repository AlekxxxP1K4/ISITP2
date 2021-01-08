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
        public static string token;
        static public AuthResponse login(string user,string pw)
        {
            AuthResponse auth = new AuthResponse();
            
            auth=LoginModel.LoginIn(user, pw);
            token = auth.token;
            return auth;
        }

        static public int role(int id)
        {
            return LoginModel.TakeUser(id,token);
        }

        static public string namelogedin(int id)
        {
            return LoginModel.namelogedin(id);
        }
 
    }
}
