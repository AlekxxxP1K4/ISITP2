using Nancy.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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


        static public int LoginIn(string user, string pass)
        {
            
            try
            {
                
                url = "http://localhost:61992/user/login/[USER]&[PW]";

                #region ConstroiURI
                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[USER]", HttpUtility.UrlEncode(user));
                uri.Replace("[PW]", HttpUtility.UrlEncode(pass));
                #endregion
                request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

                #region EnviaPedidoAnalisaResposta
                int resposta;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)     //via GET
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    resposta = int.Parse(content);

                }

                return resposta;
                #endregion


            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }

        static public int TakeUser(int id)
        {
            
            //Weather URL
            url = "http://localhost:61992/user/role/[ID]";

            #region ConstroiURI
            uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[ID]", HttpUtility.UrlEncode(id.ToString()));
            #endregion
            request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            #region EnviaPedidoAnalisaResposta
            int resposta;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)     //via GET
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                resposta = int.Parse(content);

            }
            #endregion
            return resposta;
        }
        static public string namelogedin(int id)
        {
            try
            {
                SOAPServices.Service1Client servico = new SOAPServices.Service1Client();
                return servico.nameLogedin(id);
            }catch(Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
