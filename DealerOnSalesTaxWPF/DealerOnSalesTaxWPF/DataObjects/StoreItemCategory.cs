using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTaxWPF.DataObjects
{
    public class StoreItemCategory
    {
        public string Name { get; set; }

        public bool BasicSalesTaxExempt { get; set; }

        public bool ImportTaxExempt { get; set; }

        public decimal BasicSalesTaxRate { get; set; }

        public decimal ImportSalesTaxRate { get; set; }
    }
}