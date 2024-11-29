using AutoMapper;
using IRepository;
using IServices.IApplicationServices.Fees;
using ModelDTO.ApplicationDTOs.Fees;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Fees
{
    public class UpdateApplicationFeesService : IUpdateApplicationFees
    {
        private readonly IMapper _mapper;
        private readonly IUpdateRepository<ApplicationFees> _updateRepository;
        private readonly IGetRepository<ApplicationFees> _getRepository;

        public UpdateApplicationFeesService(IMapper mapper,
                        IUpdateRepository<ApplicationFees> updateRepository,
                        IGetRepository<ApplicationFees> getRepository)
        {
            _mapper = mapper;
            _updateRepository = updateRepository;
            _getRepository = getRepository;
        }

        public async Task<ApplicationFeesDTO> UpdateAsync(ApplicationFeesDTO updateRequest)
        {
            if (updateRequest == null)
                throw new ArgumentNullException($"Cannot update null request.");

            if (updateRequest.ApplicationTypeId <= 0)
                throw new ArgumentOutOfRangeException("Type id must be greater than 0");

            if (updateRequest.ApplicationForId <= 0)
                throw new ArgumentOutOfRangeException("For id must be greater than 0");



            var applicationFees = await _getRepository.GetAsync(appFees =>
                  appFees.ApplicationTypeId == updateRequest.ApplicationTypeId
                  && appFees.ApplicationForId == updateRequest.ApplicationTypeId);

            if (applicationFees == null)
                throw new Exception("ApplicationFees doesn't exist.");

            if (updateRequest.LastUdpate < applicationFees.LastUpdate)
                throw new ArgumentOutOfRangeException("Invalid Date Range");

            ApplicationFees toUpdateObject = _mapper.Map<ApplicationFees>(updateRequest)
                 ?? throw new AutoMapperMappingException();

            ApplicationFees updatedObject = (await _updateRepository.UpdateAsync(toUpdateObject))
                ?? throw new Exception("Does not updated");

            ApplicationFeesDTO applicationFeesDTO = _mapper.Map<ApplicationFeesDTO>(updatedObject)
                    ?? throw new AutoMapperMappingException();

            return applicationFeesDTO;
        }
    }
}
