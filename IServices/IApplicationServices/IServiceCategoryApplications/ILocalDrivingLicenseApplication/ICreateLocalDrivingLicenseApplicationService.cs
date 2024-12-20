﻿using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;

namespace IServices.IApplicationServices.IServiceCategoryApplications.ILocalDrivingLicenseApplication;

public interface ICreateLocalDrivingLicenseApplicationService
{
    Task<LocalDrivingLicenseApplication> Create(CreateLocalDrivingLicenseApplicationRequest request, ILocalDrivingLicenseApplicationServicePurposeValidator validator);
}