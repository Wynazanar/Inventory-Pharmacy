using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryPharmacy.View
{
    public class Category
    {
        public int id { get; set; }
        private string text, description;

        public string Text 
        { 
            get { return text; }
            set { text = value; } 
        }

        public string Description 
        { 
            get { return description; } 
            set { description = value; } 
        }

        public Category() { }

        public Category(string text, string description)
        {
            this.text = text;
            this.description = description;
        }

        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
