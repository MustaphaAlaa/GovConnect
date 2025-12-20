using ModelDTO.TestsDTO;

namespace IServices.IValidators;

public interface ICreateTestValidator
{
    public Task<bool> IsValid(CreateTestRequest request);
}