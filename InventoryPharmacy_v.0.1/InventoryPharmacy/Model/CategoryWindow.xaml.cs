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

namespace InventoryPharmacy.Model
{
    public partial class CategoryWindow : Window
    {
        ApplicationCategory dbCat;
        TgMessage tg;

        public CategoryWindow()
        {
            InitializeComponent();

            dbCat = new ApplicationCategory();
            LoadDataGrid();

            backButton.Click += ToHomeScreenWindow;
            addItem.Click += ToCreateOrEditCategoryWindow;
            changeItem.Click += ToEditeItems;
            removeItem.Click += RemoveCat;
            searchTextBox.TextChanged += SearchTextBox;

            tg = new TgMessage();
        }

        private void ToHomeScreenWindow(object sender, RoutedEventArgs e)
        {
            HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
            homeScreenWindow.Show();
            homeScreenWindow.roleHome.Text = roleCat.Text;
            homeScreenWindow.SwichRole(roleCat.Text);
            Close();
        }

        private void ToCreateOrEditCategoryWindow(object sender, RoutedEventArgs e)
        {
            CreateOrEditCategoryWindow createOrEditCategoryWindow = new CreateOrEditCategoryWindow();
            createOrEditCategoryWindow.Show();
            createOrEditCategoryWindow.roleAddItems.Text = roleCat.Text;
            createOrEditCategoryWindow.idItemPanel.Visibility = Visibility.Collapsed;
            createOrEditCategoryWindow.saveChangeItem.Visibility = Visibility.Collapsed;
            Close();
        }

        private void ToEditeItems(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                CreateOrEditCategoryWindow createOrEditCategoryWindow = new CreateOrEditCategoryWindow();
                createOrEditCategoryWindow.Show();
                createOrEditCategoryWindow.roleAddItems.Text = roleCat.Text;
                createOrEditCategoryWindow.saveItem.Visibility = Visibility.Collapsed;

                if (dataGrid.SelectedItem is Category selectedPerson)
                {
                    createOrEditCategoryWindow.idTextBox.Text = selectedPerson.id.ToString();
                    createOrEditCategoryWindow.nameTextBox.Text = selectedPerson.Text;
                    createOrEditCategoryWindow.descriptionTextBox.Text = selectedPerson.Description;

                    createOrEditCategoryWindow.nameText = selectedPerson.Text;
                    createOrEditCategoryWindow.descriptionText = selectedPerson.Description;
                }

                Close();
            }
        }

        private void RemoveCat(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Category selectedItem)
            {
                dbCat.Categories.Remove(selectedItem);
                dbCat.SaveChanges();
                LoadDataGrid();

                tg.SendMessage($"Удаление категории: ```\n\nКатегория: {selectedItem.Text} \r\nОписание: {selectedItem.Description} \n\r ``` \r\n");
            }
        }

        private void LoadDataGrid()
        {
            try
            {
                var items = dbCat.Categories.ToList();
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
                var filteredItems = dbCat.Categories.Where(i => i.Text.ToLower().Contains(search)).ToList();
                dataGrid.ItemsSource = filteredItems;
            }
        }
    }
}
