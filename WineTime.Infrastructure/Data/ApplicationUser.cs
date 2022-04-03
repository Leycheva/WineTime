namespace WineTime.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FullName { get; set; }

    }
}
