using Task_1.Models;
using System;

namespace Task_1.ViewModels
{
    public class OrderViewModel 
    {
        public int OrderId { get; set; }
        public DateTime? SiparisTarihi { get; set; }
        public string MusteriAdi { get; set; }
        public string CalisanAdi { get; set; }

    }
}

