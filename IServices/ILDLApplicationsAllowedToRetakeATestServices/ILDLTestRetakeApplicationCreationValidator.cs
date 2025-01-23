namespace IServices.ILDLApplicationsAllowedToRetakeATestServices;

/// <summary>
/// abstract class for valdating the inserting object request befor insrting into the LDLApplicationsAllowedToRetakeATest table.
/// </summary> 
public interface ILDLTestRetakeApplicationCreationValidator
{
    Task<bool> IsValid(int LDLApplicationId, int TestTypeId);


}