using Task_1.Models;

namespace Task_1.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Salary { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
