using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;
using System.IO;
using RESTServices.Models;
using System.Text.RegularExpressions;

namespace RESTServices.Controllers
{
    [ApiController]
    [Route("/service")]
    public class ServiceController:ControllerBase
    {

        [HttpGet("verificaNifeEmail/{nif}&{email}")]
        public bool GetNifEmail(string nif,string email)
        {
            #region verifica Nif
            HttpWebRequest request;
            StringBuilder uri;
            string url;
            string apiKey = "6ff8cde4d1d8edb67e046f7d13d6de4a";
            string content;
            NIF.Root n;

            //Weather URL
            url = "https://www.nif.pt/?json=1&q=[NIF]&key=[APIKEY]";

            #region ConstroiURI
            uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[NIF]", HttpUtility.UrlEncode(nif));
            uri.Replace("[APIKEY]", apiKey);
            #endregion
            request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            #region EnviaPedidoAnalisaResposta
            //analisa resposta
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)     //via GET
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }
                content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                n = JsonConvert.DeserializeObject<NIF.Root>(content);

            }
            #endregion
            #endregion

            #region verifica email

            Regex rg = new Regex(@"^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}"
                             + "\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\"
                             + ".)+))([a-zA-Z]{2,4}|[0,9]{1,3})(\\]?)$");
           


            #endregion

            if (rg.IsMatch(email)==true && n.is_nif==true)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }



    }
}
