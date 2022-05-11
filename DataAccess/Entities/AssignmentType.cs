using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class AssignmentType
    {
        [Required, Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
