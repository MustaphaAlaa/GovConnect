
using ModelDTO.ApplicationDTOs.Category;

namespace IServices.IApplicationServices.Category;
public interface IGetLocalDrivingLicneseByUserId
{
    Task<IEnumerable<DriverIdLicenseClassId>> Get(Guid usrId);
}
