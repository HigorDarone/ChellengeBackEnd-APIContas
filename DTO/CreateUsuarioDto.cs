using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.DTO;

public class CreateUsuarioDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Required]
    [Compare("Senha")]
    public string ConfimarSenha { get; set; }
        
}
