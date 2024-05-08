namespace Product_Management_System.Models.Admin
{
    public class Order
    {
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total_Price { get; set; }
    }
}
