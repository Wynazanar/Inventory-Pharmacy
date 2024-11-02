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
    public partial class CreateOrEditCategoryWindow : Window
    {
        ApplicationCategory dbCat;

        public string nameText, descriptionText;
        TgMessage tg;

        public CreateOrEditCategoryWindow()
        {
            InitializeComponent();
            dbCat = new ApplicationCategory();
            saveItem.Click += AddCat;
            saveChangeItem.Click += ChangeCat;
            backButton.Click += ToCatWindow;

            tg = new TgMessage();
        }

        private void ToCatWindow(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Show();
            categoryWindow.roleCat.Text = roleAddItems.Text;
            Close();
        }

        public void AddCat(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string desc = descriptionTextBox.Text;

            if (name.Length < 3)
            {
                nameTextBox.ToolTip = "Это поле введено некорректно!";
                nameTextBox.Background = Brushes.Red;
            }
            else if (desc.Length < 5)
            {
                descriptionTextBox.ToolTip = "Это поле введено некорректно!";
                descriptionTextBox.Background = Brushes.Red;
            }
            else
            {
                nameTextBox.ToolTip = null;
                nameTextBox.Background = Brushes.Transparent;
                descriptionTextBox.ToolTip = null;
                descriptionTextBox.Background = Brushes.Transparent;

                Category category = new Category(name, desc);
                dbCat.Categories.Add(category);
                dbCat.SaveChanges();

                tg.SendMessage($"Добавление категории: ```\n\rКатегория: {name} \r\nОписание: {desc} \r\n ``` \r\n");

                MessageBox.Show("Категория успешно добавлена!", "Добавление категории");
                CategoryWindow categoryWindow = new CategoryWindow();
                categoryWindow.Show();
                categoryWindow.roleCat.Text = roleAddItems.Text;
                Close();
            }
        }

        public void ChangeCat(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            string desc = descriptionTextBox.Text;

            if (name.Length < 3)
            {
                nameTextBox.ToolTip = "Это поле введено некорректно!";
                nameTextBox.Background = Brushes.Red;
            }
            else if (desc.Length < 5)
            {
                descriptionTextBox.ToolTip = "Это поле введено некорректно!";
                descriptionTextBox.Background = Brushes.Red;
            }
            else
            {
                var cat = dbCat.Categories.FirstOrDefault(p => p.id == id);
                cat.Text = name;
                cat.Description = desc;
                dbCat.SaveChanges();

                tg.SendMessage($"Изменение категории: ```\r\nКатегория: {nameText} --> {name} \n\rОписание: {descriptionText} --> {desc} \n\r ``` \r\n");

                MessageBox.Show("Категория успешно обновлена!", "Обновление категории");
                CategoryWindow categoryWindow = new CategoryWindow();
                categoryWindow.Show();
                categoryWindow.roleCat.Text = roleAddItems.Text;
                Close();
            }
        }
    }
}
