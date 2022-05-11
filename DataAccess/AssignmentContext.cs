using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess
{
    public class AssignmentContext : DbContext
    {
        public DbSet<Assignment>? Assignments { get; set; }
        public DbSet<AssignmentType>? AssignmentTypes { get; set; }

        public AssignmentContext(DbContextOptions<AssignmentContext> options) : base(options) {

        }
    }
}
