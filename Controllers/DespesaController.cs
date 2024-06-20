using AutoMapper;
using ChellengeBackEnd_APIContas.Data;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChellengeBackEnd_APIContas.Controllers;

[ApiController]
[Route("[Controller]")]

public class DespesaController:ControllerBase
{
    private ChallengeContext _context;

    private IMapper _mapper;

    public DespesaController(ChallengeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarDespesa([FromBody] CreateDespesaDto despesaDto)
    {
        Despesa despesa = _mapper.Map <Despesa>(despesaDto);

        foreach (var despesaItem in _context.Despesa)
        {
            if (despesa.Descricao.Equals(despesaItem.Descricao))
            {
                return NotFound("Descricao Ja Existe");
            }

        }

        _context.Despesa.Add(despesa);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPeloId), new { id = despesa.Id }, despesa);
    
    }

    [HttpGet]

    public IEnumerable<Despesa> RecuperarDespesa()
    {
        return _context.Despesa;
    }

    [HttpGet("{Id}")]
    public IActionResult RecuperarPeloId(int id)
    {
        var despesa = _context.Despesa.FirstOrDefault(despesa => despesa.Id == id);
        if (despesa == null) return NotFound();
        return Ok(despesa);
    }

    [HttpPut("{Id}")]
    public IActionResult AtualizarDespesaPeloId (int id, [FromBody] UpdateDespesaDto updateDespesaDto)
    {
        var despesa = _context.Despesa.FirstOrDefault(
            despesa => despesa.Id == id);
        if(despesa == null) return NotFound();
        _mapper.Map(updateDespesaDto, despesa);
        _context.SaveChanges();
        return NoContent();
    }
}
