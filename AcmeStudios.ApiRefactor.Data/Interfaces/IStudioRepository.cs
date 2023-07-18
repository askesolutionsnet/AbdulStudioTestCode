using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Data;

public interface IStudioRepository
{
    Task<List<StudioItem>> AddStudioItem(StudioItem studioItem);

    Task<List<StudioItem>> GetAllStudioHeaderItems();

    Task<StudioItem> GetStudioItemById(int id);

    Task<StudioItem> UpdateStudioItem(int StudioItemId, StudioItem studioItem);

    Task<List<StudioItem>> DeleteStudioItem(int id);

    Task<List<StudioItemType>> GetAllStudioItemTypes();
}
