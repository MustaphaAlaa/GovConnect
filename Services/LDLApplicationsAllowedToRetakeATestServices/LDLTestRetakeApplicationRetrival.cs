using IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;
using ModelDTO.TestsDTO;
using Models.Tests;

namespace Services.LDLApplicationsAllowedToRetakeATestServices;

public class LDLTestRetakeApplicationRetrival : ILDLTestRetakeApplicationRetrieve
{
    public Task<LDLApplicationsAllowedToRetakeATestDTO> GetByAsync(LDLApplicationsAllowedToRetakeATest typeDTO)
    {
        throw new NotImplementedException();
    }
}
