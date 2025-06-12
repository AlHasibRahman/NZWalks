using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class SQLRegionRepositories : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepositories(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region region = await dbContext.Regions.FirstOrDefaultAsync(C => C.Id == id);
            if (region == null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            Region region = await dbContext.Regions.FirstOrDefaultAsync(C => C.Id == id);
            if (region == null)
            {
                return null;
            }
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var getregion = await dbContext.Regions.FirstOrDefaultAsync(C => C.Id == id);
            if (getregion == null)
            {
                return null;
            }
            getregion.Code = region.Code;
            getregion.Name = region.Name;
            getregion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return getregion;
        }
    }
}