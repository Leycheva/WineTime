namespace WineTime.Infrastructure.Data
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string ImageUrl { get; set; }


        [Required]
        [StringLength(4)]
        public string YearOfManufacture { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }


        [Required]
        public Sort Sort { get; set; }


        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }
        [Required]
        public int RegionId { get; set; }


        [StringLength(200)]
        public string? Description { get; set; }


        [ForeignKey(nameof(ManufactureId))]
        public Manufacture Manufacture { get; set; }
        [Required]
        public int ManufactureId { get; set; }

    }
}

