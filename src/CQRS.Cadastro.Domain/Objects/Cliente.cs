using CQRS.Core.DomainObjects;
using DocumentValidator;

namespace CQRS.Cadastro.Domain.Objects
{
    public class Cliente : Entidade, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Cpf { get; private set; }
        public Sexo Sexo { get; private set; }
        public Contato Contato { get; private set; }

        protected Cliente() { }

        public Cliente(string nome, string sobrenome, string cpf, Sexo sexo, Contato contato)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            Sexo = sexo;
            Contato = contato;

            Validar();
        }

        public void AlterarNome(string nome)
        {
            Validacoes.ValidarSeNaoVazio(nome, "O campo Nome não pode estar vazio.");
            Nome = nome;
        }

        public void AlterarSobrenome(string sobrenome)
        {
            Validacoes.ValidarSeNaoVazio(sobrenome, "O campo Sobrenome não pode estar vazio.");
            Sobrenome = sobrenome;
        }

        public void AlterarCpf(string cpf)
        {
            Validacoes.ValidarSeIgual(true, CpfValidation.Validate(cpf), "CPF inválido");
            Cpf = cpf;
        }

        public void AlterarSexo(Sexo sexo)
        {
            Validacoes.ValidarSeNaoNulo(sexo, "O campo Sexo não pode ser nulo.");
            Sexo = sexo;
        }

        public void AlterarContato(Contato contato)
        {
            Contato = contato;
        }

        public void Validar()
        {
            Validacoes.ValidarSeNaoVazio(Nome, "O campo Nome não pode estar vazio.");
            Validacoes.ValidarSeNaoVazio(Sobrenome, "O campo Sobrenome não pode estar vazio.");
            Validacoes.ValidarSeIgual(true, CpfValidation.Validate(Cpf), "CPF inválido");
            Validacoes.ValidarSeNaoNulo(Sexo, "O campo Sexo não pode ser nulo.");
        }
    }
}
