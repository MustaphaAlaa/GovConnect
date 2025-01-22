
using AutoMapper;
using IRepository.IGenericRepositories;
using IServices.IApplicationServices.Employee;
using ModelDTO.ApplicationDTOs.Employee;
using Models.ApplicationModels;
using System.Linq.Expressions;

namespace Services.ApplicationServices.Services.EmployeeAppService;

public class GetApplicationByEmployeeService : IAsyncApplicationRetrieverService
{

    private readonly IGetRepository<Application> _getReposiory;
    private readonly IMapper _mapper;

    public GetApplicationByEmployeeService(IGetRepository<Application> getReposiory, IMapper mapper)
    {
        _getReposiory = getReposiory;
        _mapper = mapper;
    }

    public async Task<ApplicationDTOForEmployee?> GetByAsync(Expression<Func<Application, bool>> predicate)
    {
        var application = await _getReposiory.GetAsync(predicate);

        return application is null ? null : _mapper.Map<ApplicationDTOForEmployee>(application);

    }


}
