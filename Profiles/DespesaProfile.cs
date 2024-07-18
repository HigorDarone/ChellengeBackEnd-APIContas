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
        CreateMap<Despesa, ReadDespesaDto>().ForMember(despesaDto => despesaDto.ReadCategoriaDto, 
            opt => opt.MapFrom(categoria => categoria.Categoria));
    }
}
