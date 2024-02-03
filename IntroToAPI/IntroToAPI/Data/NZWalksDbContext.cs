using IntroToAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IntroToAPI.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):  base(dbContextOptions)
        {
            
        }

        // dbset a property of dbcontext class that represents a collection of entities in the database
        // in an application we have Difficulty, Regions, Walks

        //Difficulty, Region, Walks comes from domain model
        public DbSet<Difficulty> Difficulties{ get; set; }
        public DbSet<Region> Regions { get; set; }  
        public DbSet<Walk> Walks { get; set; }

    }
}
