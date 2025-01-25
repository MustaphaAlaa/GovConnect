namespace IServices.IValidators.DriverValidators;

public interface IDriverCreationValidator
{
    Task<bool> IsValid(Guid UserId);
}