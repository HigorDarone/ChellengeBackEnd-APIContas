using System.ComponentModel.DataAnnotations;

namespace ChellengeBackEnd_APIContas.DTO
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Senha { get; set; }

    }
}
