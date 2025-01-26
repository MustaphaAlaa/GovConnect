using ModelDTO.TestsDTO;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.ITests.ITest;

public interface ITestCreationService : ICreateService<CreateTestRequest, TestDTO>
{
    public event Func<object?, TestDTO, Task> TestCreated;
    public event Func<object?, TestDTO, Task> OnFinalTestPassed;
}