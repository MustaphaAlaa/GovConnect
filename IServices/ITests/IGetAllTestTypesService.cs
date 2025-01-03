using ModelDTO.TestsDTOs;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.ITests;

public interface IGetAllTestTypesService : IGetAllService<TestType, TestTypeDTO>
{
}
