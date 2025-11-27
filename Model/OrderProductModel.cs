using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaginMA_2025_11_27.Model
{
    public class OrderProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long AppOrderId { get; set; }

        public string AppProductArticle { get; set; }

        public int Quantity { get; set; }

        public virtual AppOrderModel AppOrder { get; set; }

        public virtual AppProductModel AppProduct { get; set; }
    }
}
