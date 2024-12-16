using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Fees
{
    public class UpdateServiceFeesService : IUpdateApplicationFees
    {
        private readonly IMapper _mapper;
        private readonly IUpdateRepository<ServiceFees> _updateRepository;
        private readonly IGetRepository<ServiceFees> _getRepository;

        public UpdateServiceFeesService(IMapper mapper,
                        IUpdateRepository<ServiceFees> updateRepository,
                        IGetRepository<ServiceFees> getRepository)
        {
            _mapper = mapper;
            _updateRepository = updateRepository;
            _getRepository = getRepository;
        }

        public async Task<ServiceFeesDTO> UpdateAsync(ServiceFeesDTO updateRequest)
        {
            if (updateRequest == null)
                throw new ArgumentNullException($"Cannot update null request.");

            if (updateRequest.ApplicationPuropseId <= 0)
                throw new ArgumentOutOfRangeException("Purpose id must be greater than 0");

            if (updateRequest.ServiceCategoryId <= 0)
                throw new ArgumentOutOfRangeException("Category id must be greater than 0");



            var applicationFees = await _getRepository.GetAsync(appFees =>
                  appFees.ApplicationTypeId == updateRequest.ApplicationPuropseId
                  && appFees.ServiceCategoryId == updateRequest.ApplicationPuropseId);

            if (applicationFees == null)
                throw new Exception("ServiceFees doesn't exist.");

            if (updateRequest.LastUpdate < applicationFees.LastUpdate)
                throw new ArgumentOutOfRangeException("Invalid Date Range");

            ServiceFees toUpdateObject = _mapper.Map<ServiceFees>(updateRequest)
                 ?? throw new AutoMapperMappingException();

            ServiceFees updatedObject = (await _updateRepository.UpdateAsync(toUpdateObject))
                ?? throw new Exception("Does not updated");

            ServiceFeesDTO serviceFeesDto = _mapper.Map<ServiceFeesDTO>(updatedObject)
                    ?? throw new AutoMapperMappingException();

            return serviceFeesDto;
        }
    }
}
