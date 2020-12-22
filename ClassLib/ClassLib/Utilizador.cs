using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca
{
    public class Utilizador
    {
        private int iduser;
        private string username;
        private int password;
        private string email;
        private byte estado;
        private byte estadosessao;
        private int pessoa_idpessoa;

        /// <summary>
        /// Apenas para testes
        /// </summary>
        public Utilizador()
        {
            iduser = -1;
            username = "Ambrosio";
            password = 123;
            email = "abrosio2000@gmail.com";
            estado = (byte)State.Ativo;
            estadosessao = (byte)Sessao.Fechada;
            pessoa_idpessoa = -1;
        }

        public Utilizador(string user, int pw, string email)
        {
            iduser = -1;
            username = user;
            password = pw;
            this.email = email;
            estado = (byte)State.Ativo;
            estadosessao = (byte)Sessao.Fechada;
            pessoa_idpessoa = -1;
        }

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

        public string Username
        {
            get { return username; }
        }
        public int Password
        {
            get { return password; }
        }
        public string Email
        {
            get { return email; }
        }
        public byte Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public byte EstadoSessao
        {
            get { return estadosessao; }
            set { estadosessao = value; }
        }

    }
}
