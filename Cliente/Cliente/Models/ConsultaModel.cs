using Nancy.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Cliente.Models
{
    class ConsultaModel
    {

        static HttpWebRequest request;
        static StringBuilder uri;
        static string url;

        public static int AddConsulta1(Consulta c)
        {
            try
            {
                url = "http://localhost:61992/consulta/[REGISTAR]";

                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[REGISTAR]", HttpUtility.UrlEncode(c.ToString()));

                request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

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
            }
            catch(Exception ex)
            {
                throw new Exception("Add erro!", ex);
            }
        }


        public static string convencaoNomes()
        {
            DataTable dt = new DataTable();
            try
            {
                SOAPServices.Service1Client servico = new SOAPServices.Service1Client();
                dt=servico.ConvencaoInfo();
                return dt.TableName.ToString();
            }
            catch (Exception ex)
            {
                
                return "null nao entrei" + ex.Message;
                
            }

        }
    }
}
