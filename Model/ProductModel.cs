using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaginMA_2025_11_27.Model
{
    internal class ProductModel
    {
        [Key]
        public string Article { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Cost { get; set; }

        public int MaxDiscount { get; set; }

        public string Manufacturer { get; set; }

        public string Supplier { get; set; }

        public string Category { get; set; }

        public int CurrentDiscount { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public virtual ICollection<OrderProductModel> OrderItems { get; set; }
    }
}
