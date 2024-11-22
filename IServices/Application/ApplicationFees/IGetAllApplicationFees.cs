
using ModelDTO.Application.Fees;

namespace IServices.Application.Fees;

public interface IGetAllApplicationFees : IGetAllService<ApplicationFeesDTO, CompositeKeyForApplicationFees>
{

}