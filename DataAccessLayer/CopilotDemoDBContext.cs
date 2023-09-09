using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CopilotDemoDBContext : DbContext
    {
        public CopilotDemoDBContext(DbContextOptions<CopilotDemoDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
