using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Responses;

namespace assignmentsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsTypesController : ControllerBase
    {
        private readonly IAssignmentTypeService _assignmentTypeService;

        public AssignmentsTypesController(IAssignmentTypeService assignmentTypeService)
        {
            _assignmentTypeService = assignmentTypeService;
        }

        [HttpGet("GetAssignmentsTypesAsync")]
        public async Task<ActionResult<List<AssignmentTypeModelView>>> GetAssignmentsTypesAsync()
        {

           var x = await _assignmentTypeService.GetAssignmentsTypesAsync();

            return Ok(x);
        }

        [HttpGet("GetAssignmentTypeByIdAsync/{typeId}")]
        public async Task<ActionResult<AssignmentTypeModelView>> GetAssignmentTypeByIdAsync(int typeId)
        {

            var x = await _assignmentTypeService.GetAssignmentTypeByIdAsync(typeId);

            return Ok(x);
        }

        [HttpPost("NewAssignmentTypeAsync")]
        public async Task<ActionResult<AssignmentTypeModelView>> NewAssignmentAsync([FromBody] AssignmentTypeModel assignment)
        {

            var x = await _assignmentTypeService.NewAssignmentTypeAsync(assignment);

            return Ok(x);
        }
    }
}
