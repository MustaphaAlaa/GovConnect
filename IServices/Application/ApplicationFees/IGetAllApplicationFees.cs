
using ModelDTO.Application.ApplicationFees;
using ModelDTO.Application.Fees;

namespace IServices.Application;

public interface IGetAllApplicationFees : IGetAllService<ApplicationFeesDTO, CompositeKeyForApplicationFees>
{

}