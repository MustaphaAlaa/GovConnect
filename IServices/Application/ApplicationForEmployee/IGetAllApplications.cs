using ModelDTO.Application;
using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.Application.Employee;

public interface IGetAllApplicationsEmp : IGetAllService<ApplicationDTOForEmployee, Models.ApplicationModels.Application>
{
}
