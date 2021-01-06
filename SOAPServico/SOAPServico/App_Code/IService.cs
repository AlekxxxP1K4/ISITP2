using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    /// <summary>
    /// Regista um utilizador da clinica
    /// </summary>
    /// <param name="u">Ficha Utilizador</param>
    /// <param name="p">Ficha Pessoa</param>
    /// <param name="role">Role que desempenha</param>
    /// <returns></returns>
    [OperationContract]
    string RegistrarUser(Utilizador u, Pessoa p, int role);

    /// <summary>
    /// Verifica se existe um utilizador com o mesmo user
    /// </summary>
    /// <param name="user">utilizador</param>
    /// <returns></returns>
    [OperationContract]
    int VerificarUserinTable(string user);

    /// <summary>
    /// Vai a base de dados e tras o nome do id pertencente a pessoa
    /// </summary>
    /// <param name="id">id pessoa</param>
    /// <returns></returns>
    [OperationContract]
    string nameLogedin(int id);

    /// <summary>
    /// Data table com as consultas todas de um utente
    /// </summary>
    /// <param name="id">utente</param>
    /// <returns></returns>
    [OperationContract]
    DataTable ConsultasUtente(int id);

    /// <summary>
    /// Devolve tabela com as convecoes disponiveis
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    DataTable ConvencaoInfo();

    /// <summary>
    /// Devolve tabela dos profissionais de saude
    /// </summary>
    /// <returns>Data table colunas Nome profissional idprofissional</returns>
    [OperationContract]
    DataTable Medicos();

    /// <summary>
    /// Devolve tabela dos tipos de consulta
    /// </summary>
    /// <returns>Data table Colunas tipo e idtipo</returns>
    [OperationContract]
    DataTable TipoConsulta();


}