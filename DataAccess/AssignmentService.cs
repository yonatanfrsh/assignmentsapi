using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Requests;
using Models.Responses;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AssignmentService : IAssignmentService
    {
        private readonly AssignmentContext Context;

        public AssignmentService(AssignmentContext context)
        {
            Context = context;
        }

        public async Task<int> CountAssignmentsAsync(bool includeArchive)
        {
            int result = 0;
            if (includeArchive)
            {
                result = await Context.Assignments.CountAsync();
            }
            else
            {
                result = await Context.Assignments.CountAsync(a => a.Archive == false);
            }

            return result;
        }


        public async Task<AssignmentModelView> NewAssignmentAsync(AssignmentModel assignment)
        {
            var archive = assignment.Archive;
            if (archive == false)
            {
                archive = assignment.EndDate?.AddDays(7) < DateTime.Now;
            }
            Assignment assignmentE = new Assignment()
            {
                Name = assignment.Name,
                TypeId = assignment.TypeId,
                Repeated = assignment.Repeated,
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate,
                Ended = assignment.Ended,
                Description = assignment.Description,
                Archive = archive,
                Location = "Israel"
            };

            Context.Add(assignmentE);

            //Context
            //var x = await Context.Assignments.FirstOrDefault(p => p.Id = id...)


            await Context.SaveChangesAsync();

            return new AssignmentModelView();
        }

        public async Task<IEnumerable<AssignmentModelView>> ArchiveAssignmentsAsync()
        {
            DateTime archiveDate = DateTime.Today.AddDays(-7);
            var x = await Context.Assignments.AsQueryable()
                .Where(s =>
                    s.EndDate < archiveDate &&
                    s.Archive == false)
                .Include(m => m.AssignmentType)
                .ToListAsync<Assignment>();

            List<AssignmentModelView> result = new List<AssignmentModelView>();
            foreach (var item in x)
            {
                item.Archive = true;
                result.Add(new AssignmentModelView()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Repeated = item.Repeated,
                    AssignmentType = new AssignmentTypeModelView()
                    {
                        Id = item.AssignmentType.Id,
                        Name = item.AssignmentType.Name
                    },
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Ended = item.Ended,
                    Description = item.Description,
                    Archive = item.Archive

                });
            }

            await Context.SaveChangesAsync();


            return result;
        }

        public async Task<AssignmentModelView> ArchiveAssignmentAsync(AssignmentModelView assignmentMV)
        {
            var result = await Context.Assignments.AsQueryable().FirstOrDefaultAsync(a => a.Id == assignmentMV.Id);
            result.Archive = true;
            await Context.SaveChangesAsync();

            return assignmentMV;
        }
        public async Task<IEnumerable<AssignmentModelView>> ArchiveMultipleAssignmentsAsync(List<AssignmentModelView> assignmentsMV)
        {
            List<int> ids = assignmentsMV.Select(a => a.Id).ToList();
            var result = await Context.Assignments.AsQueryable().Where(a => ids.Contains(a.Id)).ToListAsync();

            foreach (var item in result)
            {
                item.Archive = true;
            }
            await Context.SaveChangesAsync();

            return assignmentsMV;
        }

        public async Task<IEnumerable<AssignmentModelView>> GetAssignmentsAsync(bool withArchive, int page)
        {
            //var x = await Context.AssignmentTypes.FirstOrDefaultAsync();

            var result = await Context.Assignments.AsNoTracking()
                .Where(a => a.Archive == false || withArchive)
                .OrderByDescending(a => a.StartDate)
                .Include(aT => aT.AssignmentType).ToListAsync();
            //await assignmentEList = Context?.AssignmentTypes.FirstOrDefault();//?.ToList(); //.OrderBy(aT => aT.Id).ToList<AssignmentType>();
            List<AssignmentModelView> assignmentEListMV = new List<AssignmentModelView>();

            foreach (var item in result)
            {
                assignmentEListMV.Add(new AssignmentModelView()
                {
                    Id = item.Id,
                    Name = item.Name,
                    AssignmentType = new AssignmentTypeModelView()
                    {
                        Id = item.AssignmentType.Id,
                        Name = item.AssignmentType.Name
                    },
                    Repeated = item.Repeated,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Ended = item.Ended,
                    Description = item.Description,
                    Archive = item.Archive
                });
            }

            return assignmentEListMV;

        }

        public async Task<IEnumerable<AssignmentModelView>> SetMultipleAssignmentsEndedAsync(List<AssignmentModelView> assignmentsMV)
        {
            List<int> ids = assignmentsMV.Select(a => a.Id).ToList();
            var result = await Context.Assignments.AsQueryable().Where(a => ids.Contains(a.Id)).ToListAsync();

            foreach (var item in result)
            {
                AssignmentModelView tempAssignmentModelView = assignmentsMV.FirstOrDefault(a => a.Id == item.Id);
                if (tempAssignmentModelView != null)
                {
                    item.Ended = tempAssignmentModelView.Ended;
                }
            }

            await Context.SaveChangesAsync();

            return assignmentsMV;
        }

        public async Task<AssignmentModelView> DeleteAssignmentAsync(int id)
        {
            var result = await Context.Assignments.AsQueryable().FirstOrDefaultAsync(a => a.Id == id);
            Context.Assignments.Remove(result);
            await Context.SaveChangesAsync();

            AssignmentModelView oResult = new AssignmentModelView()
            {
                Id = result.Id,
                Name = result.Name,
                Repeated = result.Repeated,
                AssignmentType = new AssignmentTypeModelView()
                {
                    Id = result.TypeId,
                    Name = String.Empty
                },
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Ended = result.Ended,
                Description = result.Description,
                Archive = result.Archive

            };

            return oResult;
        }

        public async Task<IEnumerable<AssignmentModelView>> DeleteMultipleAssignmentsAsync(List<AssignmentModelView> assignmentsMV)
        {
            List<int> ids = assignmentsMV.Select(a => a.Id).ToList();
            var result = await Context.Assignments.AsQueryable().Where(a => ids.Contains(a.Id)).ToListAsync();

            Context.Assignments.RemoveRange(result);

            await Context.SaveChangesAsync();

            return assignmentsMV;
        }
    }

}
