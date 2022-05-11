using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Assignment
    {
        [Required, Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [ForeignKey("AssignmentType")]
        public int TypeId { get; set; }
        public AssignmentType? AssignmentType { get; set; }
        public bool Repeated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Ended { get; set; }
        [MaxLength(400)]
        public string? Description { get; set; }
        public bool Archive { get; set; }
    }
}
