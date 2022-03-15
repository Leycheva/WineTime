namespace WineTime.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Degustation
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int AvailableSeats { get; set; }



    }
}
