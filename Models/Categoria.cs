using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descricao de categoria e obrigatoria!")]
        public string Categorizacao { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
    }
}
