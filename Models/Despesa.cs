using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.Models;

public class Despesa
{

     
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "A descricao e obrigatorio")]
    [MaxLength(60)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O Valor e obrigatorio")]
    public int Valor { get; set; }

    [Required(ErrorMessage = "A Data e obrigatorio")]
    public DateTime Data  { get; set; }

}
