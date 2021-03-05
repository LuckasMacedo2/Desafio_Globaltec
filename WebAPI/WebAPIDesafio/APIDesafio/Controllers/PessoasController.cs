using APIDesafio.Models;
using APIDesafio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDesafio.Controllers
{
    [Authorize]
    public class PessoasController : ApiController
    {
        // Lista de pessoas
        private static List<Pessoa> pessoas = new List<Pessoa>{
            new Pessoa(123, "Lucas", "28078812035", "GO", new DateTime(1998, 07, 25)),
            new Pessoa(456, "Macedo", "17530206036", "RO", new DateTime(1988, 06, 25)),
            new Pessoa(789, "Silva", "73099765090", "CE", new DateTime(1978, 05, 25))
        };

        // Retorna a lista de pessoas
        [Route("Consultar")]
        public List<Pessoa> Get()
        {
            return pessoas;
        }

        // Pessoa pelo código
        [Route("ConsultarCodigo")]
        public Pessoa Get(int codigo)
        {
            foreach(Pessoa pessoa in pessoas)
            {
                if (pessoa.Codigo == codigo)
                {
                    return pessoa;
                }
            }
            throw new HttpResponseException(Request.CreateErrorResponse
                (HttpStatusCode.NotFound, "Código inválido, pessoa não encontrada"));
        }

        // Pessoas por estado
        [Route("ConsultarEstado")]
        public List<Pessoa> Get(string UF)
        {
            if (string.IsNullOrEmpty(UF))
                throw new HttpResponseException(Request.CreateErrorResponse
                    (HttpStatusCode.NotFound, "Estado inválido"));

            List<Pessoa> pessoaLista = new List<Pessoa>();
            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa.UF == UF)
                {
                    pessoaLista.Add(pessoa);
                }
            }

            if (pessoaLista.Count == 0)
                throw new HttpResponseException(Request.CreateErrorResponse
                    (HttpStatusCode.NotFound, "Não foram encontrados registros para esse estado"));

            return pessoaLista;

        }

        // Cria uma nova pessoa
        [Route("SalvarPessoa")]
        public Pessoa Post(int codigo, string nome, string CPF, string UF, string data)
        {
            try
            {
                // Validações
                if (codigo < 0) throw new ValidarInformacoes("Código inválido!");
                if (String.IsNullOrEmpty(nome)) throw new ValidarInformacoes("Nome inválido!");
                if (!ValidarInformacoes.validarCPF(CPF)) throw new ValidarInformacoes("CPF inválido!");
                if (String.IsNullOrEmpty(UF)) throw new ValidarInformacoes("UF inválido!");

                // Formatando a data para o construtor DateTime
                string[] dataVector = data.Split('/');

                DateTime dataNascimento = new DateTime(int.Parse(dataVector[2]),
                                                    int.Parse(dataVector[1]),
                                                    int.Parse(dataVector[0]));

                Pessoa pessoa = new Pessoa(codigo, nome, CPF, UF, dataNascimento);

                pessoas.Add(pessoa);

                return pessoa;
            }
            catch (ValidarInformacoes e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, e.Message));
            }
            catch (System.ArgumentOutOfRangeException)
            {
                throw new HttpResponseException(Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, "Data Invalida"));
            }
        }

        // Atualiza o objeto pessoa
        [Route("AtualizarPessoa")]
        public Pessoa Patch(int codigo, string nome = "", string UF = "")
        {
            Pessoa pessoa = new Pessoa();

            for (int i = 0; i < pessoas.Count; i ++)
            {
                if (pessoas[i].Codigo == codigo)
                {
                    if (!string.IsNullOrEmpty(nome))
                        pessoas[i].Nome = nome;
                    if (!string.IsNullOrEmpty(UF))
                        pessoas[i].UF = UF;

                    return pessoas[i];
                }
            }
            throw new HttpResponseException(Request.CreateErrorResponse
                (HttpStatusCode.NotFound, "Pessoa não encontrada!"));
        }

        // Deleta uma pessoa pelo seu codigo
        [Route("DeletarPessoa")]
        public string Delete(int codigo)
        {
            for (int i = 0; i < pessoas.Count; i++)
            {
                if (pessoas[i].Codigo == codigo)
                {
                    pessoas.RemoveAt(i);
                    return "Pessoa Deletada";
                }
            }
            return "Pessoa Não encontrada";

        }
    }
}
