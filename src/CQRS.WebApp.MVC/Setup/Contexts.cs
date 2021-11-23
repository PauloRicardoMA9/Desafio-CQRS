using CQRS.Cadastro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.WebApp.MVC.Setup
{
    public static class Contexts
    {
        public static void AddContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CadastroContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
