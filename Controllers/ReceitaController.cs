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

    public bool VerificarDescricaoNoMesmoMes<TDto>(TDto dto) where TDto : IDescricaoDto
    {
        bool receitaVerificao = _context.Receita
         .Where(registro => registro.Descricao.ToLower() == dto.Descricao.ToLower())
         .Where(registro => registro.Data.Month == dto.Data.Month)
           .Any(registro => registro.Data.Year == dto.Data.Year);

        return receitaVerificao;

    }

    [HttpPost]
    public IActionResult AdicionarReceita([FromBody] CreateReceitaDto createReceitaDto)
    {
        bool receitaVericar = VerificarDescricaoNoMesmoMes(createReceitaDto);

        if (receitaVericar) 
        {
             return NotFound("Essa Descricao ja existe nesse Mes");
        }

        Receita receita = _mapper.Map<Receita>(createReceitaDto);

        _context.Receita.Add(receita);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPeloId),
            new { id = receita.Id }, receita);
    }

    [HttpGet]

    public IEnumerable<Receita> RecuperarReceita()
    {
        return _context.Receita;
    }

    [HttpGet("{Id}")]
    public IActionResult RecuperarPeloId(int id)
    {
      var receita = _context.Receita?.FirstOrDefault(receita => receita.Id == id);
        if (receita == null) return NotFound();
        return Ok(receita);
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

   
}



