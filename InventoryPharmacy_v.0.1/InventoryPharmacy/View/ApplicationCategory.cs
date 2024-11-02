using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryPharmacy.View
{
    class ApplicationCategory : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApplicationCategory() : base("DefaultConnection3") { }
    }
}
