using CQRS.Core.DomainObjects;

namespace CQRS.Cadastro.Domain.Objects
{
    public class Contato : Entidade
    {
        public int Ddd { get; private set; }
        public int Numero { get; private set; }
        public string Email { get; private set; }
        public Cliente Cliente { get; private set; }

        public Contato(int ddd, int numero, string email)
        {
            Ddd = ddd;
            Numero = numero;
            Email = email;

            Validar();
        }

        public void AlterarDdd(int ddd)
        {
            Validacoes.ValidarTamanho(ddd, 3, 3, "O campo DDD deve conter 3 dígitos.");
            Ddd = ddd;
        }

        public void AlterarNumero(int numero)
        {
            Validacoes.ValidarTamanho(numero, 9, 9, "O campo Número deve conter 9 dígitos.");
            Numero = numero;
        }

        public void AlterarEmail(string email)
        {
            Validacoes.ValidarSeNaoVazio(email, "O campo Email não pode estar vazio.");
            Email = email;
        }

        public void Validar()
        {
            Validacoes.ValidarTamanho(Ddd, 3, 3, "O campo DDD deve conter 3 dígitos.");
            Validacoes.ValidarTamanho(Numero, 9, 9, "O campo Número deve conter 9 dígitos.");
            Validacoes.ValidarSeNaoVazio(Email, "O campo Email não pode estar vazio.");
        }
    }
}
