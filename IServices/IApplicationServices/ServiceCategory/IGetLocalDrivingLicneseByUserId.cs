
using ModelDTO.ApplicationDTOs.Category;

namespace IServices.IApplicationServices.Category;
public interface IGetLocalDrivingLicenseByUserId
{
    Task<IEnumerable<DriverIdLicenseClassId>> Get(Guid usrId);
}
