using DealerOnSalesTaxWPF.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTaxWPF.Helper
{
    public class StoreItemCategoryHelper
    {
        public StoreItemCategory GetStoreItemCategoryByName(string categoryName, List<StoreItemCategory> categories)
        {
            //Use a Linq query to retrieve the Category that matches the name.
            return categories.Where(x => x.Name == categoryName).FirstOrDefault();
        }
    }
}
