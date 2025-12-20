using ModelDTO.TestsDTO;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.ILDLApplicationsAllowedToRetakeATestServices;

/// <summary>
/// Interface for inserting records into the LDLApplicationsAllowedToRetakeATest table.
/// </summary>
public interface ILDLTestRetakeApplicationCreator
//: ICreateService<TestDTO, LDLApplicationsAllowedToRetakeATestDTO>
{
    Task CreateAsync(TestDTO testDTO);
}
