using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace InventoryPharmacy.View
{
    class ApplicationPersonal : DbContext
    {
        public DbSet<Personal> Personals { get; set; }
        public ApplicationPersonal() : base("DefaultConnection2") { }

    }
}
