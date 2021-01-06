using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for Utilizador
/// </summary>
[DataContract]
public class Utilizador
{
    private int iduser;
    private string username;
    private string password;
    private string email;
    private string estado;
    private byte estadosessao;
    private int pessoa_idpessoa;



    [DataMember]
    public int Iduser
    {
        get
        {
            if (iduser != -1)
                return iduser;
            else return -1;
        }
        set { if (iduser == -1) iduser = value; }
    }

    [DataMember]
    public int Pessoa_idpessoa
    {
        get
        {
            if (pessoa_idpessoa != -1)
                return pessoa_idpessoa;
            else return -1;
        }
        set { if (pessoa_idpessoa == -1) pessoa_idpessoa = value; }
    }

    [DataMember]
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    [DataMember]
    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    [DataMember]
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    [DataMember]
    public string Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    [DataMember]
    public byte EstadoSessao
    {
        get { return estadosessao; }
        set { estadosessao = value; }
    }

}