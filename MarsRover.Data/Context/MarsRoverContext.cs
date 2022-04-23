using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data.Context
{
    public class MarsRoverContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TANJUAVSARR;Database=MarsRoverDB;Trusted_Connection=true;");
        }

        public DbSet<Rovers> Rovers { get; set; }
    }
}
