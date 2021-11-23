using AutoMapper;
using CQRS.Cadastro.Application.ViewModels;
using CQRS.Cadastro.Domain.Objects;

namespace CQRS.Cadastro.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<Contato, ContatoViewModel>();
        }
    }
}
