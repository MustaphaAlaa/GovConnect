using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Admin;

[ApiController]
[Route("Admin/DrivingLicenseApplication")]
public class AdminApplicationController : ControllerBase
{
    public IActionResult AddApplicationType(string applicationType)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddApplicationFor(string applicationFor)
    {
        throw new NotImplementedException();
    }

    public IActionResult AddFees()
    {
        throw new NotImplementedException();
    }
}