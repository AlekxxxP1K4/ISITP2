using Nancy.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Cliente.Models
{
    class ConsultaModel
    {

        
        static string url;
        public static string token;

        
        public static DataTable convencaoNomes()
        {
            DataTable dt = new DataTable();
            try
            {
                SOAPServices.ServiceClient servico = new SOAPServices.ServiceClient();
                dt=servico.ConvencaoInfo();
                return dt;
            }
            catch (Exception)
            {
                
                return null;
                
            }

        }

        public static DataTable medicosNomes()
        {
            DataTable dt = new DataTable();
            try
            {
                SOAPServices.ServiceClient servico = new SOAPServices.ServiceClient();
                dt = servico.Medicos();
                return dt;
            }
            catch (Exception)
            {

                return null;

            }

        }

        public static DataTable tipoconsultaNomes()
        {
            DataTable dt = new DataTable();
            try
            {
                SOAPServices.ServiceClient servico = new SOAPServices.ServiceClient();
                dt = servico.TipoConsulta();
                return dt;
            }
            catch (Exception)
            {

                return null;

            }

        }
        

        public static string MarcaConsulta(Consulta c)
        {
            try
            {
                url = "https://ipcaservicos.azurewebsites.net/consulta/registar";

               
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                string jsonString = JsonConvert.SerializeObject(c);
                var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(url, stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resposta = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                    //string resposta = response.Content.ReadAsStringAsync().Result;
                    return resposta;
                }
                else
                {
                    return response.IsSuccessStatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Erro "+ex.Message;
                throw new Exception("Add erro!", ex);
            }

        }

        public static DataTable takeConsultas(int id)
        {

            DataTable dt = new DataTable();
            SOAPServices.ServiceClient service = new SOAPServices.ServiceClient();
            dt=service.ConsultasUtente(id);
            return dt;
        }

        public static int DeleteConsulta(int idconsulta,string token)
        {
            try
            {
                url = "https://ipcaservicos.azurewebsites.net/consulta/DeleteConsulta/[ID]";
                url = url.Replace("[ID]", idconsulta.ToString());

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.DeleteAsync(url).Result;
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
            catch (Exception ex)
            {
                return -2;
                throw new Exception("Add erro!", ex);
            }

        }

    }
}
