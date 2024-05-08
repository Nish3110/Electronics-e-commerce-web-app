using System.ComponentModel.DataAnnotations;

namespace Product_Management_System.Models.Admin
{
    public class Product
    {
        public int Product_Id { get; set; }
        [Required]
        public string Product_Name { get; set; }
        [Required]
        public string Product_Description { get; set; }
        [Required]
        public decimal Product_Price { get; set; }
        [Required]
        public IFormFile? Cover_Image { get; set; }
        public string? Product_Cover_Image { get; set; }
        [Required]
        public List<IFormFile>? Images { get; set; }
        public string? Product_Images { get; set; }
        [Required]
        public int? Product_Quantity { get; set; }
        public int? Product_Status { get; set; }
        public int? Is_Deleted { get; set; }
    }
}
