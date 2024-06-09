using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
	public class LoginDTO
	{
		[Required(ErrorMessage = "User name can not be blank")]
		public string UserName { get; set; } = null!;

		[Required(ErrorMessage = "Password can not be blank")]
		public string Password { get; set; } = null!;
	}
}
