using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classesService;

        public ClassesController(IClassService classesService)
        {
            _classesService = classesService;
        }

        // GET: api/<ClassesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _classesService.GetAllClassesAsync();
            var response = new BaseResponse(StatusCodes.Status200OK, data);

            return StatusCode(response.StatusCode, response);

        }

        // GET api/<ClassesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _classesService.GetAllClassByIdAsync(id);
            var response = new BaseResponse(StatusCodes.Status200OK, data);

            return StatusCode(response.StatusCode, response);
        }

        // POST api/<ClassesController>
        [HttpPost]
        public  async Task<IActionResult> Post([FromBody] ClassDto value)
        {
            var response = new BaseResponse(StatusCodes.Status201Created);
            if(ModelState.IsValid)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = $"Invalid Input, Provided input has {ModelState.ErrorCount} Errors";
            }
            else
            {
                var data = await _classesService.AddClassAsync(value);
                response.Message = "Added SuccessFully";
                response.Data = data;
            }

            return StatusCode(response.StatusCode, response);

        }

        // PUT api/<ClassesController>/5
        [HttpPut("{id}")]
        public  async Task<IActionResult> Put(int id, [FromBody] ClassDto value)
        {
            var response = new BaseResponse(StatusCodes.Status200OK);
            if (ModelState.IsValid)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = $"Invalid Input, Provided input has {ModelState.ErrorCount} Errors";
            }
            else
            {
                var data = await _classesService.UpdateClassAsync(id, value);
                response.Data = data;
                response.Message = "Updated SuccessFully";

            }
            return StatusCode(response.StatusCode, response);
        }

        // DELETE api/<ClassesController>/5
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            await _classesService.DeleteClassAsync(id);
            var response = new BaseResponse(StatusCodes.Status200OK, null, "Deleted SuccessFully");

            return StatusCode(response.StatusCode, response);
        }
    }
}
