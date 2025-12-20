using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.IPurpose;

//public interface IGetServicePurpose : IGeWhenService<ServicePurpose>
public interface IGetServicePurpose : IAsyncRetrieveService<ServicePurpose, ServicePurpose>
{

}