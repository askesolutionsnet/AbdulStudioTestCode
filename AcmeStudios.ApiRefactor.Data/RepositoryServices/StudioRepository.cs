//using AutoMapper;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

namespace AcmeStudios.ApiRefactor.Data;

public class StudioRepository : IStudioRepository, IDisposable
{
    internal DbContext _context;

    public StudioRepository(DbContext dbContext)
    {
        this._context = dbContext;
    }

    public async Task<List<StudioItem>> AddStudioItem(StudioItem studioItem)
    {
        try
        {
            await _context.StudioItems.AddAsync(studioItem);
            await _context.SaveChangesAsync();

            return await _context.StudioItems.ToListAsync();
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public async Task<List<StudioItem>> GetAllStudioHeaderItems()
    {
        return await _context.StudioItems.ToListAsync();
    }

    public async Task<StudioItem> GetStudioItemById(int id)
    {

        return await _context.StudioItems
        .Where(item => item.StudioItemId == id)
        .Include(type => type.StudioItemType)
        .FirstOrDefaultAsync();
    }

    public async Task<StudioItem> UpdateStudioItem(int StudioItemId, StudioItem studioItem)
    {
        try
        {
            StudioItem updatedStudioItem = await _context.StudioItems
              .FirstOrDefaultAsync(c => c.StudioItemId == StudioItemId);

            updatedStudioItem.Acquired = studioItem.Acquired;
            updatedStudioItem.Description = studioItem.Description;
            updatedStudioItem.Eurorack = studioItem.Eurorack;
            updatedStudioItem.Name = studioItem.Name;
            updatedStudioItem.Price = studioItem.Price;
            updatedStudioItem.SerialNumber = studioItem.SerialNumber;
            updatedStudioItem.Sold = studioItem.Sold;
            updatedStudioItem.SoldFor = studioItem.SoldFor;
            updatedStudioItem.StudioItemTypeId = studioItem.StudioItemTypeId;

            _context.StudioItems.Update(updatedStudioItem);
            await _context.SaveChangesAsync();

            return studioItem;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<StudioItem>> DeleteStudioItem(int id)
    {

        StudioItem item = await _context.StudioItems.FirstAsync(c => c.StudioItemId == id);
        _context.Remove(item);
        await _context.SaveChangesAsync();

        return await _context.StudioItems.ToListAsync();

    }

    public async Task<List<StudioItemType>> GetAllStudioItemTypes()
    {
        return await _context.StudioItemTypes.OrderBy(s => s.Value).ToListAsync();
    }

    #region Dispose
    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
            _context = null;
        }
    }
    #endregion
}