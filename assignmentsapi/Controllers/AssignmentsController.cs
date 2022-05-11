using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Responses;
using DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace assignmentsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("CountAssignmentsAsync/{includeArchive}")]
        public async Task<ActionResult<int>> CountAssignmentsAsync(bool includeArchive)
        {
            var result = await _assignmentService.CountAssignmentsAsync(includeArchive);

            return Ok(result);

        }

        [HttpPost("NewAssignmentAsync")]
        public async Task<ActionResult<AssignmentModelView>> NewAssignmentAsync([FromBody] AssignmentModel assignment)
        {

            await _assignmentService.NewAssignmentAsync(assignment);

            return Ok(assignment);
        }

        [HttpGet("GetAssignmentsAsync/{withArchive}")]
        public async Task<ActionResult<List<AssignmentModelView>>> GetAssignmentsAsync(bool withArchive)
        {
            var result = await _assignmentService.GetAssignmentsAsync(withArchive);

            return Ok(result);

        }

        [HttpPost("SetMultipleAssignmentsEndedAsync")]
        public async Task<ActionResult<AssignmentModelView>> SetMultipleAssignmentsEndedAsync([FromBody] List<AssignmentModelView> assignmentsMV)
        {
            var result = await _assignmentService.SetMultipleAssignmentsEndedAsync(assignmentsMV);

            return Ok(result);
        }

        [HttpDelete("DeleteAssignmentAsync/{id}")]
        public async Task<ActionResult<AssignmentModelView>> DeleteAssignmentAsync(int id)
        {
            var result = await _assignmentService.DeleteAssignmentAsync(id);

            return Ok(result);
        }

        [HttpPost("DeleteMultipleAssignmentsAsync")]
        public async Task<ActionResult<AssignmentModelView>> DeleteMultipleAssignmentsAsync([FromBody] List<AssignmentModelView> assignmentsMV)
        {
            var result = await _assignmentService.DeleteMultipleAssignmentsAsync(assignmentsMV);

            return Ok(result); 
        }

        [HttpPost("ArchiveMultipleAssignmentsAsync")]
        public async Task<ActionResult<AssignmentModelView>> ArchiveAssignmentAsync([FromBody] List<AssignmentModelView> assignmentsMV)
        {
            var result = await _assignmentService.ArchiveMultipleAssignmentsAsync(assignmentsMV);

            return Ok(result);
        }

        //[HttpPost("ArchiveAssignmentAsync")]
        //public async Task<ActionResult<AssignmentModelView>> ArchiveAssignmentAsync([FromBody] AssignmentModelView assignment)
        //{
        //    var result = await _assignmentService.ArchiveAssignmentAsync(assignment);

        //    return Ok(result);
        //}


        //[HttpPost("FinishAssignment")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IAssignmentModel> FinishAssignment(IAssignmentModel assignment)
        //{
        //    return Ok();
        //}


        //[HttpDelete("DeleteAssignment")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IAssignmentModel> DeleteAssignment(IAssignmentModel assignment)
        //{
        //    return Ok();
        //}


        //[HttpGet("GetAssignments")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IAssignmentModel> GetAssignments()
        //{
        //    return Ok();
        //}

        //[HttpGet("GetAssignmentById/{AssignmentId}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IAssignmentModel> GetAssignmentById(int AssignmentId)
        //{
        //    return Ok();
        //}

        //[HttpGet("GetAssignmentsTypes")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IAssignmentModel> GetAssignmentsTypes()
        //{
        //    return Ok();
        //}
    }
}
