using Models.Responses;
using Models.Requests;

namespace DataAccess
{
    public interface IAssignmentTypeService
    {
        public Task<List<AssignmentTypeModelView>> GetAssignmentsTypesAsync();
        public Task<AssignmentTypeModelView> NewAssignmentTypeAsync(AssignmentTypeModel assignmentType);
        public Task<AssignmentTypeModelView> GetAssignmentTypeByIdAsync(int typeId);
    }
}