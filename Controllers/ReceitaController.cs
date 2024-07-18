using AutoMapper;
using ChellengeBackEnd_APIContas.Data;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using Microsoft.AspNetCore.Mvc;


namespace ChellengeBackEnd_APIContas.Controllers;



[ApiController]
[Route("[Controller]")]
public class ReceitaController : ControllerBase
{
    private ChallengeContext _context;

    private IMapper _mapper;

    public ReceitaController(ChallengeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public bool VerificarDescricaoNoMesmoMes<T>(T t) where T : IDescricaoDto
    {
          bool receitaVerificao = _context.Receita
         .Where(registro => registro.Descricao.ToLower() == t.Descricao.ToLower())
         .Where(registro => registro.Data.Month == t.Data.Month)
           .Any(registro => registro.Data.Year == t.Data.Year);

        return receitaVerificao;

    }

    [HttpPost]
    public IActionResult AdicionarReceita([FromBody] CreateReceitaDto createReceitaDto)
    {
        bool receitaVericar = VerificarDescricaoNoMesmoMes(createReceitaDto);

        if (receitaVericar) 
        {
             return NotFound("Receita com a mesma descricao ja criada nesse mes");
        }

        Receita receita = _mapper.Map<Receita>(createReceitaDto);

        _context.Receita.Add(receita);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPeloId),
            new { id = receita.Id }, receita);
    }

    [HttpGet]

    public IEnumerable<ReadReceitaDto> RecuperarReceita(
        [FromQuery] string? descricaoReceita = null)

        
    {
        if (descricaoReceita == null)
        {
            return _mapper.Map<List<ReadReceitaDto>>(_context.Receita.ToList());
        }
        else
        { 
           return _mapper.Map<List<ReadReceitaDto>>(_context.Receita.Where(receita => receita.Descricao == descricaoReceita).ToList());
        }
    }

    [HttpGet("{Id}")]
    public IActionResult RecuperarPeloId(int id)
    {
      var receita = _context.Receita?.FirstOrDefault(receita => receita.Id == id);
        if (receita == null) return NotFound();
        return Ok(receita);
    }

    [HttpGet("{Mes}/{Ano}")]
    public IActionResult RecuperarPelaData(int mes, int ano)
    {
        var retornarDadosDto = _mapper.Map<List<ReadReceitaDto>>(_context.Receita.Where(receita => receita.Data.Month == mes
       && receita.Data.Year == ano).ToList());

        if (retornarDadosDto == null)
        {
            return NoContent();
        }
        return Ok(retornarDadosDto);
    }


    [HttpPut("{Id}")]
    public IActionResult AtualizarReceitaPorId(int id, [FromBody] UpdateReceitaDto updateReceitaDto) 
    {
        var receita = _context.Receita.FirstOrDefault(
           receita => receita.Id == id);

        if (receita == null) return NotFound("Id nao existe");

       bool receitaVerificao = VerificarDescricaoNoMesmoMes(updateReceitaDto);

        if (receitaVerificao)
        {
            return NotFound("Descricao Ja Existe Nesse mes");
        }

        _mapper.Map(updateReceitaDto, receita);
        _context.SaveChanges();
        return NoContent(); 
    }
    [HttpDelete("{id}")]
    public IActionResult DeletarReceitaPeloId(int id)
    {
        var receita = _context.Despesa.FirstOrDefault(receita => receita.Id == id);
        if (receita == null) return NotFound();

        _context.Remove(receita);
        _context.SaveChanges();
        return NoContent();

    }

}



