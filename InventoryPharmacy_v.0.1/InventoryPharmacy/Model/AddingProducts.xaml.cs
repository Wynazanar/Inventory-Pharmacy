using InventoryPharmacy.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace InventoryPharmacy.Model
{
    public partial class AddingProducts : Window
    {
        ApplicationItemContext dbItem;
        TgMessage tg;
        public AddingProducts()
        {
            InitializeComponent();

            dbItem = new ApplicationItemContext();
            tg = new TgMessage();

            backButton.Click += ToHomeScreenWindow;
            addItem.Click += AddItem;
            addQuantity.Click += AddQuantity;
            removeQuantity.Click += RemoveQuantity;
            addPrice.Click += AddPrice;
            removePrice.Click += RemovePrice;
            saveButton.Click += SaveChanges;

            saveButton.IsEnabled = false;
        }

        private void ToHomeScreenWindow(object sender, RoutedEventArgs e)
        {
            HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
            homeScreenWindow.Show();
            homeScreenWindow.roleHome.Text = roleAddItem.Text;
            homeScreenWindow.SwichRole(roleAddItem.Text);
            Close();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            string barcode = barcodeInputTextBox.Text;
            LoadProductData(barcode);
            barcodeInputTextBox.Text = null;
        }

        private void LoadProductData(string barcodeText)
        {
            Product product = null;
            using (ApplicationItemContext dbItem = new ApplicationItemContext())
            {
                product = dbItem.Products.Where(b => b.Barcode == barcodeText).FirstOrDefault();

                if (product != null)
                {
                    infoTextBlock.Foreground = Brushes.Green;
                    infoTextBlock.Text = "Информация о данном товаре найдена";

                    idTextBox.Text = product.id.ToString();
                    nameTextBox.Text = product.Name;
                    quantityTextBox.Text = product.Quantity.ToString();
                    priceTextBox.Text = product.Price.ToString();
                    saveButton.IsEnabled = true;
                }
                else
                {
                    infoTextBlock.Foreground = Brushes.Red;
                    infoTextBlock.Text = "Нет информации о данном товаре!";

                    nameTextBox.Text = null;
                    quantityTextBox.Text = null;
                    priceTextBox.Text = null;
                    saveButton.IsEnabled = false;
                }
            }

        }

        private void AddQuantity(object sender, RoutedEventArgs e)
        {
            if (quantityTextBox.Text != "")
            {
                int quant = int.Parse(quantityTextBox.Text);
                quant++;
                quantityTextBox.Text = quant.ToString();
            }
        }
        private void RemoveQuantity(object sender, RoutedEventArgs e)
        {
            if (quantityTextBox.Text != "")
            {
                int quant = int.Parse(quantityTextBox.Text);
                quant--;
                quantityTextBox.Text = quant.ToString();
            }
        }
        private void AddPrice(object sender, RoutedEventArgs e)
        {
            if (priceTextBox.Text != "")
            {
                float price = float.Parse(priceTextBox.Text);
                price += 10;
                priceTextBox.Text = price.ToString();
            }
        }
        private void RemovePrice(object sender, RoutedEventArgs e)
        {
            if (priceTextBox.Text != "")
            {
                float price = float.Parse(priceTextBox.Text);
                price -= 10;
                priceTextBox.Text = price.ToString();
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            var product = dbItem.Products.FirstOrDefault(p => p.id == id);
            product.Name = nameTextBox.Text;
            product.Quantity = int.Parse(quantityTextBox.Text);
            product.Price = int.Parse(priceTextBox.Text);
            dbItem.SaveChanges();

            MessageBox.Show("Товар успешно обновлен!", "Обновление товара");
            tg.SendMessage($"Обновление товара: ```\r\nНаименование: {product.Name} \n\rКоличество: {product.Quantity} \n\rЦена: {product.Price} \n\r```");
            saveButton.IsEnabled = false;

            infoTextBlock.Text = "";
            idTextBox.Text = null;
            nameTextBox.Text = null;
            quantityTextBox.Text = null;
            priceTextBox.Text = null;
            saveButton.IsEnabled = false;
        }
    }
}
