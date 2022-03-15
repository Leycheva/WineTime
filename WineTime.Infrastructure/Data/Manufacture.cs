namespace WineTime.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Manufacture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ManufactureName { get; set; }

        [Required]
        [StringLength(1000)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }
        [Required]
        public int RegionId { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
