using IServices.ITests.ITestTypes;
using Microsoft.AspNetCore.Mvc;
using Models.Tests;

namespace Web.Controllers.Tests
{


    /*
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     !!!!!!!!!!!!!!!!!!!!!!!!!!
     * I'll clean and and restructre all endpoints later these for testing purpose
     *
     */


    [ApiController]
    [Route("api/Tests")]
    public class TestsController : ControllerBase
    {

        private readonly ITestTypeRetrievalService _getTestTypes;
        private readonly IAsyncAllTestTypesRetrieverService _getAllTestTypesService;
        private readonly ILogger<TestType> _logger;

        public TestsController(ITestTypeRetrievalService getTestTypes, IAsyncAllTestTypesRetrieverService getAllTestTypesService, ILogger<TestType> logger)
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
