using InventoryPharmacy.View;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
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
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace InventoryPharmacy.Model
{
    public partial class ItemsWindow : Window
    {
        ApplicationItemContext dbItem;
        TgMessage tg;

        public ItemsWindow()
        {
            InitializeComponent();

            dbItem = new ApplicationItemContext();
            LoadDataGrid();

            backButton.Click += ToHomeScreenWindow;
            addItem.Click += ToCreateOrEditeItems;
            changeItem.Click += ToEditeItems;
            removeItem.Click += RemoveItem;
            searchTextBox.TextChanged += SearchTextBox;
            tgBut.Click += TestTgCommands;
            tg = new TgMessage();
        }

        private void ToHomeScreenWindow(object sender, RoutedEventArgs e)
        {
            HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
            homeScreenWindow.Show();
            homeScreenWindow.roleHome.Text = roleItem.Text;
            homeScreenWindow.SwichRole(roleItem.Text);
            Close();
        }

        private void ToCreateOrEditeItems(object sender, RoutedEventArgs e)
        {
            CreateOrEditItemsWindow createOrEditItemsWindow = new CreateOrEditItemsWindow();
            createOrEditItemsWindow.Show();
            createOrEditItemsWindow.roleAddItems.Text = roleItem.Text;
            createOrEditItemsWindow.idItemPanel.Visibility = Visibility.Collapsed;
            createOrEditItemsWindow.saveChangeItem.Visibility = Visibility.Collapsed;
            Close();
        }

        private void ToEditeItems(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                CreateOrEditItemsWindow editItemsWindow = new CreateOrEditItemsWindow();
                editItemsWindow.Show();
                editItemsWindow.roleAddItems.Text = roleItem.Text;
                editItemsWindow.saveItem.Visibility = Visibility.Collapsed;

                if (dataGrid.SelectedItem is Product selectedPerson)
                {
                    editItemsWindow.idTextBox.Text = selectedPerson.id.ToString();
                    editItemsWindow.nameTextBox.Text = selectedPerson.Name;
                    editItemsWindow.quantityTextBox.Text = selectedPerson.Quantity.ToString();
                    editItemsWindow.priceTextBox.Text = selectedPerson.Price.ToString();
                    editItemsWindow.barcodeTextBox.Text = selectedPerson.Barcode;
                    editItemsWindow.catComboBox.Text = selectedPerson.Category;

                    editItemsWindow.nameText = selectedPerson.Name;
                    editItemsWindow.quantityText = selectedPerson.Quantity.ToString();
                    editItemsWindow.priceText = selectedPerson.Price.ToString();
                    editItemsWindow.barcodeText = selectedPerson.Barcode;
                    editItemsWindow.catText = selectedPerson.Category;
                }

                Close();
            }
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Product selectedItem)
            {
                dbItem.Products.Remove(selectedItem);
                dbItem.SaveChanges();
                LoadDataGrid();

                tg.SendMessage($"Удаление товара: ```\n\rНаименование: {selectedItem.Name} \r\nКоличество: {selectedItem.Quantity} \r\nЦена: {selectedItem.Price} \r\nШтрих-код: {selectedItem.Barcode}\n\r ``` \r\n");
            }
        }

        private void LoadDataGrid()
        {
            try
            {
                var items = dbItem.Products.ToList();
                dataGrid.ItemsSource = items;
            }
            catch (Exception) { }
        }

        private void SearchTextBox(object sender, TextChangedEventArgs e)
        {
            string search = searchTextBox.Text.ToLower();
            if (search == "")
            {
                LoadDataGrid();
            }
            else
            {
                var filteredItems = dbItem.Products.Where(i => i.Name.ToLower().Contains(search)).ToList();
                dataGrid.ItemsSource = filteredItems;
            }
        }

        private void TestTgCommands(object sender, RoutedEventArgs e)
        {
            List<Product> products = dbItem.Products.ToList();
            string message = "";
            foreach (Product product in products)
            {
                message += $"Наименовение: {product.Name} | Количество: {product.Quantity} | Цена {product.Price} | Штрих-код {product.Barcode} \r\n";
            }
            tg.SendMessage($"Список товаров: ```\r\n{message}\r\n```");
        }

    }
}
