using AutoMapper;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;

namespace ChellengeBackEnd_APIContas.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile() 
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
        }
    }
}
