
using IServices.ITests;
using Microsoft.AspNetCore.Mvc;
using Models.Tests;

namespace Web.Controllers.Tests
{
    [ApiController]
    [Route("api/Tests")]
    public class TestsController : ControllerBase
    {

        private readonly IGetTestTypeService _getTestTypes;
        private readonly IGetAllTestTypesService _getAllTestTypesService;
        private readonly ILogger<TestType> _logger;

        public TestsController(IGetTestTypeService getTestTypes, IGetAllTestTypesService getAllTestTypesService, ILogger<TestType> logger)
        {
            _getTestTypes = getTestTypes;
            _logger = logger;
            _getAllTestTypesService = getAllTestTypesService;
        }

        [HttpGet("/types/{TypeId:int}")]
        public async Task<IActionResult> GetType(int TypeId)
        {
            _logger.LogInformation($"Get TestType by id:{TypeId}");
            var tt = await _getTestTypes.GetByAsync(x => x.TestTypeId == TypeId);
            return Ok(tt);
        }

        [HttpGet("/types")]
        public async Task<IActionResult> GetTypes()
        {
            _logger.LogInformation($"{this.GetType().Name} --- GetTypes Action.");
            var tts = await _getAllTestTypesService.GetAllAsync();
            return Ok(tts);
        }



        [HttpGet("/types/{TypeId}/available-days")]
        public IActionResult GetTypeDays()
        {
            return Ok("Hello World");
        }

        [HttpGet("/types/{TypeId}/available-timeslots/{day}")]
        public IActionResult GetTypeAppointment(/*Dummy parameters*/int TypeId, int day)
        {
            return Ok("Hello World");
        }


    }
}
