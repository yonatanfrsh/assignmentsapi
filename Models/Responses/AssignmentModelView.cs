using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class AssignmentModelView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AssignmentTypeModelView? AssignmentType { get; set; }
        public bool Repeated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Ended { get; set; }
        public string? Description { get; set; }
        public bool Archive { get; set; }
    }
}
