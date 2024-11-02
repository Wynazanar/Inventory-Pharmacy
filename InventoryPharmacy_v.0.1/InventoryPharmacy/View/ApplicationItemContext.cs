using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace InventoryPharmacy.View
{
    class ApplicationItemContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationItemContext() : base("DefaultConnection") { }

    }
}
