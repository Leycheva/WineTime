using System.ComponentModel.DataAnnotations;

namespace WineTime.Areas.Admin.Models
{
    public enum ProductSorting
    {
        [Display(Name= "Manufacture")]
        Manufacture = 0,  
        Price = 1,
        Year = 2
    }
}
