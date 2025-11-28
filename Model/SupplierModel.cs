using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaginMA_2025_11_27.Model
{
    public class SupplierModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ManufacturerModel> Manufacturers { get; set; }
        public virtual ICollection<SupplierModel> Suppliers { get; set; }
        public virtual ICollection<CategoryModel> Categories { get; set; }

    }
}
