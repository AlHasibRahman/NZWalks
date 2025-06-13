using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class SQLWalkRepositories : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepositories(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var getWalk = await dbContext.Walks.FirstOrDefaultAsync(C => C.Id == id);
            if (getWalk == null)
            {
                return null;
            }
            dbContext.Remove(getWalk);
            await dbContext.SaveChangesAsync();
            return getWalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var getWalk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(C => C.Id == id);
            if (getWalk == null)
            {
                return null;
            }
            return getWalk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var getWalk = await dbContext.Walks.FirstOrDefaultAsync(C => C.Id == id);
            if (getWalk == null)
            {
                return null;
            }
            getWalk.Name = walk.Name;
            getWalk.Description = walk.Description;
            getWalk.lengthInKm = walk.lengthInKm;
            getWalk.WalkImageUrl = walk.WalkImageUrl;
            getWalk.DifficultyId = walk.DifficultyId;
            getWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return getWalk;
        }
    }
}