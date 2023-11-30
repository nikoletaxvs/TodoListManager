using System.ComponentModel.DataAnnotations;

namespace TodoListManager.Dto
{
    public class UserLogoutRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
