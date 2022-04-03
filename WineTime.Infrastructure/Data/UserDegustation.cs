using System.ComponentModel.DataAnnotations.Schema;

namespace WineTime.Infrastructure.Data
{
    public class UserDegustation
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public int DegustationId { get; set; }

        [ForeignKey(nameof(DegustationId))]
        public Degustation Degustation { get; set; }

    }
}
