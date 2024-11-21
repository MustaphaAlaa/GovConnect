using IRepository;
using IServices.Application.For;
using Models.Applications;

namespace Services.Application.For;

public class DeleteApplicationForService : IDeleteApplicationFor
{
    private readonly IDeleteRepository<ApplicationFor> _deleteRepository;

    public DeleteApplicationForService(IDeleteRepository<ApplicationFor> deleteRepository)
    {
        _deleteRepository = deleteRepository;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException("Invalid Id");

        var deleted = await _deleteRepository.DeleteAsync(t => t.Id == id);

        return deleted > 0;
    }
}