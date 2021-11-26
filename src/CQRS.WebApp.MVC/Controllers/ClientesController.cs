using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Cadastro.Application.Commands;
using CQRS.Cadastro.Application.Queries;
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
        private readonly ICadastroQueries _cadastroQueries;
        private readonly IMapper _mapper;

        public ClientesController(IMediatorHandler mediatorHandler,
                                  INotificationHandler<NotificacaoDeDominio> notificacoes,
                                  ICadastroQueries cadastroQueries,
                                  IMapper mapper) : base(notificacoes, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _cadastroQueries = cadastroQueries;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("index")]
        public async Task<ActionResult<IList<ClienteViewModel>>> IndexClientes()
        {
            var clientes = await _cadastroQueries.ObterClientes();
            var clientesViewModel = _mapper.Map<IList<ClienteViewModel>>(clientes);

            if (OperacaoValida())
            {
                return Ok(clientesViewModel);
            }

            return NotFound(ObterMensagensErro());
        }

        [HttpGet]
        [Route("contatos/index")]
        public async Task<ActionResult<IList<ContatoViewModel>>> IndexContatos()
        {
            var contatos = await _cadastroQueries.ObterContatos();
            var contatosViewModel = _mapper.Map<IList<ContatoViewModel>>(contatos);

            if (OperacaoValida())
            {
                return Ok(contatosViewModel);
            }

            return NotFound(ObterMensagensErro());
        }

        [HttpGet("{clienteId:guid}")]
        [Route("busca/{clienteId:guid}")]
        public async Task<ActionResult<ClienteViewModel>> ObterClienteComContatoPorId(Guid clienteId)
        {
            var cliente = await _cadastroQueries.ObterClienteComContatoPorId(clienteId);
            var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

            if (OperacaoValida())
            {
                return Ok(clienteViewModel);
            }

            return NotFound(ObterMensagensErro());
        }

        [HttpPost] 
        [Route("adicionar")]
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

            var comandoAddCliente = new ComandoAdicionarCliente(cliente.Id, cliente.Nome, cliente.Sobrenome, cliente.Cpf, cliente.Sexo);
            await _mediatorHandler.EnviarComando(comandoAddCliente);

            if (cliente.Contato != null)
            {
                await AdicionarContato(cliente.Contato, cliente.Id);
            }

            if (OperacaoValida())
            {
                return CreatedAtAction("AdicionarCliente", null);
            }

            return BadRequest();
        }

        [HttpPost("{clienteId:guid}")]
        [Route("contato/adicionar/{clienteId:guid}")]
        public async Task<IActionResult> AdicionarContato(ContatoViewModel contato, Guid clienteId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (contato.Id == Guid.Empty)
            {
                contato.Id = Guid.NewGuid();
            }

            var comandoAddContato = new ComandoAdicionarContato(contato.Id, clienteId, contato.Ddd, contato.Telefone, contato.Email);
            await _mediatorHandler.EnviarComando(comandoAddContato);

            if (OperacaoValida())
            {
                return CreatedAtAction("AdicionarContato", null);
            }

            return BadRequest(ObterMensagensErro());
        }

        [HttpDelete("{clienteId:guid}")]
        [Route("remover/{clienteId:guid}")]
        public async Task<IActionResult> RemoverCliente(Guid clienteId)
        {
            var comandoRemCliente = new ComandoRemoverCliente(clienteId);
            await _mediatorHandler.EnviarComando(comandoRemCliente);

            if (OperacaoValida())
            {
                return NoContent();
            }

            return BadRequest(ObterMensagensErro());
        }

        [HttpDelete("{clienteId:guid}")]
        [Route("contato/remover/{clienteId:guid}")]
        public async Task<IActionResult> RemoverContatoPorClienteId(Guid clienteId)
        {
            var comandoRemContato = new ComandoRemoverContato(clienteId);
            await _mediatorHandler.EnviarComando(comandoRemContato);

            if (OperacaoValida())
            {
                return NoContent();
            }

            return BadRequest(ObterMensagensErro());
        }

        [HttpPut("{clienteId:guid}")]
        [Route("atualizar/{clienteId:guid}")]
        public async Task<IActionResult> AtualizarCliente(ClienteViewModel clienteViewModel, Guid clienteId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (clienteViewModel.Contato != null)
            {
                await AtualizarContatoPorClienteId(clienteViewModel.Contato, clienteId);
            }

            if (!OperacaoValida())
            {
                return BadRequest(ObterMensagensErro());
            }

            var comandoAttCliente = new ComandoAtualizarCliente(clienteId, clienteViewModel.Nome, clienteViewModel.Sobrenome, clienteViewModel.Cpf, clienteViewModel.Sexo);
            await _mediatorHandler.EnviarComando(comandoAttCliente);

            if (OperacaoValida())
            {
                return NoContent();
            }

            return BadRequest(ObterMensagensErro());
        }

        [HttpPut("{clienteId:guid}")]
        [Route("contato/atualizar/{clienteId:guid}")]
        public async Task<IActionResult> AtualizarContatoPorClienteId(ContatoViewModel contatoViewModel, Guid clienteId)
        {
            var comandoAttContato = new ComandoAtualizarContato(clienteId, contatoViewModel.Ddd, contatoViewModel.Telefone, contatoViewModel.Email);
            await _mediatorHandler.EnviarComando(comandoAttContato);

            if (OperacaoValida())
            {
                return NoContent();
            }

            return BadRequest(ObterMensagensErro());
        }
    }
}