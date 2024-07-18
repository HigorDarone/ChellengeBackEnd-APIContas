using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.DTO
{
    public class ReadReceitaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
