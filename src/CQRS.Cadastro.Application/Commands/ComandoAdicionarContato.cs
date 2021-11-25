using CQRS.Core.Messages;
using FluentValidation;
using System;

namespace CQRS.Cadastro.Application.Commands
{
    public class ComandoAdicionarContato : Comando
    {
        public Guid ClienteId { get; private set; }
        public int Ddd { get; private set; }
        public int Telefone { get; private set; }
        public string Email { get; private set; }

        public ComandoAdicionarContato(Guid clienteId, int ddd, int telefone, string email)
        {
            ClienteId = clienteId;
            Ddd = ddd;
            Telefone = telefone;
            Email = email;
        }

        public override bool EhValido()
        {
            ResultadoDaValidacao = new ValidacaoAdicionarContato().Validate(this);
            return ResultadoDaValidacao.IsValid;
        }
    }

    public class ValidacaoAdicionarContato : AbstractValidator<ComandoAdicionarContato>
    {
        public ValidacaoAdicionarContato()
        {
            RuleFor(comando => comando.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("O id do cliente não foi informado");

            RuleFor(comando => comando.Ddd)
                .NotNull()
                .WithMessage("O DDD não foi informado");

            RuleFor(comando => comando.Telefone)
                .NotNull()
                .WithMessage("O telefone não foi informado");

            RuleFor(comando => comando.Email)
                .NotEmpty()
                .WithMessage("O email não foi informado")
                .EmailAddress()
                .WithMessage("Email inválido");
        }
    }
}
