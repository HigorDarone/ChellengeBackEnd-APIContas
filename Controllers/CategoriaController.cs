using AutoMapper;
using ChellengeBackEnd_APIContas.Data;
using ChellengeBackEnd_APIContas.DTO;
using ChellengeBackEnd_APIContas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChellengeBackEnd_APIContas.Controllers;

[ApiController]
//Pegando o prefixo antes do controller para ser a rota
[Route("[Controller]")]
public class CategoriaController : ControllerBase
{
    private ChallengeContext _context;

    private IMapper _mapper;

    public CategoriaController(ChallengeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //Post para adicionar Categoria, onde se faz o mapeamento que uma Categoria vai ser uma categoriaDto que recebe do post,
    //usando o a injecao do context para adicionar e em seguida salvar
    [HttpPost]
    //pegando o resultado enviado com [frombody]
    public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);

        _context.Categoria.Add(categoria);
        _context.SaveChanges();

        //acao para padronizacao do retorno id, onde e passado a funcao depois os parametros necessarios para ela. 
        return CreatedAtAction(nameof(RecuperarPeloId), new { id = categoria.Id }, categoria);

    }

    //Usando Inumerable como o retorno e uma lista para  
    [HttpGet]

    public IEnumerable<ReadCategoriaDto> RecuperarCategoria()
    {
        //mapeando uma lista do Dto, onde ele vai receber os dados te todos os registros pelo context
        return _mapper.Map<List<ReadCategoriaDto>>(_context.Categoria.ToList());
    }

    //get vai buscar pelo id passado
    [HttpGet("{Id}")]
    public IActionResult RecuperarPeloId(int id)
    {
        //pegando o primeiro por padrao onde categoria.id seja igual ao paremetro ID passado. 
        //Apos verificando se nao e null se for retornando um erro notFound
        //Caso nao for nulo Intanciamos a ReadCategoria que vai receber o mapeamento da query feita a cima 
        var categoria = _context.Categoria.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria != null)
        {
            ReadCategoriaDto categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
            return Ok(categoria);
        }
        return NotFound();
       
    }

    //put vai buscar pelo id passado
    [HttpPut("{Id}")]

    //pegando o ID e o resultado atualizado no envio da Api em PUT
    public IActionResult AtualizarCategoriaPorId(int id, [FromBody] UpdateCategoriaDto updateCategoriaDto)
    {
        //selecionando o Registro com o Id certo
        var categoria = _context.Categoria.FirstOrDefault(
           categoria => categoria.Id == id);

        //verificando se nao e nulo
        if (categoria == null) return NotFound("Id nao existe");

        //fazendo o mapeamento para  a categoria e salvando
        _mapper.Map(updateCategoriaDto, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    ////delete vai buscar pelo id passado
    [HttpDelete("{id}")]
    public IActionResult DeletarCategoriaPeloId(int id)
    {
        //pegando o ID e o resultado atualizado no envio da Api em PUT
        var categoria = _context.Categoria.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        //Usando o .Romeve para remover 
        _context.Remove(categoria);
        _context.SaveChanges();
        return NoContent();

    }
}
