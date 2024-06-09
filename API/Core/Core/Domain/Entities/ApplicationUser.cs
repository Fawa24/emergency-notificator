using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		[Required]
		public string FullName { get; set; } = null!;
	}
}
