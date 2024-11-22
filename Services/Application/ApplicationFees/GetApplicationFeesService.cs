using AutoMapper;
using IRepository;
using IServices.Application.Fees;

using ModelDTO.Application.Fees;
using Models.Applications;

using System.Linq.Expressions;

namespace Services.Application
{
    public class GetApplicationFeesService : IGetApplicationFees
    {
        private readonly IGetRepository<ApplicationFees> _getRepository;
        private readonly IMapper _mapper;

        public GetApplicationFeesService(IGetRepository<ApplicationFees> getRepository, IMapper mapper)
        {
            _getRepository = getRepository;
            _mapper = mapper;
        }

        public Task<ApplicationFeesDTO> GetByAsync(Expression<Func<ApplicationFeesDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
