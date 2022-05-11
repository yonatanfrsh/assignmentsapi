using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Requests;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AssignmentTypeService : IAssignmentTypeService
    {
        private readonly AssignmentContext Context;

        public AssignmentTypeService(AssignmentContext context)
        {
            Context = context;
        }

        public async Task<AssignmentTypeModelView> NewAssignmentTypeAsync(AssignmentTypeModel assignmentType)
        {
            AssignmentType assignmentTypeE = new AssignmentType()
            {
                Name = assignmentType.Name
            };

            Context.Add(assignmentTypeE);

            await Context.SaveChangesAsync();

            return new AssignmentTypeModelView() { Id = assignmentTypeE.Id, Name = assignmentTypeE.Name };
        }

        public async Task<List<AssignmentTypeModelView>> GetAssignmentsTypesAsync()
        {
            //var x = await Context.AssignmentTypes.FirstOrDefaultAsync();

            var result = await Context.AssignmentTypes.AsNoTracking().OrderBy(aT => aT.Id).ToListAsync();
            //await assignmentEList = Context?.AssignmentTypes.FirstOrDefault();//?.ToList(); //.OrderBy(aT => aT.Id).ToList<AssignmentType>();
            List<AssignmentType> assignmentEList = result.ToList<AssignmentType>();
            List<AssignmentTypeModelView> assignmentEListMV = new List<AssignmentTypeModelView>();

            foreach (var item in assignmentEList)
            {
                assignmentEListMV.Add(new AssignmentTypeModelView()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return assignmentEListMV;

        }



        public async Task<AssignmentTypeModelView> GetAssignmentTypeByIdAsync(int typeId)
        {
            //var x = await Context.AssignmentTypes.FirstOrDefaultAsync();

            var result = await Context.AssignmentTypes.AsNoTracking().FirstOrDefaultAsync(t => t.Id == typeId);

            AssignmentTypeModelView assignmentEMV = new AssignmentTypeModelView()
            {
                Id = result.Id,
                Name = result.Name
            };

            return assignmentEMV;

        }

        //        Context.Add(assignmentE);

        //        //Context
        //        //var x = await Context.Assignments.FirstOrDefault(p => p.Id = id...)


        //        await Context.SaveChangesAsync();

        //        return new AssignmentModelView();
        //    }

        //}
    }
}