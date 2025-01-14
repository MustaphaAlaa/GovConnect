using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IServices.IApplicationServices.User;



public interface IApplicationRetrieverByUser : IRetrieveByTypeService<GetApplicationByUser, Models.ApplicationModels.Application?>

{

}
