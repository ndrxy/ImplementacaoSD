using AutoMapper;
using projetoRedes.Communication.Requests;
using projetoRedes.Domain.Entities;

namespace projetoRedes.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        //DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUser, Domain.Entities.User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestAddAuthor, Author>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<RequestAddBook, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}
