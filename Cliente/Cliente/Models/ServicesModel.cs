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
    class ServicesModel
    {
        #region VAR
        static HttpWebRequest request;
        static StringBuilder uri;
        static string url;
        #endregion

        static public bool checkEmailandNif(string email, int nif)
        {
            try
            {
                url = "http://localhost:61992/service/verificaNifeEmail/[NIF]&[EMAIL]";

                #region ConstroiURI
                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[NIF]", HttpUtility.UrlEncode(nif.ToString()));
                uri.Replace("[EMAIL]", HttpUtility.UrlEncode(email));
                #endregion
                request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

                #region EnviaPedidoAnalisaResposta
                bool resposta;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)     //via GET
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    resposta = bool.Parse(content);

                }

                return resposta;
                #endregion

            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        static public int CheckEmailinTable(string email)
        {
            try
            {
                url = "http://localhost:61992/service/email/[EMAIL]";

                #region ConstroiURI
                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[EMAIL]", HttpUtility.UrlEncode(email));
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
            catch (Exception)
            {
                return -1;
                throw;
            }
        }


        static public int CheckNIFinTable(int NIF)
        {
            try
            {
                url = "http://localhost:61992/service/nif/[NIF]";

                #region ConstroiURI
                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[NIF]", HttpUtility.UrlEncode(NIF.ToString()));
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
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        static public int CheckUserinTable(string user)
        {
            try
            {
                SOAPServices.ServiceClient service = new SOAPServices.ServiceClient();
                return service.VerificarUserinTable(user);
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }
    }
}
