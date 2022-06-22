using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// Connects to the database through Entity Framework
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Contructor that takes in options
        /// </summary>
        /// <param name="options">options are</param>
        /// <returns></returns>
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        /// <summary>
        /// DbSet is of type AppUser and will translate Users into the database
        /// </summary>
        /// <value></value>
        public DbSet<AppUser> Users { get; set; }
    }
}