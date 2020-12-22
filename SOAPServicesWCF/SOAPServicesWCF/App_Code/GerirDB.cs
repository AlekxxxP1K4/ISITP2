using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Npgsql;
using System.Configuration;
using Biblioteca;

/// <summary>
/// Summary description for GerirDB
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GerirDB : System.Web.Services.WebService {

    public GerirDB () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    
    //static string connstring = String.Format("Server={0};Port={1};" + "User Id={2};Password={3};Database={4};", "localhost", 5432, "postgres", 123, "aad");
    private static NpgsqlConnection conn= new NpgsqlConnection();
    private NpgsqlCommand cmd;
    private string sql = null;

   // conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["turismoConnectionString"].ConnectionString);



    /// <summary>
    /// Metodo para registar o utilizador na base de dados
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    [WebMethod(Description ="Registar Utilizador")]
    public string RegistrarUser(Utilizador u,Pessoa p)
    {
        try
        {
            conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
            conn.Open();
            sql = @"";
            cmd = new NpgsqlCommand(sql, conn);
            //cmd.Parameters.AddWithValue("parameter", parametervalue);

            int result = (int)cmd.ExecuteScalar();
            conn.Close();

            if (result == 1)
            {
                return "Done";
            }
            else
            {
                return("Utilizador ja existe");
            }

        }
        catch (Exception exception)
        {
            conn.Close();
            return "Error"+exception.Message.ToString();
            throw;
        }
        
    }


    /// <summary>
    /// TESTE CONEXAO BD
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string Teste()
    {
            conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PostGreConnectionString"].ConnectionString);
            conn.Open();
            sql = @"select * from reg_insert(:email_doc,:password_doc,:nome_doc,:telemovel_doc)";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("email_doc", "admin1213");
            cmd.Parameters.AddWithValue("password_doc", "admin13111");
            cmd.Parameters.AddWithValue("nome_doc", "admin1121");
            cmd.Parameters.AddWithValue("telemovel_doc", Convert.ToInt32("999921958"));

            int result = (int)cmd.ExecuteScalar();

            conn.Close();

            if (result == 1)
            {
            return "DONE";
            }
            else
            {
                return "Utilizador ja existe";
            }
    }
}
