namespace WineTime.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Favorite
    {
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        [Required]
        public int UserId { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
