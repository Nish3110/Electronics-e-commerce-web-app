namespace Product_Management_System.Models
{
    public class Cart
    {
        public int Cart_Id { get; set; }
        public int User_Id { get; set; }  
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string? Product_Cover_Image { get; set; }
        public int? Is_Deleted { get; set; }
    }
}
