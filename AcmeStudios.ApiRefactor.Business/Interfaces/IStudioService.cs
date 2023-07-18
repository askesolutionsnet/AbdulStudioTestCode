using AcmeStudios.ApiRefactor.Models;

namespace AcmeStudios.ApiRefactor.Business;

public interface IStudioService
{
    Task<ServiceResponse<List<StudioItems>>> AddStudioItem(StudioItems studioItem);

    Task<ServiceResponse<List<StudioItemHeaders>>> GetAllStudioHeaderItems();

    Task<ServiceResponse<StudioItems>> GetStudioItemById(int id);

    Task<ServiceResponse<StudioItems>> UpdateStudioItem(int StudioItemId, StudioItems studioItem);

    Task<ServiceResponse<List<StudioItems>>> DeleteStudioItem(int id);

    Task<ServiceResponse<List<StudioItemTypes>>> GetAllStudioItemTypes();
}
