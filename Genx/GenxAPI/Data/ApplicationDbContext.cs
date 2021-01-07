using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenxAPI.Model;
using GenxAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace GenxAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
    }
}
