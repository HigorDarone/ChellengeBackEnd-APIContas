 using AutoMapper;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using ChellengeBackEnd_APIContas.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ChellengeBackEnd_APIContas.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
  
    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createUsuarioDto)
    {
       await _usuarioService.CadastraUsuario(createUsuarioDto);
        return Ok("Usuario cadastrado com sucesso!");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login (LoginDto loginDto)
    {
       var token = await _usuarioService.LoginUsuario( loginDto);
        return Ok(token);
    }
}
