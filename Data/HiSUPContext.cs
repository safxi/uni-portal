using Microsoft.EntityFrameworkCore;
using HiSUP.Models;

namespace HiSUP.Data
{
    public class HiSUPContext : DbContext
    {
        public HiSUPContext(DbContextOptions<HiSUPContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; } = null!;
    }
}
