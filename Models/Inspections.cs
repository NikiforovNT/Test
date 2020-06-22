using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тестовое_задание.Models
{
    public class Inspections
    {
        public Guid ID { get; set; }
        public string Designation { get; set; }
        public Inspectors Inspector { get; set; }
        public DateTime DateOfInspection { get; set; }
        public string Comment { get; set; }
        public List<Notices> Notices { get; set; }
    }
}
