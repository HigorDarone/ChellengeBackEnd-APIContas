using AutoMapper;
using ChellengeBackEnd_APIContas.Data;
using ChellengeBackEnd_APIContas.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChellengeBackEnd_APIContas.Controllers;

[ApiController]
[Route("[Controller]")]

public class ResumoController : ControllerBase
{
    private ChallengeContext _contex;

    private IMapper _mapper;


    public ResumoController(ChallengeContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }


    [HttpGet("{mes}/{ano}")]
    public async Task<IActionResult> RecuperarValorDespesa(int mes, int ano)
    {
        var valorDespesa = await _contex.Despesa.Where(
            despesa => despesa.Data.Month == mes && despesa.Data.Year == ano)
            .SumAsync(despesa => despesa.Valor);



        var totaldeCategoria = await _contex.Despesa.Where(
            despesa => despesa.Data.Month == mes && despesa.Data.Year == ano)
            .GroupBy(despesa => despesa.Categoria.Categorizacao)
            .Select(grupo => new
            {
                Categoria = grupo.Key,
                ValorTotal = grupo.Sum(despesa => despesa.Valor)
            }
            ).ToListAsync();

        var valorReceita = await _contex.Receita.Where(
           Receita => Receita.Data.Month == mes && Receita.Data.Year == ano)
          .SumAsync(Receita => Receita.Valor);


        var resumo = new
        {
            Mes = mes,
            Ano = ano,



            ValorTotalDespesa  = valorDespesa,

            ValorTotalReceita = valorReceita,

            valorPorCategoria = totaldeCategoria

        };

        return Ok(resumo);
    }

    
}