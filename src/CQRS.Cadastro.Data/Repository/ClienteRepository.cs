using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CadastroContext _context;

        public ClienteRepository(CadastroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void Adicionar(Contato contato)
        {
            _context.Contatos.Add(contato);
        }

        public async Task<IList<Cliente>> BuscarCliente(Expression<Func<Cliente, bool>> predicate)
        {
            return await _context.Clientes.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IList<Contato>> BuscarContato(Expression<Func<Contato, bool>> predicate)
        {
            return await _context.Contatos.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IList<Cliente>> ObterTodosClientes()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<IList<Contato>> ObterTodosContatos()
        {
            return await _context.Contatos.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> ObterClientePorId(Guid id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Contato> ObterContatoPorId(Guid id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public Contato ObterContatoPorClienteId(Expression<Func<Contato, bool>> predicate)
        {
            return _context.Contatos.AsNoTracking().Where(predicate).FirstOrDefault();
        }

        public void RemoverCliente(Guid id)
        {
            var cliente = new Cliente { Id = id };
            _context.Clientes.Remove(cliente);
        }

        public void RemoverContato(Guid id)
        {
            var contato = new Contato { Id = id };
            _context.Contatos.Remove(contato);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }

        public void AtualizarContato(Contato contato)
        {
            _context.Contatos.Update(contato);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}