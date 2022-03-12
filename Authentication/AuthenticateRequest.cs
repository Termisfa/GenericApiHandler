using System.ComponentModel.DataAnnotations;

namespace GenericApiHandler.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Schema { get; set; }
    }
}
