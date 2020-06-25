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
        public UserContext() : base("DbConnection")
        {
        }

        public DbSet<Inspections> Inspections { get; set; }
        public DbSet<Inspectors> Inspectors { get; set; }
        public DbSet<Notices> Notices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UserContext>(new ContextInitializer());
            base.OnModelCreating(modelBuilder);
        }

    }

    class ContextInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            Inspectors firstinspector = new Inspectors(Guid.NewGuid(), "Иванов А.А.", 145698);
            db.Inspectors.Add(firstinspector);
            Inspections firstinspection = new Inspections(Guid.NewGuid(), "Пожарная инспекция", firstinspector.ID, DateTime.Today, "Комментариев нет");
            db.Inspections.Add(firstinspection);
            db.Notices.Add(new Notices(Guid.NewGuid(), "Первое замечание", DateTime.Today, "Комментариев нет",  firstinspection.ID ));
            db.Notices.Add(new Notices(Guid.NewGuid(), "Второе замечание", DateTime.Today, "Комментариев нет",  firstinspection.ID ));
            db.SaveChanges();
        }
    }
}
