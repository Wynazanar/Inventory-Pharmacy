using InventoryPharmacy.View;
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
using System.Windows.Shapes;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InventoryPharmacy.Model
{
    public partial class CreateOrEditItemsWindow : Window
    {
        ApplicationItemContext dbItem;
        ApplicationCategory category;
        public string nameText, quantityText, priceText, barcodeText, catText;
        TgMessage tg;
        public CreateOrEditItemsWindow()
        {
            InitializeComponent();
            dbItem = new ApplicationItemContext();
            category = new ApplicationCategory();

            saveItem.Click += AddItem;
            saveChangeItem.Click += ChangeItem;
            backButton.Click += ToItemsWindow;

            tg = new TgMessage();

            try
            {
                var items = category.Categories.ToList();
                catComboBox.ItemsSource = items;
            }
            catch (Exception) { }
            
        }

        public void AddItem(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            
            string quantityInput = quantityTextBox.Text;
            int quatity;
            bool isQuantity = int.TryParse(quantityInput, out quatity);

            string priceInput = priceTextBox.Text.Replace(".", ",");
            float price;
            bool isPrice = float.TryParse(priceInput, out price);

            string barcode = barcodeTextBox.Text;
            string cat = catComboBox.SelectedItem.ToString();
            if (name.Length < 3)
            {
                nameTextBox.ToolTip = "Это поле введено некорректно!";
                nameTextBox.Background = Brushes.Red;
            }
            else if (isQuantity == false)
            {
                quantityTextBox.ToolTip = "Это поле введено некорректно!";
                quantityTextBox.Background = Brushes.Red;
            }
            else if (isPrice == false)
            {
                priceTextBox.ToolTip = "Это поле введено некорректно!";
                priceTextBox.Background = Brushes.Red;
            }
            else if (barcode.Length < 3)
            {
                barcodeTextBox.ToolTip = "Это поле введено некорректно!";
                barcodeTextBox.Background = Brushes.Red;
            }
            else
            {
                nameTextBox.ToolTip = null;
                nameTextBox.Background = Brushes.Transparent;
                quantityTextBox.ToolTip = null;
                quantityTextBox.Background = Brushes.Transparent;
                priceTextBox.ToolTip = null;
                priceTextBox.Background = Brushes.Transparent;
                barcodeTextBox.ToolTip = null;
                barcodeTextBox.Background = Brushes.Transparent;
                
                Product product = new Product(name, quantityInput, priceInput, barcode, cat);
                dbItem.Products.Add(product);
                dbItem.SaveChanges();

                tg.SendMessage($"Добавление товара: ```\n\rНаименование: {name} \n\rКатегория: {catText} --> {cat} \n\rКоличество: {quantityInput} \r\nЦена: {priceInput} \r\nШтрих-код: {barcode}\n\r ``` \r\n");

                MessageBox.Show("Товар успешно добавлен!", "Добавление товара");
                ItemsWindow itemsWindow = new ItemsWindow();
                itemsWindow.Show();
                itemsWindow.roleItem.Text = roleAddItems.Text;
                Close();
            }
        }

        private void ToItemsWindow(object sender, RoutedEventArgs e)
        {
            ItemsWindow itemsWindow = new ItemsWindow();
            itemsWindow.Show();
            itemsWindow.roleItem.Text = roleAddItems.Text;
            Close();
        }

        public void ChangeItem(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            string cat = catComboBox.SelectedItem.ToString();

            string quantityInput = quantityTextBox.Text;
            int quatity;
            bool isQuantity = int.TryParse(quantityInput, out quatity);

            string priceInput = priceTextBox.Text.Replace(".", ",");
            float price;
            bool isPrice = float.TryParse(priceInput, out price);

            string barcode = barcodeTextBox.Text;

            if (name.Length < 3)
            {
                nameTextBox.ToolTip = "Это поле введено некорректно!";
                nameTextBox.Background = Brushes.Red;
            }
            else if (isQuantity == false)
            {
                quantityTextBox.ToolTip = "Это поле введено некорректно!";
                quantityTextBox.Background = Brushes.Red;
            }
            else if (isPrice == false)
            {
                priceTextBox.ToolTip = "Это поле введено некорректно!";
                priceTextBox.Background = Brushes.Red;
            }
            else if (barcode.Length < 3)
            {
                barcodeTextBox.ToolTip = "Это поле введено некорректно!";
                barcodeTextBox.Background = Brushes.Red;
            }
            else
            {
                var product = dbItem.Products.FirstOrDefault(p => p.id == id);
                product.Name = name;
                product.Quantity = int.Parse(quantityInput);
                product.Price = price;
                product.Barcode = barcode;
                product.Category = cat;
                dbItem.SaveChanges();

                tg.SendMessage($"Изменение товара: ```\r\nНаименование: {nameText} --> {name} \n\rКатегория: {catText} --> {cat} \n\rКоличество: {quantityText} --> {quantityInput} \n\rЦена: {priceText} --> {priceInput} \n\rШтрих-код: {barcodeText} --> {barcode}\n\r ``` \r\n");

                MessageBox.Show("Товар успешно обновлен!", "Обновление товара");
                ItemsWindow itemsWindow = new ItemsWindow();
                itemsWindow.Show();
                itemsWindow.roleItem.Text = roleAddItems.Text;
                Close();
            }
        }
    }
}
