using DealerOnSalesTaxWPF.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTaxWPF.Helpers
{
    public class StoreItemHelper
    {
        public List<StoreItem> GetStoreItemsByName(string categoryName, List<StoreItem> storeItems)
        {
            //Use a Linq query to retrieve the Category that matches the name.
            return storeItems.Where(x => x.Category.Name == categoryName).ToList();
        }

        public decimal CalculateItemPrice(decimal price, decimal quantity)
        {
            return price * quantity;
        }

        public decimal CalculateSubTotal(List<StoreItem> storeItems)
        {
            decimal subtotal = 0.00M;

            for (int i = 0; i < storeItems.Count; i++)
            {
                decimal itemTotal = CalculateItemPrice(storeItems[i].Price, storeItems[i].Quantity);
                subtotal += itemTotal;
            }

            return subtotal;
        }

        public decimal CalculateSalesTaxTotal(List<StoreItem> storeItems)
        {
            decimal salesTaxTotal = 0.00M;

            for (int i = 0; i < storeItems.Count; i++)
            {
                if (!storeItems[i].Category.BasicSalesTaxExempt)
                {
                    decimal itemTotal = storeItems[i].Quantity * storeItems[i].Price * (storeItems[i].Category.BasicSalesTaxRate / 100);
                    salesTaxTotal += itemTotal;
                }
            }

            return Math.Ceiling(salesTaxTotal * 20) / 20;
        }

        public decimal CalculateImportTaxTotal(List<StoreItem> storeItems)
        {
            decimal importTaxTotal = 0.00M;

            for (int i = 0; i < storeItems.Count; i++)
            {
                if (!storeItems[i].Category.ImportTaxExempt)
                {
                    decimal itemTotal = storeItems[i].Quantity * storeItems[i].Price * (storeItems[i].Category.ImportSalesTaxRate / 100);
                    importTaxTotal += itemTotal;
                }
            }

            return Math.Ceiling(importTaxTotal * 20) / 20;
        }

        public decimal CalculateGrandTotal(List<StoreItem> storeItems)
        {
            decimal subtotal = CalculateSubTotal(storeItems);
            decimal salesTaxTotal = CalculateSalesTaxTotal(storeItems);
            decimal importTaxTotal = CalculateImportTaxTotal(storeItems);

            return subtotal + salesTaxTotal + importTaxTotal;
        }

        public decimal CalculateGrandTotal(decimal subtotal, decimal salesTaxTotal, decimal importTaxTotal)
        {
            return subtotal + salesTaxTotal + importTaxTotal;
        }
    }
}
