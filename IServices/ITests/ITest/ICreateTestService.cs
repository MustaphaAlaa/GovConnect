using ModelDTO.TestsDTO;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.ITests.ITest
{
    public interface ICreateTestService : ICreateService<Test, TestDTO>
    {
    }
}
