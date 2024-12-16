using IRepository;
using IServices.IApplicationServices.Purpose;
using Models.ApplicationModels;

namespace Services.ApplicationServices.Purpose;

public class DeleteApplicationPurposeService : IDeleteApplicationPurpose
{
    private readonly IDeleteRepository<ApplicationPurpose> _deleteRepository;

    public DeleteApplicationPurposeService(IDeleteRepository<ApplicationPurpose> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException( );

        var deleted = await _deleteRepository.DeleteAsync(t => t.ApplicationPurposeId == id);

        return deleted > 0;
    }
}