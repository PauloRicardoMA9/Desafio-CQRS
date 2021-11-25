using CQRS.Core.Messages;
using FluentValidation;
using System;

namespace CQRS.Cadastro.Application.Commands
{
    public class ComandoRemoverContato : Comando
    {
        public Guid ContatoId { get; private set; }

        public ComandoRemoverContato(Guid contatoId)
        {
            ContatoId = contatoId;
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
                RuleFor(comando => comando.ContatoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O id do contato não foi informado");
            }
        }
    }
}
