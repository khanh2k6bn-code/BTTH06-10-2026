namespace BaiTap53
{
    public class Product
    {
        public string ProductId { get; set; }     // Mã SP
        public string ProductName { get; set; }   // Tên SP
        public decimal UnitPrice { get; set; }    // Đơn giá
        public int Quantity { get; set; }         // Số lượng
        public string Category { get; set; }       // Danh mục

        public Product() { }

        public Product(string productId, string productName, decimal unitPrice, int quantity, string category)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Category = category;
        }
    }
}
