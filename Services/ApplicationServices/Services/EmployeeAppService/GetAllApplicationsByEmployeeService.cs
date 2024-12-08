﻿using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Employee;
using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;
using System.Linq.Expressions;

namespace Services.ApplicationServices.Services.EmployeeAppService;

public class GetAllApplicationsByEmployeeService : IGetAllApplicationsEmp
{

    private readonly IMapper _mapper;
    private readonly IGetAllRepository<LicenseApplication> _getAllRepository;

    public GetAllApplicationsByEmployeeService(IGetAllRepository<LicenseApplication> getAllRepository, IMapper mapper)
    {
        _mapper = mapper;
        _getAllRepository = getAllRepository;
    }

    public async Task<List<ApplicationDTOForEmployee>> GetAllAsync()
    {
        var applications = await _getAllRepository.GetAllAsync();

        return applications.Select(app =>
                           _mapper.Map<ApplicationDTOForEmployee>(app)).ToList();
    }

    public async Task<IQueryable<ApplicationDTOForEmployee>> GetAllAsync(Expression<Func<LicenseApplication, bool>> predicate)
    {
        var applications = await _getAllRepository.GetAllAsync(predicate);

        var dtos = applications.Select(app => _mapper.Map<ApplicationDTOForEmployee>(app));

        return dtos;
    }
}
