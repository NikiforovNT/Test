using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Тестовое_задание.Models;

namespace Тестовое_задание
{
    class UserContext : DbContext
    {
        public UserContext(): base("DbConnection")
        { }

        public DbSet<Inspections> Inspections { get; set; }
        public DbSet<Inspectors> Inspectors { get; set; }
        public DbSet<Notices> Notices { get; set; }
    }
}
