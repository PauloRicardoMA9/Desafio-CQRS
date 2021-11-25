using CQRS.Cadastro.Application.Commands;
using CQRS.Cadastro.Data;
using CQRS.Cadastro.Data.Repository;
using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void ResolverDependencias(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<NotificacaoDeDominio>, NotificacaoDeDominioHandler>();

            // Cadastro
            services.AddScoped<IRequestHandler<ComandoAdicionarCliente, bool>, CadastroCommandHandler>();
            services.AddScoped<IRequestHandler<ComandoAdicionarContato, bool>, CadastroCommandHandler>();
            services.AddScoped<IRequestHandler<ComandoRemoverCliente, bool>, CadastroCommandHandler>();
            services.AddScoped<IRequestHandler<ComandoRemoverContato, bool>, CadastroCommandHandler>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<CadastroContext>();
        }
    }
}
