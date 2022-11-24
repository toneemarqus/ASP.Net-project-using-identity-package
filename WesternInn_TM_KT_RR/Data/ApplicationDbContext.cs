using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WesternInn_TM_KT_RR.Model;

namespace WesternInn_TM_KT_RR.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WesternInn_TM_KT_RR.Model.Guest> Guest { get; set; }
        public DbSet<WesternInn_TM_KT_RR.Model.Booking> Booking { get; set; }
        public DbSet<WesternInn_TM_KT_RR.Model.Room> Room { get; set; }
    }
}