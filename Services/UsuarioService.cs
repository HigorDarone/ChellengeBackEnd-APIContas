using AutoMapper;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using Microsoft.AspNetCore.Identity;

namespace ChellengeBackEnd_APIContas.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManeger;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManeger, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManeger = userManeger;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task CadastraUsuario (CreateUsuarioDto createUsuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);

        IdentityResult resultadoUsuario = await
              _userManeger.CreateAsync(usuario,
              createUsuarioDto.Senha);

        if (!resultadoUsuario.Succeeded)
        {
            throw new ApplicationException("Falha ao Criar o usuario!");
        }

       
    }

    public async Task<string> LoginUsuario(LoginDto loginDto)
    {
      var resultado = await 
         _signInManager.PasswordSignInAsync(loginDto.UserName,
         loginDto.Senha, false, false);

        if(!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao Fazer Login");
        }

        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName ==
            loginDto.UserName.ToUpper());

        var token = _tokenService.GenerateToken(usuario);

        return token;
    }
      
    
}

