using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Category;
using ModelDTO.ApplicationDTOs.Category;
using Models.ApplicationModels;

namespace Services.ApplicationServices.For;

public class UpdateServiceCategoryService : IUpdateServiceCategory
{
    private readonly IUpdateRepository<ServiceCategory> _updateRepository;
    private readonly IGetRepository<ServiceCategory> _getRepository;
    private readonly IMapper _mapper;

    public UpdateServiceCategoryService(IUpdateRepository<ServiceCategory> updateRepository, IMapper mapper,
        IGetRepository<ServiceCategory> getRepository)
    {
        _updateRepository = updateRepository;
        _mapper = mapper;
        _getRepository = getRepository;
    }

    public async Task<ServiceCategoryDTO> UpdateAsync(ServiceCategoryDTO updateRequest)
    {
        if (updateRequest == null)
            throw new ArgumentNullException(nameof(updateRequest));

        if (String.IsNullOrEmpty(updateRequest.Category))
            throw new ArgumentException($"{nameof(updateRequest.Category)} cannot be null or empty");

        if (updateRequest.ServiceCategoryId <= 0)
            throw new ArgumentException($"{nameof(updateRequest)}.{nameof(updateRequest.Category)} must be greater than 0");

        var applicationFor = await _getRepository.GetAsync(appfor => appfor.ServiceCategoryId == updateRequest.ServiceCategoryId);

        if (applicationFor == null)
            throw new ArgumentException($"{nameof(updateRequest.Category)} Doesn't exist");

        applicationFor.Category = updateRequest.Category;

        ServiceCategory updatedFor = await _updateRepository.UpdateAsync(applicationFor);

        if (updatedFor == null)
            throw new Exception($"Failed to update");

        ServiceCategoryDTO updatedDto = _mapper.Map<ServiceCategoryDTO>(updatedFor);

        if (updatedDto == null)
            throw new AutoMapperMappingException($"Mapping from {nameof(ServiceCategory)} to {nameof(ServiceCategoryDTO)} failed.");

        return updatedDto;
    }
}