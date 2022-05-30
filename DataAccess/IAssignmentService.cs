using Models.Requests;
using Models.Responses;

namespace DataAccess
{
    public interface IAssignmentService
    {
        public Task<AssignmentModelView> NewAssignmentAsync(AssignmentModel assignment);
        public Task<IEnumerable<AssignmentModelView>> ArchiveAssignmentsAsync();
        public Task<IEnumerable<AssignmentModelView>> GetAssignmentsAsync(bool withArchive, int page);
        public Task<IEnumerable<AssignmentModelView>> SetMultipleAssignmentsEndedAsync(List<AssignmentModelView> assignmentsMV);
        public Task<AssignmentModelView> DeleteAssignmentAsync(int id);
        public Task<IEnumerable<AssignmentModelView>> DeleteMultipleAssignmentsAsync(List<AssignmentModelView> assignmentsMV);
        //public Task<AssignmentModelView> ArchiveAssignmentAsync(AssignmentModelView assignmentMV);
        public Task<IEnumerable<AssignmentModelView>> ArchiveMultipleAssignmentsAsync(List<AssignmentModelView> assignmentsMV);
        public Task<int> CountAssignmentsAsync(bool includeArchive);
    }
}