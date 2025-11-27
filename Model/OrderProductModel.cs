using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaginMA_2025_11_27.Model
{
    internal class OrderProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long OrderId { get; set; }

        public string ProductArticle { get; set; }

        public int Quantity { get; set; }

        public virtual OrderModel Order { get; set; }

        public virtual ProductModel Product { get; set; }
    }
}
