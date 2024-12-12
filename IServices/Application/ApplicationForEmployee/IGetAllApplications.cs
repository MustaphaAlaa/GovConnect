using ModelDTO.ApplicationDTOs;
using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IApplicationServices.Employee;

public interface IGetAllApplicationsEmp : IGetAllService< LicenseApplication,ApplicationDTOForEmployee >
{
}
