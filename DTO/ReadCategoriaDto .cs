using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.DTO
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Categorizacao {get; set;}
    }
}