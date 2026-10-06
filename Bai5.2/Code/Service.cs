namespace BaiTap52
{
    public class Service
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Service(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // Phương thức hiển thị Tên - Giá tiền trong ListBox
        public override string ToString()
        {
            return $"{Name} - {Price:N0} VNĐ";
        }
    }
}
