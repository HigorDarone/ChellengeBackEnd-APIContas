using AutoMapper;
using Castle.Core.Internal;
using ChellengeBackEnd_APIContas.Data;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChellengeBackEnd_APIContas.Controllers;

[ApiController]
[Route("[Controller]")]

public class DespesaController : ControllerBase
{
    private ChallengeContext _context;

    private IMapper _mapper;

    public DespesaController(ChallengeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public bool VerificarDescricaoNoMesmoMes<T>(T t) where T : IDescricaoDto
    {
        bool receitaVerificao = _context.Despesa
         .Where(registro => registro.Descricao.ToLower() == t.Descricao.ToLower())
         .Where(registro => registro.Data.Month == t.Data.Month)
           .Any(registro => registro.Data.Year == t.Data.Year);

        return receitaVerificao;

    }

    [HttpPost]
    public IActionResult AdicionarDespesa([FromBody] CreateDespesaDto despesaDto)
    {
        Despesa despesa = _mapper.Map<Despesa>(despesaDto);

        bool despesaverificar = VerificarDescricaoNoMesmoMes(despesaDto);

        var contagemCategoria = _context.Categoria.Count();


        if (despesaverificar)
        {
            return NotFound("Despesa com a mesma descricao ja criada nesse mes");
        }
        if (despesaDto.CategoriaId.HasValue)
        {
            despesaDto.CategoriaId = despesa.CategoriaId.Value;
        }
        else if (despesaDto.CategoriaId > contagemCategoria || despesaDto.CategoriaId <= 0)
        {
            return NotFound("O Id dessa categoria nao existe ");
        }  
        else
        {
            despesa.CategoriaId = 8;
        }

        _context.Despesa.Add(despesa);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPeloId), new { id = despesa.Id }, despesa);

    }

    [HttpGet]

    public IEnumerable<ReadDespesaDto> RecuperarDespesa([FromQuery] string? despesaDescricao = null)
    {
        if (despesaDescricao == null)
        {
            return _mapper.Map<List<ReadDespesaDto>>(_context.Despesa.ToList());
        }
        else
        {
            return _mapper.Map<List<ReadDespesaDto>>(_context.Despesa.Where(despesa => despesa.Descricao == despesaDescricao).ToList());
        }
    }

    [HttpGet("{Id}")]
    public IActionResult RecuperarPeloId(int id)
    {
        var despesa = _context.Despesa.FirstOrDefault(despesa => despesa.Id == id);
        if (despesa == null) return NotFound();
        return Ok(despesa);
    }

    [HttpGet("{Mes}/{Ano}")]
    public IActionResult RecuperarPelaData( int mes, int ano)
    {
        var retornarDadosDto = _mapper.Map<List<ReadDespesaDto>>(_context.Despesa.Where(despesa => despesa.Data.Month == mes
       && despesa.Data.Year == ano).ToList());

        if (retornarDadosDto == null)
        {
            return NoContent();
        }
        return Ok(retornarDadosDto);
    }


    [HttpPut("{Id}")]
    public IActionResult AtualizarDespesaPeloId (int id, [FromBody] UpdateDespesaDto updateDespesaDto)
    {
        var despesa = _context.Despesa.FirstOrDefault(
            despesa => despesa.Id == id);

        if(despesa == null) return NotFound();

        bool despesaverificar = VerificarDescricaoNoMesmoMes(updateDespesaDto);

        if (despesaverificar)
        {
            return NotFound("Despesa com a mesma descricao ja criada nesse mes");
        }

        _mapper.Map(updateDespesaDto, despesa);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarDespesaPeloId (int id) 
    {
        var despesa = _context.Despesa.FirstOrDefault(despesa => despesa.Id == id);
        if (despesa == null) return NotFound();

        _context.Remove(despesa);
        _context.SaveChanges();
        return NoContent();

    }
}
