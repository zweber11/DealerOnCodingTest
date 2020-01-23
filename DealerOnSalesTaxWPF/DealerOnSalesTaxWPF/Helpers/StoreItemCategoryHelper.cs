using DealerOnSalesTaxWPF.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerOnSalesTaxWPF.Helpers
{
    public class StoreItemCategoryHelper
    {
        public StoreItemCategory GetStoreItemCategoryByName(string categoryName, List<StoreItemCategory> categories)
        {
            //Use a Linq query to retrieve the Category that matches the name.
            return categories.Where(x => x.Name == categoryName).FirstOrDefault();
        }

        public List<StoreItemCategory> InitializeStoreItemCategories()
        {
            //Function is used to loop through a statically defined list of strings, and build StoreItemCategory objects for use throughout the app. 
            string[] storeCategoryNames = new string[] { "Books", "Food", "Medical", "Other" };
            List<StoreItemCategory> storeItemCategories = new List<StoreItemCategory>();

            //Loop through the names array and instantiate objects.
            for (int i = 0; i < storeCategoryNames.Length; i++)
            {
                StoreItemCategory newCategory = new StoreItemCategory()
                {
                    Name = storeCategoryNames[i],
                    BasicSalesTaxRate = 10.0M,
                    ImportSalesTaxRate = 5.0M
                };

                //If the CategoryName isn't Other, set BasicSalesTaxExempt = true. 
                //*The default value for the bool data type in C# is false, so we'll handle that condition when we instantiate the object beforehand.
                if (newCategory.Name != "Other")
                {
                    newCategory.BasicSalesTaxExempt = true;
                }

                //Furthermore, the ImportTaxExempt value will always be false on object instantiation, 
                //but in the event certain categories become exempt in the future, we'll have a field to handle this case.

                //Add the Category to the list.
                storeItemCategories.Add(newCategory);
            }

            return storeItemCategories;
        }
    }
}
