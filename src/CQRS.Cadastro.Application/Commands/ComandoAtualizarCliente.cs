﻿using CQRS.Core.Messages;
using DocumentValidator;
using FluentValidation;
using System;

namespace CQRS.Cadastro.Application.Commands
{
    public class ComandoAtualizarCliente : Comando
    {
        public Guid ClienteId { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Cpf { get; private set; }
        public int Sexo { get; private set; }

        public ComandoAtualizarCliente(Guid clienteId, string nome, string sobrenome, string cpf, int sexo)
        {
            AggregateId = clienteId;
            ClienteId = clienteId;
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            Sexo = sexo;
        }

        public override bool EhValido()
        {
            ResultadoDaValidacao = new ValidacaoAtualizarCliente().Validate(this);
            return ResultadoDaValidacao.IsValid;
        }
    }

    public class ValidacaoAtualizarCliente : AbstractValidator<ComandoAtualizarCliente>
    {
        public ValidacaoAtualizarCliente()
        {
            RuleFor(comando => comando.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("O id do cliente não foi informado");

            RuleFor(comando => comando.Nome)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(comando => comando.Sobrenome)
                .NotEmpty()
                .WithMessage("O sobrenome do cliente não foi informado");

            RuleFor(comando => CpfValidation.Validate(comando.Cpf))
                .Equal(true)
                .WithMessage("CPF inválido");

            RuleFor(comando => comando.Sexo)
                .NotEmpty()
                .WithMessage("Sexo inválido");
        }
    }
}
