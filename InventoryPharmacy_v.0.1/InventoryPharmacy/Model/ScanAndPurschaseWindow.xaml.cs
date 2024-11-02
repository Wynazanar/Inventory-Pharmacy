using InventoryPharmacy.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public partial class ScanAndPurschaseWindow : Window
    {
        ApplicationItemContext dbItem;
        EmailMessage email;
        private ObservableCollection<Product> products;
        float allCost = 0;
        int idItem = 0;

        public ScanAndPurschaseWindow()
        {
            InitializeComponent();
            addItem.Click += AddItem;
            doneScanButton.Click += MakeAPurchase;
            backButton.Click += ToHomeScreenWindow;

            products = new ObservableCollection<Product>();
            dataGrid.ItemsSource = products;
            allCountTextBox.Text = "Итоговая сумма: " + string.Format("{0:C2}", allCost);

            email = new EmailMessage();
            doneScanButton.IsEnabled = false;
        }

        private void ToHomeScreenWindow(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите прервать продажу?", "Подтвердите действие", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
                homeScreenWindow.Show();
                homeScreenWindow.roleHome.Text = roleScan.Text;
                homeScreenWindow.SwichRole(roleScan.Text);
                Close();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            string barcode = barcodeTextBox.Text.Trim();
            LoadProductData(barcode);
            barcodeTextBox.Text = null;
        }

        private void LoadProductData(string barcodeText)
        {
            Product product = null;
            using (ApplicationItemContext dbItem = new ApplicationItemContext())
            {
                product = dbItem.Products.Where(b => b.Barcode == barcodeText).FirstOrDefault();

                if (product != null)
                {
                    if (product.Quantity > 0)
                    {
                        idItem++;
                        products.Add(new Product { id = idItem, Barcode = product.Barcode, Quantity = 1, Name = product.Name, Price = product.Price });
                        allCost += product.Price;
                        doneScanButton.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Данный товар отсутствует на складе!", "Операция прервана");
                    }
                }
            }
            allCountTextBox.Text = "Итоговая сумма: " + string.Format("{0:C2}", allCost);
        }

        private void MakeAPurchase(object sender, RoutedEventArgs e)
        {
            SendEmail();
            using (ApplicationItemContext dbItem = new ApplicationItemContext())
            {
                foreach (var prod in products)
                {
                    var originalProduct = dbItem.Products.FirstOrDefault(b => b.Barcode == prod.Barcode);
                    if (originalProduct != null)
                    {
                        originalProduct.Quantity -= prod.Quantity;
                    }
                }
                dbItem.SaveChanges();
                products.Clear();
                allCost = 0;
                allCountTextBox.Text = "Итоговая сумма: " + string.Format("{0:C2}", allCost);
                doneScanButton.IsEnabled = false;
            }
        }

        private void SendEmail()
        {
            string textMessage = "";
            foreach (var prod in products)
            {
                textMessage += $"<tr style=\"border-bottom: 1px solid #EEE;\">" +
                    $"<td style=\"width: 60%;\"><p> {prod.Name}ﾠﾠﾠﾠﾠ</p></td>" +
                    $"<td style=\"width: 20%;\"><p> {prod.Quantity} </p></td>" +
                    $"<td style=\"width: 20%;\"><p> {prod.Price} </p></td>" +
                    "</tr>\r\n";
            }

            textMessage += $"<tr style=\"border-bottom: 1px solid #EEE;\">" +
                    $"<td style=\"width: 60%; text-align: right;\"><p><h2> Итоговая стоимость: </h2></p></td>" +
                    $"<td style=\"width: 20%;\"><p><h2> {allCost} </h2></p></td>" +
                    "</tr>\r\n";

            email.SendEmailMessage(textMessage);
        }

    }
}
