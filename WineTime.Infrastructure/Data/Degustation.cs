namespace WineTime.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Degustation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int Seats { get; set; }

        public ICollection<UserDegustation> Users { get; set; } = new HashSet<UserDegustation>();

    }
}
