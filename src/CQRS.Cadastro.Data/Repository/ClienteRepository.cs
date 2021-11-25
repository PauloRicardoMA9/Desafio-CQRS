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

        public async Task<IEnumerable<Cliente>> BuscarCliente(Expression<Func<Cliente, bool>> predicate)
        {
            return await _context.Clientes.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Contato>> BuscarContato(Expression<Func<Contato, bool>> predicate)
        {
            return await _context.Contatos.AsNoTracking().Where(predicate).ToListAsync();
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}