//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>
using Nancy.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class LoginModel
    {
              #region VAR
               static  HttpWebRequest request;
               static StringBuilder uri;
               static string url;
              #endregion


        static public AuthResponse LoginIn(string user, string pass)
        {
            
            try
            {
                
                url = "https://ipcaservicos.azurewebsites.net/user/login/[USER]&[PW]";

                
                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[USER]", HttpUtility.UrlEncode(user));
                uri.Replace("[PW]", HttpUtility.UrlEncode(pass));

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uri.ToString());

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync(uri.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    AuthResponse resposta = JsonConvert.DeserializeObject<AuthResponse>(response.Content.ReadAsStringAsync().Result);
                    //string resposta = response.Content.ReadAsStringAsync().Result;
                    return resposta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }

        static public int TakeUser(int id,string token)
        {
            
            
            url = "https://ipcaservicos.azurewebsites.net/user/role/[ID]";

            uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[ID]", HttpUtility.UrlEncode(id.ToString()));

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri.ToString());

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = client.GetAsync(uri.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                int resposta = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
                //string resposta = response.Content.ReadAsStringAsync().Result;
                return resposta;
            }
            else
            {
                return -1;
            }
        }
        static public string namelogedin(int id)
        {
            try
            {
                SOAPServices.ServiceClient servico = new SOAPServices.ServiceClient();
                return servico.nameLogedin(id);
            }catch(Exception ex)
            {
                return ex.Message;
            }
            
        }

        
    }

    public class AuthResponse
    {
        public int logedid { get; set; }
        public string token { get; set; }
    }
}
