using ModelDTO.Application.Type;
using Models.ApplicationModels;

namespace IServices.Application.Type;

public interface IGetAllApplicationTypes :   IGetAllService< ApplicationTypeDTO, ApplicationType >
{
    
}