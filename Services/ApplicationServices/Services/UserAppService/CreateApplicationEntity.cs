﻿using AutoMapper;
using IRepository.IGenericRepositories;
using ModelDTO.ApplicationDTOs.User;
using Models.ApplicationModels;
using Services.Exceptions;

namespace Services.ApplicationServices.Services.UserAppServices;

public class CreateApplicationEntity : ICreateApplicationEntity
{
    private readonly ICreateRepository<Application> _createRepository;
    private readonly IMapper _mapper;


    public CreateApplicationEntity(ICreateRepository<Application> createRepository,
        IMapper mapper)
    {
        _createRepository = createRepository;
        _mapper = mapper;
    }


    public async Task<Application> CreateNewApplication(CreateApplicationRequest request, ServiceFees serviceFees)
    {

        if (serviceFees is null)
        {
            throw new ArgumentNullException();
        }

        var newApplication = _mapper.Map<Application>(request)
                             ?? throw new AutoMapperMappingException();

        newApplication.ApplicationDate = DateTime.Now;
        newApplication.LastStatusDate = DateTime.Now;
        newApplication.PaidFees = serviceFees.Fees;
        newApplication.ApplicationStatus = (EnApplicationStatus.InProgress);
        newApplication.UpdatedByEmployeeId = null;

        /*i'll make class/method to create it and all application needed */
        Application applicationisCreated = await _createRepository.CreateAsync(newApplication)
                                           ?? throw new FailedToCreateException();
        return applicationisCreated;
    }
}