using ModelDTO.TestsDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.ITests.ITestTypes;

public interface IAsyncAllTestTypesRetrieverService : IAsyncAllRecordsRetrieverService<TestType, TestTypeDTO>
{
}
