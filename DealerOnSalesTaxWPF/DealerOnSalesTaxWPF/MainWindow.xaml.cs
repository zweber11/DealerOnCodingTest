﻿using System;
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
using DealerOnSalesTaxWPF.Helper;

namespace DealerOnSalesTaxWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<StoreItemCategory> storeItemCategories = new List<StoreItemCategory>();
        List<StoreItem> storeItems = new List<StoreItem>();
        StoreItemHelper storeItemHelper = new StoreItemHelper();
        StoreItemCategoryHelper storeItemCategoryHelper = new StoreItemCategoryHelper();

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
            int enteredQuantity;
            decimal enteredPrice;

            //Check all StoreItem fields to ensure there are valid values in each.
            if (cboItemCategories.SelectedItem == null) //ItemCategory
            {
                MessageBox.Show("Please select an Item Category.");
            }
            else if (txtItemDescription.Text == null) //ItemDescription
            {
                MessageBox.Show("Please enter an item description.");
            }
            else if (!int.TryParse(txtQuantity.Text, out enteredQuantity)) //Quantity
            {
                MessageBox.Show("Please enter a valid whole number Quantity. Entered value: " + txtQuantity.Text);
            }
            else if (!decimal.TryParse(txtPrice.Text, out enteredPrice)) //Price
            {
                MessageBox.Show("Please enter a valid Price. Entered value: " + txtPrice.Text);
            }
            else //If all fields check out, add the item to the StoreItems list, view, and update the receipt.
            {
                StoreItemCategory selectedCategory = storeItemCategoryHelper.GetStoreItemCategoryByName(cboItemCategories.SelectedItem.ToString(), storeItemCategories);

                StoreItem newItem = new StoreItem()
                {
                    Category = selectedCategory, 
                    Description = txtItemDescription.Text.Trim(),
                    Quantity = enteredQuantity,
                    Price = enteredPrice,
                    AddedOn = DateTime.Now
                };

                storeItems.Add(newItem);
                lstCart.Items.Add(newItem);
            }
        }

        private void BtnClearCart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to remove all of the items from your cart?", "Clear Cart button selected", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            //TODO: Add handling for Yes, No.

            //Clear out storeItems list, view, and receipt view.
        }

        private void CboItemCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //When the item category is changed, update the tax labels and calculated values accordingly.
            var selectedCategoryName = cboItemCategories.SelectedItem.ToString();

            //Use a Linq query to retrieve the Category that matches the name.
            StoreItemCategory selectedCategory = storeItemCategoryHelper.GetStoreItemCategoryByName(selectedCategoryName, storeItemCategories);

            //Check the BasicSalesTaxExempt value
            if (selectedCategory.BasicSalesTaxExempt)
            {
                lblBasicSalesTaxLabel.Content = "Basic Sales Tax Exempt";
                lblBasicSalesTaxCalc.Content = "N/A";
            }
            else
            {
                lblBasicSalesTaxLabel.Content = "Basic Sales Tax (" + selectedCategory.BasicSalesTaxRate.ToString() + "%)";

                //TODO: recalculate tax & total here.
                lblBasicSalesTaxCalc.Content = "N/A";
            }

            //Check the ImportTaxExempt value
            if (selectedCategory.ImportTaxExempt)
            {
                lblImportTaxLabel.Content = "Import Tax Exempt";
                lblImportTaxCalc.Content = "N/A";
            }
            else
            {
                lblImportTaxLabel.Content = "Import Tax (" + selectedCategory.ImportSalesTaxRate.ToString() + "%)";

                //TODO: recalculate tax & total here.
                lblImportTaxCalc.Content = "N/A";
            }
        }
    }
}
