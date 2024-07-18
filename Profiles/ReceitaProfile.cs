using AutoMapper;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;

namespace ChellengeBackEnd_APIContas.Profiles;

    public class ReceitaProfile : Profile
{
    public ReceitaProfile() 
    {
        CreateMap<CreateReceitaDto, Receita>();
        CreateMap<UpdateReceitaDto, Receita>();
        CreateMap<Receita, ReadReceitaDto>();
    }

}

