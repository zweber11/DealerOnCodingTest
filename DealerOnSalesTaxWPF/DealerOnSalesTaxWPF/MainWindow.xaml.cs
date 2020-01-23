using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DealerOnSalesTaxWPF.DataObjects;

namespace DealerOnSalesTaxWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<StoreItemCategory> storeItemCategories = new List<StoreItemCategory>();

        /// <summary>
        /// DealerOn Coding Test
        /// Problem #2: Sales Tax 
        /// Author: Zack Weber
        /// Date: 1/22/2020
        /// Built using Visual Studio 2017
        /// 
        /// Overview: 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            InitializeStoreItemCategories();

            //MessageBox.Show("Categories initialized.");
        }

        private void InitializeStoreItemCategories()
        {
            //Function is used to loop through a statically defined list of strings, and build StoreItemCategory objects for use throughout the app. 
            string[] storeCategoryNames = new string[] { "Books", "Food", "Medical", "Other" };

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

                //Populate the dropdown control with the category names.
                cboItemCategories.Items.Add(newCategory.Name);
            }
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add logic here.
            MessageBox.Show("Item was added to cart!");
        }

        private void BtnClearCart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to remove all of the items from your cart?", "Clear Cart button selected", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }
    }
}
