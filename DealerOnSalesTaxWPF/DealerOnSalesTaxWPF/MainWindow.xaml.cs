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
using DealerOnSalesTaxWPF.Helpers;

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

            storeItemCategories = storeItemCategoryHelper.InitializeStoreItemCategories();
            PopulateItemCategoryControl(storeItemCategories);

            //InitializeCartColumns();
        }

        private void PopulateItemCategoryControl(List<StoreItemCategory> storeItemCategories)
        {
            for (int i = 0; i < storeItemCategories.Count; i++)
            {
                cboItemCategories.Items.Add(storeItemCategories[i].Name);
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
                BindCartData();

                CalculateTotals();
            }
        }

        private void CalculateTotals()
        {
            decimal subtotal = storeItemHelper.CalculateSubTotal(storeItems);
            decimal salesTaxTotal = storeItemHelper.CalculateSalesTaxTotal(storeItems);
            decimal importTaxTotal = storeItemHelper.CalculateImportTaxTotal(storeItems);
            decimal grandTotal = storeItemHelper.CalculateGrandTotal(subtotal, salesTaxTotal, importTaxTotal);

            lblSubtotalCalc.Content = subtotal;
            lblSalesTaxesCalc.Content = salesTaxTotal;
            lblImportTaxesCalc.Content = importTaxTotal;
            lblGrandTotalCalc.Content = grandTotal;
        }

        private void BindCartData()
        {
            lstCart.ItemsSource = null;
            lstCart.ItemsSource = storeItems;
        }

        private void BtnClearCart_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove all of the items from your cart?", 
                "Clear Cart button selected", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Clear out storeItems list, view, and receipt view.
                storeItems.Clear();
                BindCartData();
            }
        }

        private void CboItemCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //When the item category is changed, update the tax labels and calculated values accordingly.
            var selectedCategoryName = cboItemCategories.SelectedItem.ToString();

            //Use a Linq query to retrieve the Category that matches the name.
            StoreItemCategory selectedCategory = storeItemCategoryHelper.GetStoreItemCategoryByName(selectedCategoryName, storeItemCategories);
        }

        private void InitializeCartColumns()
        {
            string[] cartColumnHeaders = new string[] { "Item Category", "Description", "Quantity", "Price", "AddedOn" };
            GridView gridView = new GridView();

            for (int i = 0; i < cartColumnHeaders.Length; i++)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.DisplayMemberBinding = new Binding();
                gvc.Header = cartColumnHeaders[i];
                gvc.Width = 100;
                gridView.Columns.Add(gvc);
            }

            lstCart.View = gridView;
        }
    }
}
