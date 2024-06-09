using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
	public class RegisterDTO
	{
		[Required(ErrorMessage = "User name can not be blank")]
		public string UserName { get; set; } = null!;

		[Required(ErrorMessage = "User name can not be blank")]
		public string FullName { get; set; } = null!;

		[Required(ErrorMessage = "Email can not be blank")]
		[EmailAddress(ErrorMessage = "Email have to be in the right format")]
		public string Email { get; set; } = null!;

		[Required(ErrorMessage = "Password can not be blank")]
		public string Password { get; set; } = null!;

		[Required(ErrorMessage = "Phone number can not be blank")]
		[Phone(ErrorMessage = "Phone have to be in the right format")]
		public string PhoneNumber { get; set; } = null!;
	}
}
