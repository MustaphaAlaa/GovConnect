using ModelDTO.ApplicationDTOs.Purpose;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.IPurpose;

public interface IGetAllServicePurpose : IAsyncAllRecordsRetrieverService<ServicePurpose, ServicePurposeDTO>
{

}