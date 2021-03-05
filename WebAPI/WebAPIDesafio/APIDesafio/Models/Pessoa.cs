using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDesafio.Models
{
    public class Pessoa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa()
        {
            this.Codigo = 0;
            this.Nome = "";
            this.CPF = "00000000000";
            this.UF = "";
            this.DataNascimento = new DateTime(2001, 01, 01);
        }

        public Pessoa(int Codigo, string Nome, string CPF, string UF, DateTime DataNascimento)
        {
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.CPF = CPF;
            this.UF = UF;
            this.DataNascimento = DataNascimento;
        }
    }
}