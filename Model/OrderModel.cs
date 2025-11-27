using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaginMA_2025_11_27.Model
{
    internal class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int PickupPoint { get; set; }

        public long ClientId { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public virtual UserModel Client { get; set; }

        public virtual ICollection<OrderProductModel> OrderItems { get; set; }
    }
}
