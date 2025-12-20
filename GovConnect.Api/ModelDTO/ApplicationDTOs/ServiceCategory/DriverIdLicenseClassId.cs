using Models.LicenseModels;

namespace ModelDTO.ApplicationDTOs.Category;
public record DriverIdLicenseClassId(Guid driverId, LocalDrivingLicense localDrivingLicense);
