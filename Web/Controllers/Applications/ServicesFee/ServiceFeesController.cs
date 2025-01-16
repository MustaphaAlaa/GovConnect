using IServices.IApplicationServices.Fees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.ApplicationDTOs.Fees;

namespace Web.Controllers.Applications.ServicesFee
{
    [Route("api/ServiceFees")]
    [ApiController]
    public class ServiceFeesController : ControllerBase
    {
        private readonly IUpdateServiceFees _updateServiceFees;
        private readonly ILogger<ServiceFeesController> _logger;

        public ServiceFeesController(IUpdateServiceFees updateServiceFees, ILogger<ServiceFeesController> logger)
        {
            _updateServiceFees = updateServiceFees;
            _logger = logger;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateServiceFees(ServiceFeesDTO serviceFeesDTO)
        {

            var UpdatedVersion = await _updateServiceFees.UpdateAsync(serviceFeesDTO);


            return Ok(UpdatedVersion);
        }
    }
}
