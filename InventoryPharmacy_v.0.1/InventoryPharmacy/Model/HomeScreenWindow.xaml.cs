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

namespace InventoryPharmacy.Model
{
    public partial class HomeScreenWindow : Window
    {
        public HomeScreenWindow()
        {
            InitializeComponent();

            exitButton.Click += Exit;
            itemsButton.Click += ToItemsWindow;
            personalButton.Click += ToPersonalList;
            scanButton.Click += ToScanWindow;
            changeAccountButton.Click += ToMainWindow;
            addingButton.Click += ToAddingProduct;
            categoryButton.Click += ToCategoryWindow;
            priceButton.Click += ToPriceWindow;
        }

        private void ToPersonalList(object sender, RoutedEventArgs e)
        {
            PersonalsWindow personalWindow = new PersonalsWindow();
            personalWindow.Show();
            personalWindow.rolePersonal.Text = roleHome.Text;
            Close();
        }

        private void ToAddingProduct(object sender, RoutedEventArgs e)
        {
            AddingProducts addingProducts = new AddingProducts();
            addingProducts.Show();
            addingProducts.roleAddItem.Text = roleHome.Text;
            Close();
        }

        private void ToMainWindow(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите сменить аккаунт?", "Подтвердите действие", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            }
        }

        private void ToItemsWindow(object sender, RoutedEventArgs e)
        {
            ItemsWindow itemsWindow = new ItemsWindow();
            itemsWindow.Show();
            itemsWindow.roleItem.Text = roleHome.Text;
            Close();
        }

        private void ToScanWindow(object sender, RoutedEventArgs e)
        {
            ScanAndPurschaseWindow scan = new ScanAndPurschaseWindow();
            scan.Show();
            scan.roleScan.Text = roleHome.Text;
            Close();
        }

        private void ToCategoryWindow(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Show();
            categoryWindow.roleCat.Text = roleHome.Text;
            Close();
        }

        private void ToPriceWindow(object sender, RoutedEventArgs e)
        {
            PriceWindow priceWindow = new PriceWindow();
            priceWindow.Show();
            priceWindow.rolePrice.Text = roleHome.Text;
            Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите завершить работу?", "Подтвердите действие", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public void SwichRole(string role)
        {
            if (role.Contains("Администратор"))
            {
                
            }
            else if (role.Contains("Фармацевт"))
            {
                categoryButton.Visibility = Visibility.Collapsed;
                personalButton.Visibility = Visibility.Collapsed;
            }
            else if (role.Contains("Провизор"))
            {
                scanButton.Visibility = Visibility.Collapsed;
                personalButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                itemsButton.Visibility = Visibility.Collapsed;
                categoryButton.Visibility = Visibility.Collapsed;

                v1.Visibility = Visibility.Collapsed;

                addingButton.Visibility = Visibility.Collapsed;
                scanButton.Visibility = Visibility.Collapsed;

                v2.Visibility = Visibility.Collapsed;

                priceButton.Visibility = Visibility.Collapsed;
                personalButton.Visibility = Visibility.Collapsed;

                v3.Visibility = Visibility.Collapsed;
            }
        }
    }
}
