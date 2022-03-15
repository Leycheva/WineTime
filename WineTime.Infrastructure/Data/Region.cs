namespace WineTime.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Region
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

    }
}
