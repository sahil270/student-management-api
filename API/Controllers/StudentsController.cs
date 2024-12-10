using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentesService;

        public StudentsController(IStudentService studentService)
        {
            _studentesService = studentService;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _studentesService.GetAllStudentesAsync();
            var response = new BaseResponse(StatusCodes.Status200OK, data);

            return StatusCode(response.StatusCode, response);
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _studentesService.GetStudentByIdAsync(id);
            var response = new BaseResponse(StatusCodes.Status200OK, data);

            return StatusCode(response.StatusCode, response);
        }
        
        
        [HttpGet("Class/{id}")]
        public async Task<IActionResult> GetByClass(int id)
        {
            var data = await _studentesService.GetAllStudentByClassIdAsync(id);
            var response = new BaseResponse(StatusCodes.Status200OK, data);

            return StatusCode(response.StatusCode, response);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDto value)
        {
            var response = new BaseResponse(StatusCodes.Status201Created);
            if (ModelState.IsValid)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = $"Invalid Input, Provided input has {ModelState.ErrorCount} Errors";
            }
            else
            {
                var data = await _studentesService.AddStudentAsync(value);
                response.Message = "Added SuccessFully";
                response.Data = data;
            }

            return StatusCode(response.StatusCode, response);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDto value)
        {
            var response = new BaseResponse(StatusCodes.Status201Created);
            if (ModelState.IsValid)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = $"Invalid Input, Provided input has {ModelState.ErrorCount} Errors";
            }
            else
            {
                var data = await _studentesService.UpdateStudentAsync(id, value);
                response.Message = "Updated SuccessFully";
                response.Data = data;
            }

            return StatusCode(response.StatusCode, response);
        }
        
        // PUT api/<StudentsController>/5
        [HttpPut("{id}/Class/{classId}")]
        public async Task<IActionResult> Assign([FromRoute] int id, [FromRoute] int classId)
        {
            await _studentesService.AssignStudentToClass(id, classId);
            var response = new BaseResponse(StatusCodes.Status200OK, null, "Student Assigned to Class Successfully");

            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentesService.DeleteStudentAsync(id);
            var response = new BaseResponse(StatusCodes.Status200OK, null, "Deleted SuccessFully");

            return StatusCode(response.StatusCode, response);
        }
    }
}
