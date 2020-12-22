using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Pessoa
    {
        int idPessoa;
        string nome;
        string telefone;
        int nif;
        string morada;
        DateTime data;


        public Pessoa()
        {
            idPessoa = -1;
            nome = "Ambrosio";
            telefone = "999999";
            nif = 1234567;
            morada = "Rua Alberto";
            data = DateTime.Parse("20/06/2000");
        }

        public Pessoa(string n, string tele,int nif,string morada,DateTime dt)
        {
            idPessoa = -1;
            nome = n;
            telefone = tele;
            this.nif = nif;
            this.morada = morada;
            data = dt;
        }

        public int IdPessoa
        {
            get
            {
                if (idPessoa != -1)
                    return idPessoa;
                else return -1;
            }
            set { if (idPessoa == -1) idPessoa = value; }
        }

        public string Nome
        {
            get { return nome; }
        }
        public string Telefone
        {
            get { return telefone; }
        }

        public int Nif
        {
            get { return nif; }
        }

        public string Morada
        {
            get { return morada; }
        }
        public DateTime Data
        {
            get { return data; }
        }

    }
}
