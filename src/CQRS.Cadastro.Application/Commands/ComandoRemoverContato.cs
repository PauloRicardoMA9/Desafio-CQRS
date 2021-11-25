using CQRS.Core.Messages;
using FluentValidation;
using System;

namespace CQRS.Cadastro.Application.Commands
{
    public class ComandoRemoverContato : Comando
    {
        public Guid ClienteId { get; private set; }

        public ComandoRemoverContato(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool EhValido()
        {
            ResultadoDaValidacao = new ValidacaoRemoverContato().Validate(this);
            return ResultadoDaValidacao.IsValid;
        }

        public class ValidacaoRemoverContato : AbstractValidator<ComandoRemoverContato>
        {
            public ValidacaoRemoverContato()
            {
                RuleFor(comando => comando.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O id do cliente não foi informado");
            }
        }
    }
}
