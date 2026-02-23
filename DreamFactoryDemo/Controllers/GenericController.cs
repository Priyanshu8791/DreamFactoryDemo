using DreamFactoryDemo.Model;
using DreamFactoryDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamFactoryDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : ControllerBase
    {
        private readonly DreamFactoryService _service;
        public GenericController(DreamFactoryService service)
        {
            _service = service;
        }

        [HttpGet("{tableName}/{id}")]
        public async Task<IActionResult> GetById(string tableName, string id)
        {
            var data = await _service.GetByIdAsync(tableName, id);
            return Ok(data);
        }

        [HttpGet("{tableName}")]
        public async Task<IActionResult> Get(string tableName)
        {
            var data = await _service.GetTableDataAsync(tableName);
            return Ok(data);
        }

        [HttpPost("{tableName}")]
        public async Task<IActionResult> Insert(string tableName, [FromBody] object data)
        {
            var result = await _service.InsertAsync(tableName, data);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetAttendance([FromBody] AttendanceRequest model)
        {
            var result = await _service.GetAttendance(
                model.EmpId,
                model.Month,
                model.Year);

            return Ok(result);
        }
    }
}
