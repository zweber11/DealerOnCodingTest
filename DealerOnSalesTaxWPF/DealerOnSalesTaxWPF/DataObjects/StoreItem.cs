using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTaxWPF.DataObjects
{
    public class StoreItem
    {
        public StoreItemCategory Category { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
