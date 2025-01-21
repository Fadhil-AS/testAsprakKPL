namespace APITokoOnline.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer_name { get; set; }
        public string Product_name { get; set; }
        public int Quantity { get; set; }
        public double Total_price { get; set; }
        public DateTime Order_date { get; set; }
        public string Shipping_address { get; set; }
        public string Status { get; set; }
    }
}
