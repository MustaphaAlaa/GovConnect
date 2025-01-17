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
public interface ILDLTestRetakeApplicationCreator
{
    Task CreateAsync(object? sender, TestDTO e);
}
