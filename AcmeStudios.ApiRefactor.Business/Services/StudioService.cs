using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Models;
using AutoMapper;

namespace AcmeStudios.ApiRefactor.Business;

public class StudioService : IStudioService
{
    private readonly IStudioRepository _studioServiceRepository;
    public IMapper _mapper { get; set; }
    private MapperConfiguration config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<AutoMapperProfile>();
    });

    public StudioService(IStudioRepository studioServiceRepository)
    {
        _studioServiceRepository = studioServiceRepository;
        _mapper = config.CreateMapper();
    }

    public async Task<ServiceResponse<StudioItems>> GetStudioItemById(int id)
    {

        var item = await _studioServiceRepository.GetStudioItemById(id);

        var serviceResponse = new ServiceResponse<StudioItems>
        {
            Data = _mapper.Map<StudioItems>(item),
            Message = "Here's your selected studio item",
            Success = true
        };

        return serviceResponse;

    }

    public async Task<ServiceResponse<List<StudioItems>>> AddStudioItem(StudioItems studioItem)
    {
        var studioItems = await _studioServiceRepository.AddStudioItem(_mapper.Map<StudioItem>(studioItem));


        var serviceResponse = new ServiceResponse<List<StudioItems>>
        {
            Data = _mapper.Map<List<StudioItems>>(studioItems),
            Message = $"New item added",
            Success = true
        };

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<StudioItemHeaders>>> GetAllStudioHeaderItems()
    {
        var getAllStudioHeaderitems = await _studioServiceRepository.GetAllStudioHeaderItems();

        var serviceResponse = new ServiceResponse<List<StudioItemHeaders>>
        {
            Data = _mapper.Map<List<StudioItemHeaders>>(getAllStudioHeaderitems) ,
            Message = "Here's all the items in your studio",
            Success = true
        };

        return serviceResponse;
    }

    public async Task<ServiceResponse<StudioItems>> UpdateStudioItem(int StudioItemId, StudioItems studioItem)
    {
        var serviceResponse = new ServiceResponse<StudioItems>();

        var updatedStudioItem = await _studioServiceRepository.UpdateStudioItem(StudioItemId, _mapper.Map<StudioItem>(studioItem));

        serviceResponse.Data = _mapper.Map<StudioItems>(updatedStudioItem);
        serviceResponse.Message = "Update successful";
        serviceResponse.Success = true;


        return serviceResponse;

    }

    public async Task<ServiceResponse<List<StudioItems>>> DeleteStudioItem(int id)
    {
        var serviceResponse = new ServiceResponse<List<StudioItems>>();

        var studioItem = await _studioServiceRepository.DeleteStudioItem(id);

        serviceResponse.Data = _mapper.Map<List<StudioItems>>(studioItem);
        serviceResponse.Success = true;
        serviceResponse.Message = "Item deleted";

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<StudioItemTypes>>> GetAllStudioItemTypes()
    {

        var studioItemsTypes = await _studioServiceRepository.GetAllStudioItemTypes();

        var serviceResponse = new ServiceResponse<List<StudioItemTypes>>
        {
            Data = _mapper.Map<List<StudioItemTypes>>(studioItemsTypes),
            Message = "Item types fetched",
            Success = true
        };

        return serviceResponse;
    }
}