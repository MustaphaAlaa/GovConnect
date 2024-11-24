using ModelDTO.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices.Application.User
{
    public interface ICreateApplication : ICreateService<CreateApplicationRequest, ApplicationDTOForUser>
    {
    }
}
