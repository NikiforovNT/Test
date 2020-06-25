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
        public Guid InspectionID { get; set; }
        public Inspections Inspection { get; set; }

        public Notices()
        {
            ID = Guid.NewGuid();
        }
        public Notices(Guid id, string text, DateTime dateofelimination, string comment, Guid inspectionid)
        {
            ID = id;
            Text = text;
            DateOfElimination = dateofelimination;
            Comment = comment;
            InspectionID = inspectionid;
        }
    }
}
