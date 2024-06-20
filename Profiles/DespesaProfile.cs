using AutoMapper;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using System.Runtime;

namespace ChellengeBackEnd_APIContas.Profiles;

public class DespesaProfile : Profile
{
    public DespesaProfile() 
    {
        CreateMap<CreateDespesaDto, Despesa>();
        CreateMap<UpdateDespesaDto, Despesa>();
    }
}
