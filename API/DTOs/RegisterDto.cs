using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {   
        [Required] public string Username { get; set; }
                
        [StringLength(12, MinimumLength = 6)]
        [Required] public string Password { get; set; }
        [Required] public string Firstname { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public int Congregation { get; set; }
    }
}