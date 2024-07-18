using AutoMapper;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;

namespace ChellengeBackEnd_APIContas.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario> ();
        
    }
}
