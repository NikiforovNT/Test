using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тестовое_задание.Models
{
    public class Inspectors
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public ICollection<Inspections> Inspections { get; set; }
        public Inspectors()
        {
            ID = Guid.NewGuid();
            Inspections = new List<Inspections>();
        }
        public Inspectors(Guid id, string name, int number)
        {
            ID = id;
            Name = name;
            Number = number;
            Inspections = new List<Inspections>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
