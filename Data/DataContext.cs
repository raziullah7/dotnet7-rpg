using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg_vs.Data
{
    public class DataContext : DbContext
    {
        // DbSet type field represents a Table in the Database
        public DbSet<Character> Characters => Set<Character>();

        // constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}