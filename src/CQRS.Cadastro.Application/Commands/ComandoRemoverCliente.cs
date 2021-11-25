using CQRS.Core.Messages;
using FluentValidation;
using System;

namespace CQRS.Cadastro.Application.Commands
{
    public class ComandoRemoverCliente : Comando
    {
        public Guid ClienteId { get; private set; }

        public ComandoRemoverCliente(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool EhValido()
        {
            ResultadoDaValidacao = new ValidacaoRemoverCliente().Validate(this);
            return ResultadoDaValidacao.IsValid;
        }
    }

    public class ValidacaoRemoverCliente : AbstractValidator<ComandoRemoverCliente>
    {
        public ValidacaoRemoverCliente()
        {
            RuleFor(comando => comando.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("O id do cliente não foi informado");
        }
    }
}
