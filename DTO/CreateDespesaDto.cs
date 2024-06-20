using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.DTO;

public class CreateDespesaDto : IDescricaoDto
{
    [Required(ErrorMessage = "A descricao e obrigatorio")]
    [StringLength(60)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O Valor e obrigatorio")]
    public int Valor { get; set; }

    [Required(ErrorMessage = "A Data e obrigatorio")]
    public DateTime Data { get; set; }

}
