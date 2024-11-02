using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryPharmacy.View
{
    public class Product
    {
        public int id { get; set; }
        private string name, barcode, category;
        private int quantity;
        private float price;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public Product() { }
        public Product(string name, string quantity, string price, string barcode, string cat)
        {
            this.name = name;
            this.quantity = int.Parse(quantity);
            this.price = float.Parse(price);
            this.barcode = barcode;
            this.category = cat;
        }

        public override string ToString()
        {
            return $"Наименовение: {Name} \r\nКоличество: {Quantity} \r\nЦена: {Price} \r\nШтрих-код: {Barcode}";
        }
    }
}
