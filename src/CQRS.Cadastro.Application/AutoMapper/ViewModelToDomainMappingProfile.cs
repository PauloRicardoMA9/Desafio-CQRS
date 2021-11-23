using AutoMapper;
using CQRS.Cadastro.Application.ViewModels;
using CQRS.Cadastro.Domain.Objects;

namespace CQRS.Cadastro.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<ContatoViewModel, Contato>();
        }
    }
}
