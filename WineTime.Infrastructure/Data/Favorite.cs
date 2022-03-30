namespace WineTime.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Favorite
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
