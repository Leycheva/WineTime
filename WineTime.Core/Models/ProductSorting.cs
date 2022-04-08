namespace WineTime.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum ProductSorting
    {
        [Display(Name= "Manufacture")]
        Manufacture = 0,  
        Price = 1,
        Year = 2
    }
}
