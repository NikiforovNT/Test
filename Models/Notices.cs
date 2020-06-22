using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тестовое_задание.Models
{
    public class Notices
    {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public DateTime DateOfElimination { get; set; }
        public string Comment { get; set; }
    }
}
