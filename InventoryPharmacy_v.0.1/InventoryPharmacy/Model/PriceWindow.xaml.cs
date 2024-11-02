using InventoryPharmacy.View;
using Microsoft.Win32;
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
using Telegram.Bot.Types;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace InventoryPharmacy.Model
{
    public partial class PriceWindow : Window
    {
        ApplicationItemContext dbItem;

        public PriceWindow()
        {
            InitializeComponent();
            backButton.Click += ToHomeScreenWindow;
            createPrice.Click += CreateFilePrice;
            dbItem = new ApplicationItemContext();
        }

        private void ToHomeScreenWindow(object sender, RoutedEventArgs e)
        {
            HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
            homeScreenWindow.Show();
            homeScreenWindow.roleHome.Text = rolePrice.Text;
            homeScreenWindow.SwichRole(rolePrice.Text);
            Close();
        }

        private void CreateFilePrice(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word (*.docx)|*.docx|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                MessageBox.Show($"Файл будет сохранен по пути: {filePath}");

                using (var document = DocX.Create(filePath))
                {
                    int columns = 3;
                    int rows = (dbItem.Products.Count() + columns - 1) / columns;

                    var table = document.InsertTable(rows, columns);
                    List<Product> products = dbItem.Products.ToList();

                    int currentRow = 0;
                    
                    int currentCol = 0;
                    Random rand = new Random();

                    foreach (Product product in products)
                    {
                        if (currentCol >= columns)
                        {
                            currentRow++;
                            currentCol = 0;
                        }

                        int random = rand.Next(100000, 999999);

                        table.Rows[currentRow].Cells[currentCol].Paragraphs[0]
                            .Append("ООО \"Аптека\"\r\n")
                            .FontSize(12)
                            .Alignment = ((Xceed.Document.NET.Alignment)Alignment.center);
                        table.Rows[currentRow].Cells[currentCol].Paragraphs[0]
                            .Append($"{product.Name} \r\n\r\n")
                            .Bold(true)
                            .FontSize(16)
                            .Alignment = ((Xceed.Document.NET.Alignment)Alignment.left);
                        table.Rows[currentRow].Cells[currentCol].Paragraphs[0]
                            .Append($"{product.Price}\r\n")
                            .Bold(true)
                            .FontSize(24)
                            .Alignment = ((Xceed.Document.NET.Alignment)Alignment.right);
                        table.Rows[currentRow].Cells[currentCol].Paragraphs[0]
                            .Append($"№{random} от {DateTime.Now}\r\n")
                            .Bold(false)
                            .FontSize(12)
                            .Alignment = ((Xceed.Document.NET.Alignment)Alignment.right);

                        currentCol++;
                    }

                    document.Save();
                }
            }
        }
    }
}
