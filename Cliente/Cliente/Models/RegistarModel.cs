//<Authors> Oleksandr Sierov & Rafael Faria </Authors>
//<Email> a16991@alunos.ipca.pt & 17004@alunos.ipca.pt </Email>
//<Institution> IPCA - Instituto Politecnico do Cávado e do Ave </Institution>
//<Version=4.1/>
//<Date>10/01/2021 <Last Change/></Date>
//<OBS></OBS>
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class RegistarModel
    {


        static public string registar(SOAPServices.Pessoa p,SOAPServices.Utilizador u,int role)
        {
            SOAPServices.ServiceClient servico = new SOAPServices.ServiceClient();
            
            return servico.RegistrarUser(u,p,role);

        }

        static public string UpdateUser(int iduser, string pwlast,string pwnew,string token)
        {
            try
            {
                string url = "https://ipcaservicos.azurewebsites.net/User/updateUser";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(new
                { id = iduser, pw = pwlast, newpw = pwnew }), Encoding.UTF8, "application/json")).Result;

                string conteudo = JsonConvert.DeserializeObject<string>(resp.Content.ReadAsStringAsync().Result);
                return conteudo;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        
    }
}
