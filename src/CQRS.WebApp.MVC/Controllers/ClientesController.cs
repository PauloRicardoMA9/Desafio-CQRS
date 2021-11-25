using System;
using System.Threading.Tasks;
using CQRS.Cadastro.Application.Commands;
using CQRS.Cadastro.Application.ViewModels;
using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.WebApp.MVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler,
                                  INotificationHandler<NotificacaoDeDominio> notificacoes) : base(notificacoes, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost] 
        [Route("clientes-adicionar")]
        public async Task<IActionResult> AdicionarCliente(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(cliente.Id == Guid.Empty)
            {
                cliente.Id = Guid.NewGuid();
            }

            var contato = cliente.Contato;

            var comandoAddCliente = new ComandoAdicionarCliente(cliente.Id, cliente.Nome, cliente.Sobrenome, cliente.Cpf, cliente.Sexo);
            await _mediatorHandler.EnviarComando(comandoAddCliente);

            if (contato != null)
            {
                var comandoAddContato = new ComandoAdicionarContato(cliente.Id, contato.Ddd, contato.Telefone, contato.Email);
                await _mediatorHandler.EnviarComando(comandoAddContato);
            }

            if (OperacaoValida())
            {
                return CreatedAtAction("AdicionarCliente", null);
            }

            return BadRequest();
        }
    }
}