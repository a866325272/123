using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.User.Params
{
    public class LoginParam
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
