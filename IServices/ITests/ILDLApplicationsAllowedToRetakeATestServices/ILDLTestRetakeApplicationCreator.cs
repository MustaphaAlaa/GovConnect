using ModelDTO.TestsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.ITests.ILDLApplicationsAllowedToRetakeATestServices;

/// <summary>
/// Interface for inserting records into the LDLApplicationsAllowedToRetakeATest table.
/// </summary>
public abstract class LDLTestRetakeApplicationCreatorBase
{
    protected abstract Task CreateAsync(object? sender, TestDTO e);
}


/// <summary>
/// abstract class for valdating the inserting object request befor insrting into the LDLApplicationsAllowedToRetakeATest table.
/// </summary> 
public interface ILDLTestRetakeApplicationCreationValidator
{
    Task<bool> IsValid(int LDLApplicationId, int TestTypeId);


}