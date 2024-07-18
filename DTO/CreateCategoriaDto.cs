using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.DTO
{
    public class CreateCategoriaDto
    {
        [Required(ErrorMessage = "A descricao de categoria e obrigatoria!")]
        public string Categorizacao { get; set; }


    }
}
