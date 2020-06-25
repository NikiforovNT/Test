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
        public Guid InspectorID { get; set; }
        public Inspectors Inspector { get; set; }
        public DateTime DateOfInspection { get; set; }
        public string Comment { get; set; }

        public ICollection<Notices> Notices { get; set; }

        public Inspections()
        {
            ID = Guid.NewGuid();
            Notices = new List<Notices>();
        }
        public Inspections(Guid id, string designation, Guid inspectorid, DateTime dateofinspection, string comment)
        {
            ID = id;
            Designation = designation;
            InspectorID = inspectorid;
            DateOfInspection = dateofinspection;
            Comment = comment;
            Notices = new List<Notices>();
        }
    }
}
